using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot_Hero : UIBase
{
    public HeroData data;

    public Vector2 selectTweeingPos = Vector2.zero;
    public Vector2 deSelectTweeingPos = Vector2.zero;

    public override bool Init()
    {
        if (!base.Init()) return false;
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindEvent(gameObject, OnClick);

        deSelectTweeingPos = rect.anchoredPosition;
        selectTweeingPos = deSelectTweeingPos + new Vector2(0, 25f);

        GetImage((int)Images.Image_Selected).gameObject.SetActive(false);
        DeSelectTweeingPos();
        return true;
    }

    public void Redraw(int _index , bool isSelected)
    {
        if (!init)
            Init();
        DeSelectTweeingPos();
        GetImage((int)Images.Image_Selected).gameObject.SetActive(false);

        data = Managers.Data.GetHeroData(_index);
        Managers.Resource.Load<Sprite>(data.attack.ToString(), (_sprite) => { GetImage((int)Images.Image_Icon).sprite = _sprite; });
        //Managers.Resource.Load<Sprite>(data.name, (_sprite) => { GetImage((int)Images.Image_Illust).sprite = _sprite; });
        GetText((int)Texts.Text_HeroName).text = data.name;
        if (isSelected) 
        {
            GetImage((int)Images.Image_Selected).gameObject.SetActive(true);
            SelectTweeing();
        }
    }

    public void OnClick()
    {
        Managers.Game.main.SelectHero(data.index);
    }

    public void SelectTweeing()
    {
        rect.DOAnchorPos(selectTweeingPos, 0.5f).SetUpdate(true);
    }

    public void DeSelectTweeingPos()
    {
        rect.DOAnchorPos(deSelectTweeingPos, 0.5f).SetUpdate(true);
    }

    private enum Images
    {
        Image_Illust, Image_Icon, Image_Selected
    }

    private enum Texts
    {
        Text_HeroName
    }

}
