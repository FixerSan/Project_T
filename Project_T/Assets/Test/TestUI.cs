public class TestUI : TestUIBase
{
    public override bool Init()
    {
        if (!base.Init()) return false;
        BindText(typeof(Texts));

        GetText(nameof(Texts.Text_Title)).text = "�ٺ�";
        return true;
    }

    private enum Texts
    {
        Text_Title,
    }
}
