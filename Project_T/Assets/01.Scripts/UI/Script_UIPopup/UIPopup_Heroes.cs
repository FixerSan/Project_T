using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup_Heroes : UIPopup
{
    private UISlot_Hero[] slots;

    public override bool Init()
    {
        if(!base.Init()) return false;
        Transform slotTrans = Util.FindChild<Transform>(gameObject, "Trans_Slots"); 
        slots = slotTrans.GetComponentsInChildren<UISlot_Hero>();
        BindButton(typeof(Buttons));


        BindEvent(GetButton((int)Buttons.Button_Select).gameObject, Managers.Game.main.ChangeHero);
        FirstDraw();
        return true;
    }

    public void FirstDraw()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Define.Hero)).Length; i++)
            slots[i].Redraw(i, i == Managers.Game.main.nowHeroIndex);
    }

    public override void RedrawUI()
    {
        base.RedrawUI();
        for (int i = 0; i < Enum.GetValues(typeof(Define.Hero)).Length; i++)
            slots[i].Redraw(i, i == Managers.Game.main.selectedHeroIndex);
    }

    private enum Buttons
    {
        Button_Select
    }
}
