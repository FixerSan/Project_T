using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Interaction();
    }

    public abstract void Interaction();
}
