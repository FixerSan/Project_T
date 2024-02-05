using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestScene : BaseScene
{
    Vector3 pos = Vector3.zero;
    public SortingGroup sg;
    public override void Init(Action _callback)
    {
        Managers.Game.StartStage();
        Managers.UI.ShowSceneUI<UIScene_Stage>();
        Managers.Screen.CameraController.SetTarget(Managers.Object.SpawnPlayer(Vector3.zero).transform);
        Managers.Object.SpawnBoomController(new Vector3(0, 5, 0));
        _callback?.Invoke();
    }

    private void FixedUpdate()
    {
        if (Managers.Object.monsters.Count >= 100) return;
        pos.x = UnityEngine.Random.Range(-10, 10);
        pos.y = UnityEngine.Random.Range(-10, 10);
        pos = pos.normalized;
        pos *= 10;
        Managers.Object.SpawnMonster(0, Managers.Object.PlayerController.transform.position + pos);
    }

    public override void Clear()
    {

    }
}
