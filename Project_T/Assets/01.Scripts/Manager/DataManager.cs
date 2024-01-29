using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    public Dictionary<int, UserData> userDatas = new Dictionary<int, UserData>();
    public Dictionary<int, StageLevelData> stageLevelDatas = new Dictionary<int, StageLevelData>();

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

    public void LoadPreData(Action _callback)
    {
        //LoadUserData();
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
        StageLevelDataProfile proflie = Managers.Resource.Load<StageLevelDataProfile>("StageLevelDataProfile");
        for (int i = 0; i < proflie.datas.Count; i++)
            stageLevelDatas.Add(proflie.datas[i].level, proflie.datas[i]);
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