using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup_SelectLevelUpReward : UIPopup
{
    public UISlot_LevelUpReward[] slots;

    public override bool Init()
    {
        if(!base.Init()) return false;
        slots = Util.FindChild<Transform>(gameObject, "Trans_Slot").GetComponentsInChildren<UISlot_LevelUpReward>();

        return true;
    }

    public void RedrawUI(int[] _skillIndexes)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Redraw(_skillIndexes[i]);
        }
    }
}
