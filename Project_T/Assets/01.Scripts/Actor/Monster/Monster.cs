using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Monster
{
    public MonsterData data;
    public MonsterController controller;

    public Actor attackTarget;

    private Vector3 dir;
    #region Temp
    private Collider2D[] tempColliders;
    private PlayerController tempPlayer;
    private Vector3 tempVector;
    #endregion

    public void Created()
    {
        controller.StartCoroutine(CreatedRoutine());
    }

    public IEnumerator CreatedRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        controller.ChangeState(Define.MonsterState.Idle);
    }

    public void Move(Vector2 _moveDir)
    {
        if (_moveDir.x > 0) controller.ChangeDirection(Define.Direction.Left);
        if (_moveDir.x < 0) controller.ChangeDirection(Define.Direction.Right);

        controller.rb.velocity = _moveDir * controller.status.CurrentSpeed * 5 * Time.fixedDeltaTime;
    }

    public void Stop()
    {
        controller.rb.velocity = Vector2.zero;
    }

    public void SetAttackTarget(Actor _attackTarget)
    {
        attackTarget = _attackTarget;
    }

    public virtual bool CheckFollow()
    {
        if (attackTarget != null)
        {
            controller.ChangeState(Define.MonsterState.Follow);
            return true;
        }
        return false;
    }

    public virtual void Follow()
    {
        if (attackTarget == null)
        {
            controller.ChangeState(Define.MonsterState.Idle);
            return;
        }

        dir = (attackTarget.transform.position - controller.transform.position).normalized;
        Move(dir);
    }

    public virtual void FindAttackTarget()
    {
        controller.StartCoroutine(FindAttackTargetRoutine());
    }

    public virtual IEnumerator FindAttackTargetRoutine()
    {
        while (!controller.isDead)
        {
            //범위 안에 플레이어 캐릭터를 찾고
            tempColliders = Physics2D.OverlapCircleAll(controller.transform.position, data.findAttackTargetRange);

            //플레이어 캐릭터 먼저 찾기
            for (int i = 0; i < tempColliders.Length; i++)
            {
                tempPlayer = tempColliders[i].GetComponent<PlayerController>();
                if (tempPlayer != null && tempPlayer.currentState != Define.PlayerState.Die)
                {
                    SetAttackTarget(tempPlayer);
                    break;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public virtual bool CheckAttack()
    {
        if (attackTarget == null) return false;
        if(Vector2.Distance(controller.transform.position, attackTarget.transform.position) < data.attackTargetRange)
        {
            controller.ChangeState(Define.MonsterState.Attack);
            return true;
        }

        return false;
    }

    public virtual void Attack()
    {
        controller.StartCoroutine(AttackRoutione());
    }

    public virtual IEnumerator AttackRoutione()
    {
        tempVector = attackTarget.transform.position - controller.transform.position;
        if (tempVector.x >= 0)
            controller.ChangeDirection(Define.Direction.Left);
        if (tempVector.x <= 0)
            controller.ChangeDirection(Define.Direction.Right);
        Managers.Battle.AttackCalculation(controller, attackTarget);
        yield return new WaitForSeconds(1f);
        controller.ChangeState(Define.MonsterState.Follow);
    }

    public virtual bool CheckDie()
    {
        if (controller.status.currentNowHP <= 0.0)
        {
            controller.ChangeState(Define.MonsterState.Die);
            return true;
        }    

        return false;
    }

    public virtual void Die()
    {
        controller.isDead = true;
        controller.StopAllCoroutines();
        controller.StartCoroutine(DieRoutine());
    }

    public virtual IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(2);
        controller.coll.enabled = false;
        Stop();
        Managers.Object.ClearMonster(controller);
    }
}

namespace Monsters
{
    public class BaseMonster : Monster
    {
        public BaseMonster(MonsterData _data, MonsterController _controller)
        {
            data = _data;
            controller = _controller;
        }
    }
}

public class MonsterData
{
    public float findAttackTargetRange;
    public float attackTargetRange;
    public MonsterData()
    {
        findAttackTargetRange = 2.0f;
        attackTargetRange = 1.0f;
    }
}

