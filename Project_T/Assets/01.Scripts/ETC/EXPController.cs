using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPController : BaseItemController
{
    public SpriteRenderer sr;
    public int level;
    public float exp;

    public void Init(int _level, float _exp)
    {
        level = _level;
        exp = _exp;
        sr = Util.FindChild<SpriteRenderer>(gameObject);

        Managers.Resource.Load<Sprite>($"EXP_{level}", (_sprite) => { sr.sprite = _sprite; });
    }

    public override void Interaction()
    {
        Managers.Game.stage.GetEXP(exp);
        Managers.Object.ClearEXPController(this);
    }
}
