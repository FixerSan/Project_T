using DG.Tweening;
using System;
using UnityEngine;

public class UIPopup_SelectStage : UIPopup
{
    public override bool Init()
    {
        if (!base.Init()) return false;
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindRect(typeof(Rects));

        BindEvent(GetButton((int)Buttons.Button_Start).gameObject, OnClick_Start);
        BindEvent(GetButton((int)Buttons.Button_Close).gameObject, () => { ClosePopupUP(); });
        BindEvent(GetButton((int)Buttons.Button_RightArrow).gameObject, Managers.Game.main.SetNextStage);
        BindEvent(GetButton((int)Buttons.Button_LeftArrow).gameObject, Managers.Game.main.SetBeforeStage);

        RedrawUI();
        return true;
    }

    public override void RedrawUI()
    {
        base.RedrawUI();
        Managers.Resource.Load<Sprite>($"{Managers.Game.main.stageData.stageTitle}{Managers.Game.main.stageData.stageLevel}", (_sprite) => { GetImage((int)Images.Image_Stage).sprite = _sprite; });
        GetText((int)Texts.Text_Title).text = Managers.Game.main.stageData.stageTitle;
        GetText((int)Texts.Text_Stage).text = Managers.Game.main.stageData.stageLevel;
        StartTweening(null);
    }

    public override void ClosePopupUP(Action _callback = null)
    {
        base.ClosePopupUP(_callback);
        if (Managers.Object.LobbyCharacterController != null) { }
    }

    public void StartTweening(Action _callback)
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);
        sequence.OnStart(() => 
        {
            GetImage((int)Images.Image_Stage).color = Color.clear;
            GetImage((int)Images.Image_Stage).rectTransform.anchoredPosition = GetRect((int)Rects.Rect_Image_StageInitPos).anchoredPosition;
            GetText((int)Texts.Text_Stage).color = Color.clear;
            GetText((int)Texts.Text_Stage).rectTransform.anchoredPosition = GetRect((int)Rects.Rect_Text_StageInitPos).anchoredPosition;
            GetText((int)Texts.Text_Title).color = Color.clear;
            GetText((int)Texts.Text_Title).rectTransform.anchoredPosition = GetRect((int)Rects.Rect_Text_TitleInitPos).anchoredPosition;
        });
        sequence.Join(GetImage((int)Images.Image_Stage).DOColor(Color.white, 1));
        sequence.Join(GetImage((int)Images.Image_Stage).rectTransform.DOAnchorPos(GetRect((int)Rects.Rect_Image_StageTweeningPos).anchoredPosition, 1));
        sequence.Join(GetText((int)Texts.Text_Stage).DOColor(Color.white, 1));
        sequence.Join(GetText((int)Texts.Text_Stage).rectTransform.DOAnchorPos(GetRect((int)Rects.Rect_Text_StageTweeningPos).anchoredPosition, 1));
        sequence.Join(GetText((int)Texts.Text_Title).DOColor(Color.white, 1));
        sequence.Join(GetText((int)Texts.Text_Title).rectTransform.DOAnchorPos(GetRect((int)Rects.Rect_Text_TitleTweeningPos).anchoredPosition, 1));
        sequence.onComplete += () => { _callback?.Invoke(); };
    }


    public void OnClick_Start()
    {
        Managers.Game.main.StartStage();
    }

    private enum Texts
    {
        Text_Title,
        Text_Stage
    }

    private enum Buttons
    {
        Button_RightArrow, 
        Button_LeftArrow,
        Button_Start,
        Button_Close
    }

    private enum Rects
    {
        Rect_Image_StageTweeningPos,
        Rect_Image_StageInitPos,
        Rect_Text_TitleTweeningPos,
        Rect_Text_TitleInitPos,
        Rect_Text_StageTweeningPos,
        Rect_Text_StageInitPos
    }

    private enum Images
    {
        Image_Stage
    }
}
