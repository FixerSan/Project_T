using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : class
{
    public abstract void Enter(T _entity);
    public abstract void Update(T _entity);
    public abstract void Exit(T _entity);
}
