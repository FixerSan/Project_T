public class UIScene : UIBase
{
    public override bool Init()
    {
        if (!base.Init()) return false;
        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    public virtual void RedrawUI()
    {

    }
}
