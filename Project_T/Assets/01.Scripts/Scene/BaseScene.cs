using System;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{

    public abstract void Init(Action _callback);
    public abstract void Clear();
}
