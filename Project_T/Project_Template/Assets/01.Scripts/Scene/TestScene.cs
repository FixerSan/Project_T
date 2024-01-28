using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    public override void Init(Action _callback)
    {
        Managers.UI.ShowSceneUI<Test_UIScene_Main>();
        _callback?.Invoke();
    }

    public override void Clear()
    {

    }
}
