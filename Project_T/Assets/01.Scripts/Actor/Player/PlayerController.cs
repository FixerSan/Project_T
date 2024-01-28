using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    public Player player;
    private StateMachine<PlayerController> fsm;
    private Dictionary<Define.PlayerState, State<PlayerController>> states;
    public Dictionary<Define.PlayerState, int> animationHashs = new Dictionary<Define.PlayerState, int>();

    public Define.PlayerState currentState;
    public Define.PlayerState changeState;
    public Rigidbody2D rb;
    public Animator anim;

    public bool isDead = false;
    private bool init = false;

    public void Init(Player _player, Dictionary<Define.PlayerState, State<PlayerController>> _states, Status _status)
    {
        player = _player;
        states = _states;
        status = _status;

        fsm = new StateMachine<PlayerController>(this, states[Define.PlayerState.Idle]);
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();
        animationHashs.Clear();
        animationHashs.Add(Define.PlayerState.Idle, Animator.StringToHash("0_idle"));
        animationHashs.Add(Define.PlayerState.Follow, Animator.StringToHash("1_Run"));
        animationHashs.Add(Define.PlayerState.Move, Animator.StringToHash("1_Run"));
        animationHashs.Add(Define.PlayerState.Attack, Animator.StringToHash("2_Attack_Normal"));
        animationHashs.Add(Define.PlayerState.Die, Animator.StringToHash("4_Death")); 

        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);

        isDead = false;
        init = true;

        player.FindAttackTargetLoop();
        status.currentNowHP = 10000;
    }

    public void Update()
    {
        if (!init) return;
        if (isDead) return;
        if (player.CheckDie())
            return;
        fsm.Update();
        CheckChangeStateInIspector();
    }

    public void ChangeState(Define.PlayerState _nextState, bool _isCanChangeSameState = false)
    {
        if (currentState == _nextState)
        {
            if (_isCanChangeSameState)
                fsm.ChangeState(states[_nextState]);
            return;
        }
        currentState = _nextState;
        changeState = _nextState;
        anim.Play(animationHashs[_nextState]);
        fsm.ChangeState(states[_nextState]);
    }

    public void CheckChangeStateInIspector()
    {
        if (currentState != changeState)
            ChangeState(changeState);
    }
    public override void Hit(float _damage)
    {
        GetDamage(_damage);
    }

    public override void GetDamage(float _damage)
    {
        status.currentNowHP -= _damage;
    }
}
