using Monsters;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public PlayerController PlayerController { get { return playerController; } }
    private PlayerController playerController;

    public Vector3 PlayerMovePos
    {
        get
        {
            if (playerMovePos == null)
                CreatePlayerMovePos();
            return playerMovePos.transform.position;
        }
    }
    private PlayerMovePos playerMovePos;

    private PlayerAttackController playerAttackController;
    public PlayerAttackController PlayerAttackController
    {
        get
        {
            if (playerAttackController == null)
                CreatePlayerAttackController();
            return playerAttackController;
        }
    }


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
    private Transform playerControllerTrans;

    public List<MonsterController> monsters = new List<MonsterController>();
    public Transform MonsterTrans
    {
        get
        {
            if (monsterTrans == null)
            {
                GameObject go = GameObject.Find("@MonsterTrans");
                if (go == null)
                    go = new GameObject("@MonsterTrans");
                monsterTrans = go.transform;
            }
            return monsterTrans;
        }
    }
    private Transform monsterTrans;


    public List<EXPController> exps = new List<EXPController>();
    public List<BoomController> booms = new List<BoomController>();
    public List<MagnetController> magnets = new List<MagnetController>();

    public LobbyCharacterController LobbyCharacterController
    {
        get
        {
            if (lobbyCharacterController == null)
                SpawnLobbyCharacterController();
            return lobbyCharacterController;
        }
    }
    private LobbyCharacterController lobbyCharacterController;

    public Transform ItemTrans
    {
        get
        {
            if (itemTrans == null)
            {
                GameObject go = GameObject.Find("@ItemTrans");
                if (go == null)
                    go = new GameObject("@ItemTrans");
                itemTrans = go.transform;
            }
            return itemTrans;
        }
    }
    private Transform itemTrans;

    public PlayerController SpawnPlayer(int _index,Vector3 _playerPos)
    {
        playerController = Managers.Resource.Instantiate($"Player_{+_index}").GetOrAddComponent<PlayerController>();
        Player player = new Player(new PlayerData(), playerController);
        Dictionary<Define.PlayerState, State<PlayerController>> states = new Dictionary<Define.PlayerState, State<PlayerController>>();
        states.Add(Define.PlayerState.Idle, new PlayerStates.Idle());
        states.Add(Define.PlayerState.Move, new PlayerStates.Move());
        states.Add(Define.PlayerState.Die, new PlayerStates.Die());
        playerController.SetPosition(_playerPos);
        playerController.Init(player, states, new Status());

        return playerController;
    }



    public MonsterController SpawnMonster(int _monsterIndex, Vector3 _monsterPos)
    {
        MonsterController controller = Managers.Resource.Instantiate($"Monster_{_monsterIndex}", _pooling: true).GetOrAddComponent<MonsterController>();
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
                states.Add(Define.MonsterState.Idle, new MonsterStates.Base.Idle());
                states.Add(Define.MonsterState.Move, new MonsterStates.Base.Move());
                states.Add(Define.MonsterState.Follow, new MonsterStates.Base.Follow());
                states.Add(Define.MonsterState.Attack, new MonsterStates.Base.Attack());
                states.Add(Define.MonsterState.SkillCast, new MonsterStates.Base.SkillCast());
                states.Add(Define.MonsterState.Die, new MonsterStates.Base.Die());
                break;
        }
        controller.SetPosition(_monsterPos);
        controller.transform.SetParent(MonsterTrans);
        controller.Init(monster, states, new Status());
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

    public PlayerMovePos CreatePlayerMovePos()
    {
        GameObject go = GameObject.Find("PlayerMovePos");
        if (go == null)
            go = Managers.Resource.Instantiate("PlayerMovePos");
        playerMovePos = go.GetOrAddComponent<PlayerMovePos>();
        return playerMovePos;
    }

    public PlayerAttackController CreatePlayerAttackController()
    {
        GameObject go = GameObject.Find("PlayerAttackController");
        if (go == null)
            go = Managers.Resource.Instantiate("PlayerAttackController");
        playerAttackController = go.GetOrAddComponent<PlayerAttackController>();
        return playerAttackController;
    }

    public EXPController SpawnEXPController(int _expLevel, float _exp, Vector3 _spanwPos)
    {
        EXPController controller = Managers.Resource.Instantiate("EXPController").GetOrAddComponent<EXPController>();
        controller.Init(_expLevel, _exp);
        controller.transform.SetParent(ItemTrans);
        controller.transform.position = _spanwPos;
        exps.Add(controller);
        return controller;
    }

    public void ClearEXPController(EXPController _controller)
    {
        exps.Remove(_controller);
        Managers.Resource.Destroy(_controller.gameObject);
    }

    public BoomController SpawnBoomController(Vector3 _spanwPos)
    {
        BoomController controller = Managers.Resource.Instantiate("BoomController").GetOrAddComponent<BoomController>();
        controller.transform.SetParent(ItemTrans);
        controller.transform.position = _spanwPos;
        booms.Add(controller);
        return controller;
    }

    public void ClearBoomController(BoomController _controller)
    {
        booms.Remove(_controller);
        Managers.Resource.Destroy(_controller.gameObject);
    }

    public MagnetController SpawnMagnetController(Vector3 _spanwPos)
    {
        MagnetController controller = Managers.Resource.Instantiate("MagnetController").GetOrAddComponent<MagnetController>();
        controller.transform.SetParent(ItemTrans);
        controller.transform.position = _spanwPos;
        magnets.Add(controller);
        return controller;
    }

    public void ClearMagnetController(MagnetController _controller)
    {
        magnets.Remove(_controller);
        Managers.Resource.Destroy(_controller.gameObject);
    }

    public LobbyCharacterController SpawnLobbyCharacterController()
    {
        GameObject go = Managers.Resource.Instantiate($"{nameof(LobbyCharacterController)}_{Managers.Game.main.nowHeroIndex}");
        lobbyCharacterController = go.GetOrAddComponent<LobbyCharacterController>();
        lobbyCharacterController.transform.position = new Vector3(-3.5f, -2.5f, 0);
        lobbyCharacterController.transform.SetParent(null);
        lobbyCharacterController.Init();
        lobbyCharacterController.MoveToMain();
        return lobbyCharacterController;
    }

    public void ClearLobbyCharacterController()
    {
        Managers.Resource.Destroy(lobbyCharacterController.gameObject);
        lobbyCharacterController = null;
    }

    public void ClearLobbyCharacterController(LobbyCharacterController _controller)
    {
        Managers.Resource.Destroy(_controller.gameObject);
        lobbyCharacterController = null;
    }
}
