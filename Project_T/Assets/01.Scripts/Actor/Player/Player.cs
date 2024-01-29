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
