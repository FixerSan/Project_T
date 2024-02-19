using System;

public class UIPopup : UIBase
{
    public override bool Init()
    {
        if (!base.Init())
            return false;

        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    public virtual void ClosePopupUP(Action _callback = null)
    {
        Managers.UI.ClosePopupUI(this);
        _callback?.Invoke();
    }

    protected virtual void Update()
    {

    }

    private void OnDisable()
    {

    }

    public virtual void RedrawUI()
    {

    }
}
