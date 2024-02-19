public class MagnetController : BaseItemController
{
    public override void Interaction()
    {
        for (int i = 0; i < Managers.Object.exps.Count; i++)
        {
            Managers.Object.exps[i].Get(Managers.Object.PlayerController.transform);
            Managers.Object.ClearMagnetController(this);
        }
    }
}
