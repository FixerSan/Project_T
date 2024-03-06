using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Profile/HeroData", fileName = "HeroDataProfile")]

public class HeroDataProfile : ScriptableObject
{
    public List<HeroData> datas = new List<HeroData>();
}
