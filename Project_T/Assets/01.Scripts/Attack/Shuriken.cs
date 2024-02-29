using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public PlayerController player;
    public float rotationForce;
    public float moveForce;
    private bool init = false;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;

    private float activeTimer = 0;

    public void Init(PlayerController _player, Vector2 _direction)
    {
        player = _player;
        direction = _direction;
        init = true;
        rb = GetComponent<Rigidbody2D>();
        activeTimer = 0;
    }

    public void Update()
    {
        if (!init) return;
        MoveToDirection();
        CheckDestroy();
    }

    public void MoveToDirection()
    {
        rb.MovePosition((Vector2)transform.position + direction * Time.deltaTime * moveForce);
        transform.eulerAngles += Vector3.back * rotationForce * Time.deltaTime;
    }

    public void CheckDestroy()
    {
        activeTimer += Time.deltaTime;
        if (activeTimer >= 10)
            Managers.Resource.Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Attack(collision.GetComponent<Actor>());
            Managers.Resource.Destroy(gameObject);
        }
    }

    public void Attack(Actor _actor)
    {
        Managers.Battle.AttackCalculation(player, _actor, _damage: 100);
    }

    public void OnDisable()
    {
        player = null;
        direction = Vector2.zero;
        init = false;
        activeTimer = 0;
    }
}
