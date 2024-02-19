using DG.Tweening;
using System;
using UnityEngine;

public class UIPopup_SelectLevelUpReward : UIPopup
{
    public UISlot_LevelUpReward[] slots;

    public override bool Init()
    {
        if (!base.Init()) return false;
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
        DOTween.timeScale = 1f;
        Sequence startSequence = DOTween.Sequence();
        startSequence.SetUpdate(true);
        startSequence.Join(GetImage((int)Images.Image_BackGround).DOColor(new Color(0, 0, 0, 0.5f), 1f)).SetEase(Ease.Linear);
        startSequence.Join(slots[0].rect.DOAnchorPosY(GetRect((int)Rects.Trans_StartTweeningPos_SlotOne).anchoredPosition.y, 1f));
        startSequence.Join(slots[1].rect.DOAnchorPosY(GetRect((int)Rects.Trans_StartTweeningPos_SlotTwo).anchoredPosition.y, 1f));
        startSequence.Join(slots[2].rect.DOAnchorPosY(GetRect((int)Rects.Trans_StartTweeningPos_SlotThree).anchoredPosition.y, 1f));
        startSequence.AppendCallback(() =>
        {
            _callback?.Invoke();
            startSequence.Kill();
        });
    }

    public void EndTweening(Action _callback)
    {
        DOTween.timeScale = 1f;
        Sequence endSequence = DOTween.Sequence();
        endSequence.SetUpdate(true);
        endSequence.Join(GetImage((int)Images.Image_BackGround).DOColor(Color.clear, 1f));
        endSequence.Join(slots[0].rect.DOAnchorPosY(GetRect((int)Rects.Trans_EndTweeningPos_SlotOne).anchoredPosition.y, 1f));
        endSequence.Join(slots[1].rect.DOAnchorPosY(GetRect((int)Rects.Trans_EndTweeningPos_SlotTwo).anchoredPosition.y, 1f));
        endSequence.Join(slots[2].rect.DOAnchorPosY(GetRect((int)Rects.Trans_EndTweeningPos_SlotThree).anchoredPosition.y, 1f));
        endSequence.AppendCallback(() =>
        {
            _callback?.Invoke();
            endSequence.Kill();
        });
    }

    public override void ClosePopupUP(Action _callback)
    {
        EndTweening(() =>
        {
            base.ClosePopupUP(_callback);
        });
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
