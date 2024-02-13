using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemController : MonoBehaviour
{
    private bool isNotGet = true;
    private Vector3 dir;
    private Rigidbody2D rb;
    private float firstTweeningForce = 3;
    private float tweeningForce = 1;
    private float getTweeningDelay = 0.25f;
    private float maxSpeed = 30;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (isNotGet)
            Get(collision.transform);

        else
            Interaction();
    }

    public void Get(Transform _collision)
    {
        isNotGet = false;
        GetTweening(_collision.transform);
    }

    public abstract void Interaction();

    public void GetTweening(Transform _collision)
    {
        dir = (transform.position - _collision.transform.position).normalized;
        rb.AddForce(dir * firstTweeningForce, ForceMode2D.Impulse);

        StartCoroutine(GetTweeningRoutine(_collision));
    }

    public IEnumerator GetTweeningRoutine(Transform _collision)
    {
        yield return new WaitForSeconds(getTweeningDelay);
        while(true)
        {
            yield return null;
            if (rb.velocity.magnitude >= maxSpeed) continue;
            dir = (_collision.transform.position - transform.position).normalized;
            rb.AddForce(dir * tweeningForce , ForceMode2D.Impulse);
        }
    }
}
