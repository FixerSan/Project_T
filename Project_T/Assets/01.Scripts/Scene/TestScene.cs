using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    Vector3 pos = Vector3.zero;
    public override void Init(Action _callback)
    {
        Managers.UI.ShowSceneUI<UIScene_Test>();
        Managers.Screen.CameraController.SetTarget(Managers.Object.SpawnPlayer(Vector3.zero).transform);


        _callback?.Invoke();
    }

    private void FixedUpdate()
    {
        if (Managers.Object.monsters.Count >= 10) return;
        pos.x = UnityEngine.Random.Range(-10, 10);
        pos.y = UnityEngine.Random.Range(-10, 10);
        pos = pos.normalized;
        pos *= 10;
        Managers.Object.SpawnMonster(0, pos);
    }

    public override void Clear()
    {

    }
}
