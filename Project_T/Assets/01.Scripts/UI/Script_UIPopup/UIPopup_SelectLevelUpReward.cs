using DG.Tweening;
using System;
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
        BindImage(typeof(Images));
        BindRect(typeof(Rects));
        return true;
    }

    public void RedrawUI(int[] _skillIndexes)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Redraw(_skillIndexes[i]);
        }
        StartTweening();
    }


    public void StartTweening(Action _callback = null)
    {
        Sequence startSequence = DOTween.Sequence();
        startSequence.Join(GetImage((int)Images.Image_BackGround).DOColor(new Color(0, 0, 0, 1), 1f)).SetEase(Ease.Linear);
        startSequence.Join(slots[0].rect.DOAnchorPos(GetRect((int)Rects.Trans_StartTweeningPos_SlotOne).anchoredPosition, 1)).SetEase(Ease.Linear);
        startSequence.Join(slots[1].rect.DOAnchorPos(GetRect((int)Rects.Trans_StartTweeningPos_SlotTwo).anchoredPosition, 1)).SetEase(Ease.Linear);
        startSequence.Join(slots[2].rect.DOAnchorPos(GetRect((int)Rects.Trans_StartTweeningPos_SlotThree).anchoredPosition, 1)).SetEase(Ease.Linear);
        startSequence.onComplete += () =>
        {
            _callback.Invoke();
            startSequence.Kill();
        };
        startSequence.Play();
    }

    public void EndTweening(Action _callback)
    {
        Sequence endSequence = DOTween.Sequence();
        endSequence.Join(GetImage((int)Images.Image_BackGround).DOColor(Color.clear, 1f));
        endSequence.Join(slots[0].rect.DOAnchorPosY(GetRect((int)Rects.Trans_EndTweeningPos_SlotOne).anchoredPosition.y, 1));
        endSequence.Join(slots[1].rect.DOAnchorPosY(GetRect((int)Rects.Trans_EndTweeningPos_SlotTwo).anchoredPosition.y, 1));
        endSequence.Join(slots[2].rect.DOAnchorPosY(GetRect((int)Rects.Trans_EndTweeningPos_SlotThree).anchoredPosition.y, 1));
        endSequence.onComplete += () => 
        {
            _callback.Invoke();
            endSequence.Kill();
        };
        endSequence.Play();
    }
    public override void ClosePopupUP()
    {
        EndTweening(base.ClosePopupUP);
    }

    private enum Images
    {
        Image_BackGround
    }

    private enum Rects
    {
        Trans_StartTweeningPos_SlotOne,
        Trans_StartTweeningPos_SlotTwo,
        Trans_StartTweeningPos_SlotThree,
        Trans_EndTweeningPos_SlotOne,
        Trans_EndTweeningPos_SlotTwo,
        Trans_EndTweeningPos_SlotThree
    }
}
