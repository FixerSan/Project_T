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

    //���� ���� �Ǿ��� ��
    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<Object>("Preload", _completeCallback: () =>                  //�����ε� ���ҽ� �ε�
        {
            Managers.Data.LoadPreData(() => 
            {
                gameSettings = Managers.Resource.Load<GameSettingsProfile>("GameSettingsProfile");      //���� ���� ����
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
        //�ִ� ���� ���� üũ
        if(attacks.ContainsKey(_attackType))
        {
            //�ִ� �����̸� ���� �������
            currentAttackLevel = attacks[_attackType].level;
            Managers.Resource.Destroy(attacks[_attackType].gameObject);
            attacks.Remove(_attackType);
        }
       //���� �߰�
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

        //needEXP �� �缳��
        StageLevelData data = Managers.Data.GetStageLevelData(currentPlayerLevel);
        needEXP = data.needEXP;

        //TODO :: ������ �ϰ� ���� ���� ���� â
    }

    public void Clear()
    {
        //�ʱ�ȭ
        currentPlayerLevel = 1;
        StageLevelData data = Managers.Data.GetStageLevelData(currentPlayerLevel);
        needEXP = data.needEXP;
        currentEXP = 0;
    }
}
