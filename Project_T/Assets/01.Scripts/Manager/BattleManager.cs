using System;
using UnityEngine;

public class BattleManager
{
    ////���� ó��
    public void AttackCalculation(Actor _attacker, Actor _hiter, float _damage = -1, float _knockBackForce = 0, Action<float> _damageCallback = null)
    {
        Vector3 _knockBackDir = (_attacker.transform.position - _hiter.transform.position).normalized * (-1f);
        float currentDamage = _damage;
        if (_damage == -1)
            currentDamage = _attacker.status.CurrentAttackForce;

        //ġ��Ÿ���� üũ �� ������ ġ��Ÿ ó��
        float tempInt = UnityEngine.Random.Range(0, 100);
        if (tempInt < _attacker.status.CurrentCriticalProbability)
        {
            currentDamage = (int)(currentDamage * _attacker.status.CurrentCriticalForce);
            //if (_attacker is RangerController)
            //{
            //    RangerController ranger = _attacker as RangerController;
            //    Debug.Log($"{ranger.data.name} :: ġ��Ÿ ������ :: {currentDamage}");
            //}
        }


        //���� ���
        currentDamage = currentDamage * (100 / (_hiter.status.CurrentDefenseForce + 100));



        //������ ó�� �� �ݹ�
        _hiter.Hit(currentDamage, _knockBackDir, _knockBackForce);
        _damageCallback?.Invoke(currentDamage);
    }
}
