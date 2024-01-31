using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : BaseAttack
{
    private PlayerController player;

    public float attackDuration;
    private float attackDurationTimer;

    public float rotationForce;

    private Vector3 tempVecter = Vector3.zero;
    private Vector3 firstSpriteScale;
    private SpriteRenderer[] srs;
    
    public override void Init(PlayerController _player)
    {
        player = _player;
        attackCooltimer = 0;
        attackDurationTimer = attackDuration;
        srs = Util.FindChild<Transform>(gameObject, "Sprite").GetComponentsInChildren<SpriteRenderer>();
        firstSpriteScale = srs[0].transform.localScale;
        for (int i = 0; i < srs.Length; i++)
            srs[i].transform.localScale = Vector3.zero;
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
        for (int i = 0; i < srs.Length; i++)
            srs[i].transform.DOScale(firstSpriteScale, 1);
        StartCoroutine(AttackRoutine());
        isAttacking = true;
    }

    public IEnumerator AttackRoutine()
    {
        while (attackDurationTimer > 0)
        {
            yield return null;
            attackDurationTimer -= Time.deltaTime;
            tempVecter.z += Time.deltaTime * rotationForce;
            transform.eulerAngles = tempVecter;
        }

        EndAttack();
    }

    public void EndAttack()
    {
        attackDurationTimer = attackDuration;
        isAttacking = false;

        for (int i = 0; i < srs.Length; i++)
            srs[i].transform.DOScale(Vector3.zero, 1).onComplete += () => { transform.eulerAngles = Vector3.zero; };
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttacking) return;
        Debug.Log(collision.name);
        if (collision.CompareTag("Enemy"))
            Attack(collision.GetComponent<Actor>());
    }
    public void Attack(Actor _actor)
    {
        Managers.Battle.AttackCalculation(player, _actor,_damage:1, _knockBackForce: knockBackForce);
    }
}
