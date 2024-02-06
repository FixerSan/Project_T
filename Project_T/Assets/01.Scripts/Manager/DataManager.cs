using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    public Dictionary<int, UserData> userDatas = new Dictionary<int, UserData>();
    public Dictionary<int, StageLevelData> stageLevelDatas = new Dictionary<int, StageLevelData>();
    public Dictionary<int, SkillData> attackDatas = new Dictionary<int, SkillData>();

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

    public void LoadPreData(Action _callback)
    {
        //LoadUserData();
        LoadAttackData();
        LoadStageLevelData();
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
        Managers.Resource.Load<StageLevelDataProfile>("StageLevelDataProfile", (_) => 
        {
            for (int i = 0; i < _.datas.Count; i++)
                stageLevelDatas.Add(_.datas[i].level, _.datas[i]);
        });
    }

    public void LoadAttackData()
    {
        Managers.Resource.Load<SkillDataProfile>("AttackDataProfile", (_) =>
        {
            for (int i = 0; i < _.datas.Count; i++)
                attackDatas.Add(_.datas[i].index, _.datas[i]);
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