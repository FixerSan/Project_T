using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public int level;
    protected bool init = true;
    protected bool isAttacking = false;

    public float attackCooltime;
    protected float attackCooltimer;
    public float knockBackForce;

    public abstract void Init(PlayerController _player);
}
