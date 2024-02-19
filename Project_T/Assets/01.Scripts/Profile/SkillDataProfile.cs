using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Profile/AttackData", fileName = "AttackDataProfile")]
public class SkillDataProfile : ScriptableObject
{
    public List<SkillData> datas = new List<SkillData>();
}
