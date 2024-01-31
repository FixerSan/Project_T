using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Vector3 positionOffset;

    public void Update()
    {
        transform.position = Managers.Object.PlayerController.transform.position + positionOffset;
    }

    public void AddAttack(Define.Attacks _attack, int _level, Action<BaseAttack> _callback)
    {
        BaseAttack attack = Managers.Resource.Instantiate($"{_attack}_{_level}").GetComponent<BaseAttack>();
        attack.transform.SetParent(transform);
        attack.transform.localPosition = Vector3.zero;
        attack.Init(Managers.Object.PlayerController);
        _callback.Invoke(attack);
    }
}
