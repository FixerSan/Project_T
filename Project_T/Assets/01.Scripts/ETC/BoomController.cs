public class BoomController : BaseItemController
{
    public override void Interaction()
    {
        Managers.Game.stage.GetItem_Boom();
        Managers.Object.ClearBoomController(this);
    }
}
