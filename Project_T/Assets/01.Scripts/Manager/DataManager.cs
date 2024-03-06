using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, UserData> userDatas = new Dictionary<int, UserData>();
    public Dictionary<int, StageLevelData> stageLevelDatas = new Dictionary<int, StageLevelData>();
    public Dictionary<int, SkillData> attackDatas = new Dictionary<int, SkillData>();
    public Dictionary<int, StageData> stageDatas = new Dictionary<int, StageData>();
    public Dictionary<int, HeroData> heroDatas = new Dictionary<int, HeroData>();

    public UserData GetUserData(int _index)
    {
        if (userDatas.TryGetValue(_index, out UserData _data)) return _data;
        return null;
    }

    public StageLevelData GetStageLevelData(int _index)
    {
        if (stageLevelDatas.TryGetValue(_index, out StageLevelData _data)) return _data;
        return null;
    }
    public SkillData GetAttackData(int _index)
    {
        if (attackDatas.TryGetValue(_index, out SkillData _data)) return _data;
        return null;
    }
    
    public StageData GetStageData(int _index)
    {
        if (stageDatas.TryGetValue(_index, out StageData _data)) return _data;
        return null;
    }

    public HeroData GetHeroData(int _index)
    {
        if (heroDatas.TryGetValue(_index, out HeroData _data)) return _data;
        return null;
    }

    public void LoadPreData(Action _callback)
    {
        //LoadUserData();
        LoadAttackData();
        LoadStageLevelData();
        LoadStageData();
        LoadHeroData();
        _callback?.Invoke();
    }

    public void LoadUserData()
    {
        TextAsset text = Managers.Resource.Load<TextAsset>("Data_User");
        UserDatas datas = JsonUtility.FromJson<UserDatas>(text.text);
        for (int i = 0; i < datas.datas.Length; i++)
            userDatas.Add(datas.datas[i].ID, datas.datas[i]);
    }

    public void LoadStageLevelData()
    {
        Managers.Resource.Load<GameObject>("Datas", (_) =>
        {
            Datas datas = _.GetComponent<Datas>();
            for (int i = 0; i < datas.level.datas.Count; i++)
                stageLevelDatas.Add(datas.level.datas[i].level, datas.level.datas[i]);
        });
    }

    public void LoadStageData()
    {
        Managers.Resource.Load<GameObject>("Datas", (_) =>
        {
            Datas datas = _.GetComponent<Datas>();
            for (int i = 0; i < datas.stage.datas.Length; i++)
                stageDatas.Add(datas.stage.datas[i].index, datas.stage.datas[i]);
        });
    }

    public void LoadAttackData()
    {
        Managers.Resource.Load<GameObject>("Datas", (_) =>
        {
            Datas datas = _.GetComponent<Datas>();
            for (int i = 0; i < datas.skill.datas.Count; i++)
                attackDatas.Add(datas.skill.datas[i].index, datas.skill.datas[i]);
        });
    }

    public void LoadHeroData()
    {
        Managers.Resource.Load<GameObject>("Datas", (_) =>
        {
            Datas datas = _.GetComponent<Datas>();
            for (int i = 0; i < datas.hero.datas.Count; i++)
                heroDatas.Add(datas.hero.datas[i].index, datas.hero.datas[i]);
        });
    }
}


[System.Serializable]
public class UserDatas
{
    public UserData[] datas;
}

[System.Serializable]
public class UserData
{
    public int ID;
}
[System.Serializable]
public class StageLevelData
{
    public int level;
    public float needEXP;
}

[System.Serializable]
public class SkillData
{
    public int index;
    public string name;
    public string description;
    public Define.Attacks attackType;
}



[System.Serializable]
public class StageData
{
    public int index;
    public string stageTitle;
    public string stageLevel;
}

[System.Serializable]
public class HeroData
{
    public int index;
    public float hp;
    public float speed;
    public string name;
    public Define.Attacks attack;
}