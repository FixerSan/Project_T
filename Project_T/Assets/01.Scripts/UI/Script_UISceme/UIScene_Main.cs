public class UIScene_Main : UIScene
{
    public override bool Init()
    {
        if (!base.Init()) return false;
        BindButton(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Button_Start).gameObject, OnClick_Start);


        return true;
    }

    private void OnClick_Start()
    {
        Managers.Game.main.OpenStageList();
    }

    private enum Buttons
    {
        Button_Start
    }
}
