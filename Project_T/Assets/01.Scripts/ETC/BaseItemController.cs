using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemController : MonoBehaviour
{
    private bool isNotGet = true;
    private Vector3 dir;
    private Rigidbody2D rb;
    public float firstTweeningForce = 3;
    public float tweeningForce = 1;
    public float getTweeningDelay = 0.25f;

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
            dir = (_collision.transform.position - transform.position).normalized;
            rb.AddForce(dir * tweeningForce , ForceMode2D.Impulse);
        }
    }
}
