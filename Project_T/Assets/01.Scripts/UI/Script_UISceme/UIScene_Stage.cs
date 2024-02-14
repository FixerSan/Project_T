using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class UIScene_Stage : UIScene
{
    public Slider levelSlider;
    private float tempFloat;
    private int min;
    private int second;



    public override bool Init()
    {
        if(!base.Init()) return false;
        BindButton(typeof(Buttons));
        BindSlider(typeof(Sliders));
        BindText(typeof(Texts));

        GetText((int)Texts.Text_Level).text = Managers.Game.stage.currentPlayerLevel.ToString();
        GetText((int)Texts.Text_Time).text = "0m 0s";
        GetSlider((int)Sliders.Slider_LevelGauge).value = 0;
        return true;
    }

    public override void RedrawUI()
    {
        GetText((int)Texts.Text_Level).text = Managers.Game.stage.currentPlayerLevel.ToString();
        GetSlider((int)Sliders.Slider_LevelGauge).value = Managers.Game.stage.currentEXP / Managers.Game.stage.needEXP;

        tempFloat = Managers.Game.stage.time / 60;
        min = Mathf.FloorToInt(tempFloat);
        tempFloat -= min;
        tempFloat *= 60;
        second = Mathf.FloorToInt(tempFloat);


        GetText((int)Texts.Text_Time).text = $"{min}m {second}s";

        if (Managers.Game.stage.currentStagePattern == 1)
        {
            GetSlider((int)Sliders.Slider_Time).value = Managers.Game.stage.time / 120f;
        }

        if (Managers.Game.stage.currentStagePattern == 2)
        {
            GetSlider((int)Sliders.Slider_Time).value = (Managers.Game.stage.time - 120) / 120f;
        }

        if (Managers.Game.stage.currentStagePattern == 3)
        {
            GetSlider((int)Sliders.Slider_Time).value = (Managers.Game.stage.time - 240) / 120f;
        }
    }

    private enum Texts
    {
        Text_Level, Text_Time
    }

    private enum Buttons
    {

    }

    private enum Sliders
    {
        Slider_LevelGauge, Slider_Time
    }
}
