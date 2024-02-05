using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : BaseItemController
{
    public override void Interaction()
    {
        Managers.Game.stage.GetItem_Boom();
        Managers.Object.ClearBoomController(this);
    }
}
