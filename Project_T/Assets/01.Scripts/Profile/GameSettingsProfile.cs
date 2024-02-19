using UnityEngine;

[CreateAssetMenu(menuName = "Profile/GameSettingsProfile", fileName = "GameSettingsProfile")]
public class GameSettingsProfile : ScriptableObject
{
    public bool isDebuging;
    public Define.Scene startScene;
}
