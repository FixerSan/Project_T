using Monsters;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public PlayerController PlayerController { get { return playerController; } }
    private PlayerController playerController;

    public Vector3 PlayerMovePos 
    {
        get
        {
            if(playerMovePos == null)
                CreatePlayerMovePos();
            return playerMovePos.transform.position; 
        }
    }
    private PlayerMovePos playerMovePos;


    public Transform PlayerControllerTrans
    {
        get
        {
            if (playerControllerTrans == null)
            {
                GameObject go = GameObject.Find("@PlayerControllerTrans");
                if (go == null)
                    go = new GameObject(name: "@PlayerControllerTrans");
                playerControllerTrans = go.transform;
            }
            return playerControllerTrans;
        }
    }
    public Transform playerControllerTrans;

    public List<MonsterController> monsters = new List<MonsterController>();

    public PlayerController SpawnPlayer(Vector3 _playerPos)
    {
        playerController = Managers.Resource.Instantiate($"Player").GetOrAddComponent<PlayerController>();
        Player player = new Player(new PlayerData(), playerController);
        Dictionary<Define.PlayerState, State<PlayerController>> states = new Dictionary<Define.PlayerState, State<PlayerController>>();
        states.Add(Define.PlayerState.Idle, new PlayerStates.Idle());
        states.Add(Define.PlayerState.Move, new PlayerStates.Move());
        states.Add(Define.PlayerState.Follow, new PlayerStates.Follow());
        states.Add(Define.PlayerState.Attack, new PlayerStates.Attack());
        states.Add(Define.PlayerState.SkillCast, new PlayerStates.SkillCast());
        states.Add(Define.PlayerState.Die, new PlayerStates.Die());
        playerController.SetPosition(_playerPos);
        playerController.Init(player, states, new Status());

        return playerController;
    }



    public MonsterController SpawnMonster(int _monsterIndex, Vector3 _monsterPos)
    {
        MonsterController controller = Managers.Resource.Instantiate($"Monster_{_monsterIndex}", _pooling:true).GetOrAddComponent<MonsterController>();
        BaseMonster monster = null;
        Dictionary<Define.MonsterState, State<MonsterController>> states = new Dictionary<Define.MonsterState, State<MonsterController>>();

        switch (_monsterIndex)
        {
            case 0:
                monster = new BaseMonster(new MonsterData(), controller);
                states.Add(Define.MonsterState.Create, new MonsterStates.Base.Create());
                states.Add(Define.MonsterState.Idle, new MonsterStates.Base.Idle());
                states.Add(Define.MonsterState.Move, new MonsterStates.Base.Move());
                states.Add(Define.MonsterState.Follow, new MonsterStates.Base.Follow());
                states.Add(Define.MonsterState.Attack, new MonsterStates.Base.Attack());
                states.Add(Define.MonsterState.SkillCast, new MonsterStates.Base.SkillCast());
                states.Add(Define.MonsterState.Die, new MonsterStates.Base.Die());
                break;

            default:
                monster = new BaseMonster(new MonsterData(), controller);
                states.Add(Define.MonsterState.Create, new MonsterStates.Base.Create());
                states.Add(Define.MonsterState.Idle, new MonsterStates.Base.Create());
                states.Add(Define.MonsterState.Move, new MonsterStates.Base.Move());
                states.Add(Define.MonsterState.Follow, new MonsterStates.Base.Follow());
                states.Add(Define.MonsterState.Attack, new MonsterStates.Base.Attack());
                states.Add(Define.MonsterState.SkillCast, new MonsterStates.Base.SkillCast());
                states.Add(Define.MonsterState.Die, new MonsterStates.Base.Die());
                break;
        }
        controller.SetPosition(_monsterPos);
        controller.Init(monster, states,new Status());
        controller.monster.attackTarget = playerController;
        monsters.Add(controller);

        return controller;
    }

    public void ClearPlayer()
    {
        Managers.Resource.Destroy(playerController.gameObject);
        playerController = null;
    }

    public void ClearMonster(MonsterController _monster)
    {
        if (monsters.Contains(_monster))
        {
            monsters.Remove(_monster);
            Managers.Resource.Destroy(_monster.gameObject);
        }
    }

    public void ClearAllMonster()
    {
        for (int i = 0; i < monsters.Count; i++)
            Managers.Resource.Destroy(monsters[i].gameObject);

        monsters.Clear();
    }

    public void CreatePlayerMovePos()
    {
        GameObject go = GameObject.Find("PlayerMovePos");
        if(go == null)
            go = Managers.Resource.Instantiate("PlayerMovePos");
        playerMovePos = go.GetOrAddComponent<PlayerMovePos>();
    }
}
