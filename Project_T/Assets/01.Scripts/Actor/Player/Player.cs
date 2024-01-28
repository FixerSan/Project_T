using UnityEngine;
using System.Collections;

public class Player
{
    public PlayerData data;
    public PlayerController controller;
    public MonsterController attackTarget;

    private Collider2D[] tempColliders;
    private MonsterController tempMonsterController;
    private Vector3 tempVector;

    private Vector3 dir;

    public Player(PlayerData _data, PlayerController _controller)
    {
        data = _data;
        controller = _controller;
    }

    public bool CheckMove()
    {
        if (Managers.Input.joystickInputValue != Vector2.zero)
        {
            controller.ChangeState(Define.PlayerState.Move);
            return true;
        }
        return false;
    }

    public bool CheckMove_Attack()
    {
        if (Managers.Input.joystickInputValue != Vector2.zero)
        {
            controller.StopCoroutine(controller.routines[nameof(AttackRoutine)]);
            if(controller.routines.ContainsKey(nameof(AttackRoutine)))
                controller.routines.Remove(nameof(AttackRoutine));
            controller.ChangeState(Define.PlayerState.Move);
            return true;
        }
        return false;
    }

    public bool CheckStop()
    {
        if(Managers.Input.joystickInputValue == Vector2.zero)
        {
            controller.ChangeState(Define.PlayerState.Idle);
            Stop();
            return true;
        }
        return false;
    }

    public void Move(Vector2 _moveDir)
    {
        if (_moveDir.x > 0) controller.ChangeDirection(Define.Direction.Left);
        if (_moveDir.x < 0) controller.ChangeDirection(Define.Direction.Right);
        _moveDir = _moveDir.normalized;

        controller.rb.velocity = _moveDir * controller.status.CurrentSpeed * 10 * Time.fixedDeltaTime;
    }

    public void Stop()
    {
        controller.rb.velocity = Vector2.zero;
    }

    public virtual bool CheckFollow()
    {
        if (attackTarget != null)
        {
            controller.ChangeState(Define.PlayerState.Follow);
            return true;
        }
        return false;
    }

    public virtual void Follow()
    {
        if (attackTarget == null)
        {
            controller.ChangeState(Define.PlayerState.Idle);
            return;
        }

        dir = (attackTarget.transform.position - controller.transform.position).normalized;
        Move(dir);
    }

    public void SetAttackTarget(MonsterController _attackTarget)
    {
        attackTarget = _attackTarget;
    }
    public virtual void FindAttackTarget()
    {
        attackTarget = null;

        //범위 안에 플레이어 캐릭터를 찾고
        tempColliders = Physics2D.OverlapCircleAll(controller.transform.position, data.findAttackTargetRange);

        for (int i = 0; i < tempColliders.Length; i++)
        {
            tempMonsterController = tempColliders[i].GetComponent<MonsterController>();
            if (tempMonsterController != null && tempMonsterController.currentState != Define.MonsterState.Die)
            {
                if (attackTarget == null)
                {
                    SetAttackTarget(tempMonsterController);
                    continue;
                }

                if (Vector2.Distance(tempMonsterController.transform.position, controller.transform.position) < Vector2.Distance(attackTarget.transform.position, controller.transform.position))
                    attackTarget = tempMonsterController;
            }
        }
    }
    public virtual void FindAttackTargetLoop()
    {
        controller.StartCoroutine(FindAttackTargetLoopRoutine());
    }

    public virtual IEnumerator FindAttackTargetLoopRoutine()
    {
        while (!controller.isDead)
        {
            FindAttackTarget();
            yield return new WaitForSeconds(0.5f);
        }
    }



    public virtual bool CheckAttack()
    {
        if (attackTarget == null) return false;
        if (Vector2.Distance(controller.transform.position, attackTarget.transform.position) < data.attackTargetRange)
        {
            controller.ChangeState(Define.PlayerState.Attack);
            return true;
        }

        return false;
    }

    public virtual void Attack()
    {
        if (attackTarget.currentState == Define.MonsterState.Die)
            FindAttackTarget();

        if (attackTarget == null)
            controller.ChangeState(Define.PlayerState.Idle);

        tempVector = attackTarget.transform.position - controller.transform.position;
        if (tempVector.x >= 0)
            controller.ChangeDirection(Define.Direction.Left);
        if(tempVector.x <= 0)
            controller.ChangeDirection(Define.Direction.Right);
        controller.routines.Add(nameof(AttackRoutine),controller.StartCoroutine(AttackRoutine()));
    }

    public virtual IEnumerator AttackRoutine()
    {
        Managers.Battle.AttackCalculation(controller, attackTarget);
        yield return new WaitForSeconds(1f);
        controller.ChangeState(Define.PlayerState.Idle);
        controller.routines.Remove(nameof(AttackRoutine));
    }
    public virtual bool CheckDie()
    {
        if (controller.status.currentNowHP <= 0.0)
        {
            controller.ChangeState(Define.PlayerState.Die);
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
        //Managers.Object.ClearMonster(controller);
    }
}

public class PlayerData
{
    public float findAttackTargetRange;
    public float attackTargetRange;
    public PlayerData()
    {
        findAttackTargetRange = 2.0f;
        attackTargetRange = 1.0f;
    }
}
