using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : BaseAttack
{
    public PlayerController player;
    public override void Init(PlayerController _player)
    {
        player = _player;
        init = true;
    }

    public void Update()
    {
        if (!init) return;
        CheckStartAttack();
    }

    public override void StartAttack()
    {
        attackCooltimer = attackCooltime;
        StartCoroutine(AttackRoutine());
    }

    public IEnumerator AttackRoutine()
    {
        for (int i = 0; i < level; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Shuriken shuriken = Managers.Resource.Instantiate(Define.Attacks.Shuriken.ToString(), _parent: Managers.Object.PlayerAttackController.transform, _pooling: true).GetOrAddComponent<Shuriken>();
            shuriken.transform.SetPositionAndRotation(player.transform.position, Quaternion.identity);
            shuriken.Init(player, (Managers.Game.stage.PlayerAttackTrans.transform.position - Managers.Object.PlayerController.transform.position).normalized);
        }
    }
}
