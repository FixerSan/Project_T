using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameSettingsProfile gameSettings;
    public StageSystem stage = new StageSystem();

    private void Awake()
    {
        GameStart();
    }

    //게임 시작 되었을 때
    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<Object>("Preload", _completeCallback: () =>                  //프리로드 리소스 로드
        {
            Managers.Data.LoadPreData(() => 
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
        });
    }

    public void StartStage()
    {
        stage.Clear();
    }
}

public class StageSystem
{
    public Dictionary<Define.Attacks, BaseAttack> attacks = new Dictionary<Define.Attacks, BaseAttack>();

    public const int maxLevel = 20;
    public int currentPlayerLevel = 1;
    public float needEXP;
    public float currentEXP;

    public void GetAttack(Define.Attacks _attackType)
    {
        int currentAttackLevel = 0;
        //있는 무기 인지 체크
        if(attacks.ContainsKey(_attackType))
        {
            //있는 무기이면 현재 무기삭제
            currentAttackLevel = attacks[_attackType].level;
            Managers.Resource.Destroy(attacks[_attackType].gameObject);
            attacks.Remove(_attackType);
        }
       //무기 추가
        Managers.Object.PlayerController.AddAttack(_attackType, currentAttackLevel + 1 , (_attack) => { attacks.Add(_attackType, _attack); });
    }

    public void GetEXP(float _exp)
    {
        if (maxLevel == currentPlayerLevel) return;
        currentEXP += _exp;
        if (currentEXP >= needEXP)
            LevelUp();
    }

    public void LevelUp()
    {
        currentEXP -= needEXP;
        currentPlayerLevel++;

        //needEXP 값 재설정
        StageLevelData data = Managers.Data.GetStageLevelData(currentPlayerLevel);
        needEXP = data.needEXP;

        //TODO :: 레벨업 하고 나서 무기 선택 창
    }

    public void Clear()
    {
        //초기화
        currentPlayerLevel = 1;
        StageLevelData data = Managers.Data.GetStageLevelData(currentPlayerLevel);
        needEXP = data.needEXP;
        currentEXP = 0;
    }
}
