using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    ////공격 처리
    public void AttackCalculation(Actor _attacker, Actor _hiter, float _damage = -1, Action<float> _damageCallback = null)
    {
        float currentDamage = _damage;
        if (_damage == -1)
            currentDamage = _attacker.status.CurrentAttackForce;

        //치명타인지 체크 후 맞으면 치명타 처리
        float tempInt = UnityEngine.Random.Range(0, 100);
        if (tempInt < _attacker.status.CurrentCriticalProbability)
        {
            currentDamage = (int)(currentDamage * _attacker.status.CurrentCriticalForce);
            //if (_attacker is RangerController)
            //{
            //    RangerController ranger = _attacker as RangerController;
            //    Debug.Log($"{ranger.data.name} :: 치명타 데메지 :: {currentDamage}");
            //}
        }


        //방어력 계산
        currentDamage = currentDamage * (100 / (_hiter.status.CurrentDefenseForce + 100));

        //데미지 처리 및 콜백
        _hiter.Hit(currentDamage);
        _damageCallback?.Invoke(currentDamage);
    }
}
