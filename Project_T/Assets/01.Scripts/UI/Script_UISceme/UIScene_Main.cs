public class UIScene_Main : UIScene
{
    public override bool Init()
    {
        if (!base.Init()) return false;
        BindButton(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Button_Start).gameObject, OnClick_Start);
        BindEvent(GetButton((int)Buttons.Button_Heroes).gameObject, OnClick_Heroes);

        return true;
    }

    private void OnClick_Start()
    {
        Managers.Game.main.OpenStageList();
    }

    private void OnClick_Heroes()
    {
        Managers.UI.ShowPopupUI<UIPopup_Heroes>();
    }

    private enum Buttons
    {
        Button_Start, Button_Heroes
    }
}
