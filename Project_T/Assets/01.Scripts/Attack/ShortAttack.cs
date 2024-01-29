using System.Collections.Generic;
using UnityEngine;

public class ShortAttack : BaseAttack
{
    public PlayerController player;
    public Dictionary<int, Actor> actors = new Dictionary<int, Actor>();

    private SpriteRenderer sr;
    private Animator animator;

    public float attackTime;
    private float attackTimer;

    private bool init = false;
    private bool isAttacking = false;
    public override void Init(PlayerController _player)
    {
        player = _player;
        sr = Util.FindChild<SpriteRenderer>(gameObject, "Sprite", true);
        animator = GetComponent<Animator>();
        attackTimer = 0;
        init = true;
    }

    public void Update()
    {
        if (!init) return;
        CheckAttack();

    }

    public void CheckAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            StartAttack();
        }
    }

    public void StartAttack()
    {
        attackTimer = attackTime;
        sr.enabled = true;
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
        Managers.Battle.AttackCalculation(player, _actor);
    }


    public void Clear()
    {
        actors.Clear();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttacking) return;
        Debug.Log(collision.name);
        if (collision.CompareTag("Enemy"))
            Attack(collision.GetComponent<Actor>());
    }
}
