using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public Status status;
    public Dictionary<string, Coroutine> routines = new Dictionary<string, Coroutine>();
    protected Vector3 tempVecter = Vector3.zero;

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public abstract void Hit(float _damage);
    public abstract void GetDamage(float _damage);


    public void ChangeDirection(Define.Direction _direction)
    {
        if (_direction == Define.Direction.Left) tempVecter.y = 180;
        if (_direction == Define.Direction.Right) tempVecter.y = 0;

        transform.eulerAngles = tempVecter;
    }
}
