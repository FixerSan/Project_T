using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    public Player player;
    private StateMachine<PlayerController> fsm;
    private Dictionary<Define.PlayerState, State<PlayerController>> states;
    public Dictionary<Define.PlayerState, int> animationHashs = new Dictionary<Define.PlayerState, int>();

    public Dictionary<Define.Attacks, BaseAttack> attacks = new Dictionary<Define.Attacks, BaseAttack>();

    public Define.PlayerState currentState;
    public Define.PlayerState changeState;
    public Rigidbody2D rb;
    public Animator anim;

    public MonsterController attackTarget;

    public readonly Vector3 positionVisualOffset = new Vector3(0f, 0.4f, 0f);

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

        status.currentNowHP = 10000;
        status.defaultAttackForce = 100;

        Managers.Game.stage.GetAttack(Define.Attacks.Hammer);
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
    public override void Hit(float _damage, Vector3 _knockBackDir, float _knockBackForce = 0)
    {
        GetDamage(_damage);
    }

    public override void GetDamage(float _damage, Action _callback = null)
    {
        status.currentNowHP -= _damage;
    }
    public override void KnockBack(Vector3 _knockBackDir, float _knockBackForce = 0)
    {

    }

    public Transform FindAttackTarget()
    {
        MonsterController attackTarget = null;
        for (int i = 0; i < Managers.Object.monsters.Count; i++)
        {
            if (Managers.Object.monsters[i].currentState == Define.MonsterState.Die) continue;
            if (attackTarget == null)
            {
                attackTarget = Managers.Object.monsters[i];
                continue;
            }

            if (Vector2.Distance(transform.position + positionVisualOffset, attackTarget.transform.position) > Vector2.Distance(transform.position + positionVisualOffset, Managers.Object.monsters[i].transform.position))
                attackTarget = Managers.Object.monsters[i];
        }

        if (attackTarget == null) return null;
        return attackTarget.transform;
    }

}
