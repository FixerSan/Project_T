using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    public GameSettingsProfile gameSettings;
    public StageSystem stage = new StageSystem();

    //���� ���� �Ǿ��� ��
    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<UnityEngine.Object>("Preload",_callback:(name,current,end) => { Debug.Log($"{name}�� �ε�� {current} / {end}"); }, _completeCallback: () =>                  //�����ε� ���ҽ� �ε�
        {
            Managers.Data.LoadPreData(() => 
            {
                gameSettings = Managers.Resource.Load<GameObject>("Datas").GetOrAddComponent<Datas>().game;      //���� ���� ����
                if (gameSettings.isDebuging)                                                            //����� ���� ��
                {
                    Debug.Log("y");
                    Managers.Scene.LoadScene(gameSettings.startScene);
                    return;
                }
                //����� ���� �ƴ� ��
                Debug.Log("n");            
            });
        });
        Time.timeScale = 10;
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

public class StageSystem
{
    public Dictionary<Define.Attacks, BaseAttack> attacks = new Dictionary<Define.Attacks, BaseAttack>();

    public const int maxLevel = 20;
    public int currentPlayerLevel = 1;
    public float needEXP;
    public float currentEXP;

    public bool isStarted = true;
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
        currentPlayerLevel = 1;
        currentEXP = 0;
        needEXP = Managers.Data.GetStageLevelData(currentPlayerLevel).needEXP;
    }

    public void GetAttack(Define.Attacks _attackType)
    {
        int currentAttackLevel = 1;
        //�ִ� ���� ���� üũ
        if(attacks.ContainsKey(_attackType))
        {
            //�ִ� �����̸� ���� �������
            currentAttackLevel = attacks[_attackType].level + 1;
            Managers.Resource.Destroy(attacks[_attackType].gameObject);
            attacks.Remove(_attackType);
        }
       //���� �߰�
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

        //needEXP �� �缳��
        StageLevelData data = Managers.Data.GetStageLevelData(currentPlayerLevel);
        needEXP = data.needEXP;

        //TODO :: ������ �ϰ� ���� ���� ���� â
        Time.timeScale = 0;

        int[] skillIndexes = GetRandomIndex();
        bool isSame = CheckSame(skillIndexes);

        while(isSame)
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
        //�ʱ�ȭ
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
            if(time >= 120f)
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
