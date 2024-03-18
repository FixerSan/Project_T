using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameSettingsProfile gameSettings;
    public MainSystem main = new MainSystem();
    public StageSystem stage = new StageSystem();

    //게임 시작 되었을 때
    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<UnityEngine.Object>("Preload", _completeCallback: () =>                  //프리로드 리소스 로드
        {
            Managers.Data.LoadPreData(() =>
            {
                gameSettings = Managers.Resource.Load<GameObject>("Datas").GetOrAddComponent<Datas>().game;      //게임 세팅 설정
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
        stage.Init();
    }

    public void Update()
    {
        stage.Update();
    }
}

public class MainSystem
{
    public StageData stageData;
    public int selectedStageIndex = 1;
    public int selectedHeroIndex = 0;
    public int nowHeroIndex = 0;

    public void OpenStageList()
    {
        Managers.Object.LobbyCharacterController.MoveToStage(() =>
        {
            stageData = Managers.Data.GetStageData(selectedStageIndex);
            Managers.UI.ShowPopupUI<UIPopup_SelectStage>();
        });
    }

    public void SetNextStage()
    {
        selectedStageIndex++;
        if (selectedStageIndex > Define.maxStageIndex)
            selectedStageIndex = Define.minStageIndex;
        stageData = Managers.Data.GetStageData(selectedStageIndex);
        Managers.UI.activePopups[Define.UIType.UIPopup_SelectStage].RedrawUI();
    }

    public void SetBeforeStage()
    {
        selectedStageIndex--;
        if (selectedStageIndex < Define.minStageIndex)
            selectedStageIndex = Define.maxStageIndex;
        stageData = Managers.Data.GetStageData(selectedStageIndex);
        Managers.UI.activePopups[Define.UIType.UIPopup_SelectStage].RedrawUI();
    }

    public void SelectHero(int _heroIndex)
    {
        selectedHeroIndex = _heroIndex;
        Managers.UI.activePopups[Define.UIType.UIPopup_Heroes].RedrawUI();
    }

    public void ChangeHero()
    {
        Managers.Object.ClearLobbyCharacterController();
        nowHeroIndex = selectedHeroIndex;
        Managers.Object.SpawnLobbyCharacterController();
        Managers.UI.ClosePopupUI(Managers.UI.activePopups[Define.UIType.UIPopup_Heroes]);
    }

    public void StartStage()
    {
        if (stageData.index == 1) Managers.Scene.LoadScene(Define.Scene.Stage_One);
    }
}


public class StageSystem
{
    public Dictionary<Define.Attacks, BaseAttack> attacks = new Dictionary<Define.Attacks, BaseAttack>();

    public const int maxLevel = 20;
    public int currentPlayerLevel = 1;
    public float needEXP;
    public float currentEXP;

    public bool isStarted = false;
    public float time = 0;
    public int currentStagePattern = 1;

    public Transform PlayerAttackTrans
    {
        get
        {
            return Managers.Object.PlayerController.FindAttackTarget();
        }
    }

    public void Init()
    {
        isStarted = true;
        time = 0;
        currentStagePattern = 1;
        currentPlayerLevel = 1;
        currentEXP = 0;
        needEXP = Managers.Data.GetStageLevelData(currentPlayerLevel).needEXP;
    }

    public void GetAttack(Define.Attacks _attackType)
    {
        int currentAttackLevel = 1;
        //있는 무기 인지 체크
        if (attacks.ContainsKey(_attackType))
        {
            //있는 무기이면 현재 무기삭제
            currentAttackLevel = attacks[_attackType].level + 1;
            Managers.Resource.Destroy(attacks[_attackType].gameObject);
            attacks.Remove(_attackType);
        }
        //무기 추가
        Managers.Object.PlayerAttackController.AddAttack(_attackType, currentAttackLevel, (_attack) => { attacks.Add(_attackType, _attack); });
    }

    public void GetEXP(float _exp)
    {
        if (maxLevel == currentPlayerLevel) return;
        currentEXP += _exp;
        if (currentEXP >= needEXP)
            LevelUp();
        RedrawUI();
    }

    public void LevelUp()
    {
        currentEXP -= needEXP;
        currentPlayerLevel++;

        //needEXP 값 재설정
        StageLevelData data = Managers.Data.GetStageLevelData(currentPlayerLevel);
        needEXP = data.needEXP;

        //TODO :: 레벨업 하고 나서 무기 선택 창
        Time.timeScale = 0;

        int[] skillIndexes = GetRandomIndex();
        bool isSame = CheckSame(skillIndexes);

        while (isSame)
        {
            skillIndexes = GetRandomIndex();
            isSame = CheckSame(skillIndexes);
        }

        Managers.UI.ShowPopupUI<UIPopup_SelectLevelUpReward>().RedrawUI(skillIndexes);
        RedrawUI();
    }

    public int[] GetRandomIndex()
    {
        int[] skillIndexes = new int[3] { (int)Extension.GetRandomEnum<Define.Attacks>(), (int)Extension.GetRandomEnum<Define.Attacks>(), (int)Extension.GetRandomEnum<Define.Attacks>() };
        return skillIndexes;
    }

    public bool CheckSame(int[] _skillIndexes)
    {
        bool isSame = false;

        for (int i = 0; i < _skillIndexes.Length; i++)
        {
            if (isSame) break;
            for (int j = 0; j < _skillIndexes.Length; j++)
            {
                if (i == j) continue;
                if (_skillIndexes[j] == _skillIndexes[i])
                {
                    isSame = true;
                    break;
                }
            }
        }

        return isSame;
    }

    public void SelectLevelUpReward(int _selectAttackIndex)
    {
        SkillData skill = Managers.Data.GetAttackData(_selectAttackIndex);
        Managers.UI.activePopups[Define.UIType.UIPopup_SelectLevelUpReward].ClosePopupUP(() =>
        {
            Time.timeScale = 1;
            Managers.Game.stage.GetAttack(skill.attackType);
        });
    }

    public void GetItem_Boom()
    {
        for (int i = 0; i < Managers.Object.monsters.Count; i++)
        {
            if (Managers.Object.monsters[i].sr.isVisible)
                Managers.Object.monsters[i].ChangeState(Define.MonsterState.Die);
        }
    }

    public void RedrawUI()
    {
        Managers.UI.SceneUI?.RedrawUI();
    }

    public void Clear()
    {
        //초기화
        currentPlayerLevel = 1;
        StageLevelData data = Managers.Data.GetStageLevelData(currentPlayerLevel);
        needEXP = data.needEXP;
        currentEXP = 0;
    }

    public void Update()
    {
        if (!isStarted) return;
        CheckTime();
    }

    public void CheckTime()
    {
        time += Time.deltaTime;
        if (currentStagePattern == 1)
        {
            if (time >= 120f)
            {
                Managers.Scene.GetActiveScene<TestScene>().NextPattern();
                currentStagePattern++;
            }
        }

        if (currentStagePattern == 2)
        {
            if (time >= 240f)
            {
                Managers.Scene.GetActiveScene<TestScene>().NextPattern();
                currentStagePattern++;
            }
        }

        if (currentStagePattern == 3)
        {
            if (time >= 360f)
            {
                Managers.Scene.GetActiveScene<TestScene>().NextPattern();
                currentStagePattern++;
            }
        }
        RedrawUI();
    }
}
