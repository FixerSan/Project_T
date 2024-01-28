using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameSettingsProfile gameSettings;

    private void Awake()
    {
        GameStart();
    }

    //게임 시작 되었을 때
    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<Object>("Preload", _completeCallback: () =>                  //프리로드 리소스 로드
        {
            gameSettings = Managers.Resource.Load<GameSettingsProfile>("GameSettingsProfile");      //게임 세팅 설정
            if (gameSettings.isDebuging)                                                            //디버깅 중일 때
            {
                Debug.Log("y");
                Managers.Scene.LoadScene(gameSettings.startScene);
                return;
            }
            //디버깅 중이 아닐 때
            Debug.Log("n");
        });
    }
}
