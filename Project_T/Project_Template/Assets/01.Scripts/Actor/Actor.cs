using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public Status status;

    public abstract void Init();
}

public class Status
{
    public float defaultHP;
    public float plusHP;

    public float CurrentHP { get { return defaultHP + plusHP; } }
}
