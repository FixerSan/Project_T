using UnityEngine;

public class UISlot_LevelUpReward : UIBase
{
    public SkillData data;
    public RectTransform rect;
    public override bool Init()
    {
        if (!base.Init()) return false;
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindEvent(gameObject, SelectReward);
        rect = GetComponent<RectTransform>();
        return true;
    }

    public void Redraw(int _attackIndex)
    {
        data = Managers.Data.GetAttackData(_attackIndex);
        GetText((int)Texts.Text_Title).text = data.name;
        GetText((int)Texts.Text_Description).text = data.description;
        Managers.Resource.Load<Sprite>(data.name, (_sprite) => { GetImage((int)Images.Image_Icon).sprite = _sprite; });

        Managers.Resource.Load<Sprite>("DisableStar", (_sprite) => { GetImage((int)Images.Image_StarOne).sprite = _sprite; });
        Managers.Resource.Load<Sprite>("DisableStar", (_sprite) => { GetImage((int)Images.Image_StarTwo).sprite = _sprite; });
        Managers.Resource.Load<Sprite>("DisableStar", (_sprite) => { GetImage((int)Images.Image_StarThree).sprite = _sprite; });
        Managers.Resource.Load<Sprite>("DisableStar", (_sprite) => { GetImage((int)Images.Image_StarFour).sprite = _sprite; });
        Managers.Resource.Load<Sprite>("DisableStar", (_sprite) => { GetImage((int)Images.Image_StarFive).sprite = _sprite; });

        int nowAttackLevel = 0;
        foreach (var attack in Managers.Game.stage.attacks)
        {
            if (attack.Key == data.attackType)
                nowAttackLevel = attack.Value.level;
        }

        switch (nowAttackLevel)
        {
            case 0:
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarOne).sprite = _sprite; });
                break;
            case 1:
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarOne).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarTwo).sprite = _sprite; });
                break;
            case 2:
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarOne).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarTwo).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarThree).sprite = _sprite; });
                break;
            case 3:
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarOne).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarTwo).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarThree).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarFour).sprite = _sprite; });
                break;
            case 4:
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarOne).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarTwo).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarThree).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarFour).sprite = _sprite; });
                Managers.Resource.Load<Sprite>("EnableStar", (_sprite) => { GetImage((int)Images.Image_StarFive).sprite = _sprite; });
                break;
        }
    }

    public void SelectReward()
    {
        Managers.Game.stage.SelectLevelUpReward(data.index);
    }

    private enum Texts
    {
        Text_Title,
        Text_Description
    }

    private enum Images
    {
        Image_StarOne,
        Image_StarTwo,
        Image_StarThree,
        Image_StarFour,
        Image_StarFive,
        Image_Icon
    }
}
