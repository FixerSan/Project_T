using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Profile/StageLevelData", fileName = "StageLevelDataProfile")]
public class StageLevelDataProfile : ScriptableObject
{
    public List<StageLevelData> datas;
}
