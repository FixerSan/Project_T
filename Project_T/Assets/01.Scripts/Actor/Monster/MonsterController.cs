using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : Actor
{
    public Monster monster;
    private StateMachine<MonsterController> fsm;
    private Dictionary<Define.MonsterState, State<MonsterController>> states;
    public Dictionary<Define.MonsterState, int> animationHashs = new Dictionary<Define.MonsterState, int>();

    public Define.MonsterState currentState;
    public Define.MonsterState changeState;
    public Rigidbody2D rb;
    public Animator anim;

    private bool init = false;
    public bool isDead = false;
    public void Init(Monster _monster, Dictionary<Define.MonsterState, State<MonsterController>> _states, Status _status)
    {
        monster = _monster;
        states = _states;
        status = _status;

        fsm = new StateMachine<MonsterController>(this, states[Define.MonsterState.Create]);
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();

        animationHashs.Clear();
        animationHashs.Add(Define.MonsterState.Idle, Animator.StringToHash("0_idle"));
        animationHashs.Add(Define.MonsterState.Move, Animator.StringToHash("1_Run"));
        animationHashs.Add(Define.MonsterState.Follow, Animator.StringToHash("1_Run"));
        animationHashs.Add(Define.MonsterState.Attack, Animator.StringToHash("2_Attack_Normal"));
        animationHashs.Add(Define.MonsterState.Die, Animator.StringToHash("4_Death"));
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);

        init = true;
        isDead = false;
    }


    public void ChangeState(Define.MonsterState _nextState, bool _isCanChangeSameState = false)
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

    public void SetAttackTarget(PlayerController _attackTarget)
    {
        monster.SetAttackTarget(_attackTarget);
    }

    public void Update()
    {
        if (!init) return;
        if (isDead) return;
        if (monster.CheckDie())
            return;
        CheckChangeStateInIspector();
        fsm.Update();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, monster.data.findAttackTargetRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, monster.data.attackTargetRange);
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
