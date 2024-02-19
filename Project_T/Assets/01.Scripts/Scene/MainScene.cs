using System;

public class MainScene : BaseScene
{
    public override void Init(Action _callback)
    {
        Managers.Object.SpawnLobbyCharacterController();
        Managers.UI.ShowSceneUI<UIScene_Main>();
        _callback?.Invoke();
    }

    public override void Clear()
    {

    }

}
