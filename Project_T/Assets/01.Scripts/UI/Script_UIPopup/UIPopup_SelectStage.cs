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

        BindEvent(GetButton((int)Buttons.Button_Start).gameObject, OnClick_Start);
        BindEvent(GetButton((int)Buttons.Button_Close).gameObject, () => { ClosePopupUP(); });
        BindEvent(GetButton((int)Buttons.Button_RightArrow).gameObject, Managers.Game.main.SetNextStage);
        BindEvent(GetButton((int)Buttons.Button_LeftArrow).gameObject, Managers.Game.main.SetBeforeStage);

        RedrawUI();
        StartTweening(null);
        return true;
    }

    public override void RedrawUI()
    {
        base.RedrawUI();
        Managers.Resource.Load<Sprite>(Managers.Game.main.stageData.stageTitle, (_sprite) => { GetImage((int)Images.Image_StageImage).sprite = _sprite; });
        GetText((int)Texts.Text_Title).text = Managers.Game.main.stageData.stageTitle;
        GetText((int)Texts.Text_Stage).text = Managers.Game.main.stageData.stageLevel;
    }

    public override void ClosePopupUP(Action _callback = null)
    {
        base.ClosePopupUP(_callback);
        if (Managers.Object.LobbyCharacterController != null) { }
    }

    public void StartTweening(Action _callback)
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);
        sequence.Join(GetImage((int)Images.Image_StageImage).DOColor(Color.white, 1));
        sequence.Join(GetImage((int)Images.Image_StageImage).rectTransform.DOAnchorPos(Vector3.zero, 1));
        sequence.Join(GetText((int)Texts.Text_Stage).DOColor(Color.white, 1));
        sequence.Join(GetText((int)Texts.Text_Stage).rectTransform.DOAnchorPos(Vector3.zero, 1));
        sequence.Join(GetText((int)Texts.Text_Title).DOColor(Color.white, 1));
        sequence.Join(GetText((int)Texts.Text_Title).rectTransform.DOAnchorPos(Vector3.zero, 1));
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


    private enum Images
    {
        Image_StageImage
    }
}
