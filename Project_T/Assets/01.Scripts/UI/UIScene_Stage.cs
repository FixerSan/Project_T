using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScene_Stage : UIScene
{
    public Slider levelSlider;
    public override bool Init()
    {
        if(!base.Init()) return false;
        levelSlider = Util.FindChild<Slider>(gameObject, "Slider_LevelGauge");
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetText((int)Texts.Text_Level).text = Managers.Game.stage.currentPlayerLevel.ToString();
        levelSlider.value = 0;
        return true;
    }

    public override void RedrawUI()
    {
        GetText((int)Texts.Text_Level).text = Managers.Game.stage.currentPlayerLevel.ToString();
        levelSlider.value = Managers.Game.stage.currentEXP / Managers.Game.stage.needEXP;
    }

    private enum Texts
    {
        Text_Level
    }

    private enum Buttons
    {

    }
}
