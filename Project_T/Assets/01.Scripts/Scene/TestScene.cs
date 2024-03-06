using System;
using UnityEngine;

public class TestScene : BaseScene
{
    Vector3 pos = Vector3.zero;
    public int nowPattern = 1;
    private int tempInt = 0;

    public override void Init(Action _callback)
    {
        Managers.Game.StartStage();
        Managers.UI.ShowSceneUI<UIScene_Stage>();
        Managers.Screen.CameraController.SetTarget(Managers.Object.SpawnPlayer(Managers.Game.main.nowHeroIndex,Vector3.zero).transform);
        Managers.Object.SpawnBoomController(new Vector3(0, 5, 0));
        Managers.Object.SpawnMagnetController(new Vector3(0, 10, 0));
        _callback?.Invoke();
    }

    private void FixedUpdate()
    {
        if (Managers.Object.monsters.Count >= 100) return;
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        if (nowPattern == 1)
        {
            pos.x = UnityEngine.Random.Range(-10f, 10f);
            pos.y = UnityEngine.Random.Range(-10f, 10f);
            if (pos == Vector3.zero) return;
            pos = pos.normalized;
            pos *= 7.5f;
            Managers.Object.SpawnMonster(0, Managers.Object.PlayerController.transform.position + pos);
        }

        if (nowPattern == 2)
        {
            pos.x = UnityEngine.Random.Range(-10f, 10f);
            pos.y = UnityEngine.Random.Range(-10f, 10f);
            if (pos == Vector3.zero) return;
            pos = pos.normalized;
            pos *= 7.5f;
            tempInt = UnityEngine.Random.Range(0, 2);
            Managers.Object.SpawnMonster(1, Managers.Object.PlayerController.transform.position + pos);
        }

        if (nowPattern == 3)
        {
            pos.x = UnityEngine.Random.Range(-10f, 10f);
            pos.y = UnityEngine.Random.Range(-10f, 10f);
            if (pos == Vector3.zero) return;
            pos = pos.normalized;
            pos *= 7.5f;
            tempInt = UnityEngine.Random.Range(0, 2);
            Managers.Object.SpawnMonster(tempInt, Managers.Object.PlayerController.transform.position + pos);
        }
    }

    public void NextPattern()
    {
        nowPattern++;
    }

    public override void Clear()
    {

    }
}
