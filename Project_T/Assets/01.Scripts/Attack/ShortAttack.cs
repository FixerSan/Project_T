using System.Collections.Generic;
using UnityEngine;

public class ShortAttack : BaseAttack
{
    public PlayerController player;
    public Dictionary<int, Actor> actors = new Dictionary<int, Actor>();

    private SpriteRenderer sr;
    private Animator animator;

    private Vector3 dir;
    private float angle;

    public override void Init(PlayerController _player)
    {
        player = _player;
        sr = Util.FindChild<SpriteRenderer>(gameObject, "Sprite", true);
        animator = GetComponent<Animator>();
        attackCooltimer = 0;
        init = true;
    }

    public void Update()
    {
        if (!init) return;
        CheckStartAttack();
    }

    public void CheckStartAttack()
    {
        if (isAttacking) return;
        attackCooltimer -= Time.deltaTime;
        if (attackCooltimer <= 0)
        {
            StartAttack();
        }
    }

    public void StartAttack()
    {
        attackCooltimer = attackCooltime;
        sr.enabled = true;

        //가까운 적 바라보게 하기
        dir = (Managers.Game.stage.PlayerAttackTrans.position - transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

        animator.Play("Attack");
        isAttacking = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
        sr.enabled = false;
        Clear();
    }

    public void Attack(Actor _actor)
    {
        if (actors.ContainsKey(_actor.GetInstanceID())) return;
        actors.Add(_actor.GetInstanceID(), _actor);
        Managers.Battle.AttackCalculation(player, _actor, _damage:10, _knockBackForce: knockBackForce);
    }


    public void Clear()
    {
        actors.Clear();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttacking) return;
        if (collision.CompareTag("Enemy"))
            Attack(collision.GetComponent<Actor>());
    }
}
