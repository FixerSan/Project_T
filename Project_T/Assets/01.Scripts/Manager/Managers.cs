using System.Runtime.Serialization;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    private ResourceManager resource;
    private PoolManager pool;
    private UIManager ui;
    private ScreenManager screen;
    private SceneManager scene;
    private ObjectManager _object;
    private BattleManager battle;
    private DataManager data;
    private EventManager _event;

    private InputManager input;
    private CoroutineManager routine;
    private GameManager game;

    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
    public static UIManager UI { get { return Instance?.ui; } }
    public static ScreenManager Screen { get { return Instance?.screen; } }
    public static SceneManager Scene { get { return Instance?.scene; } }
    public static ObjectManager Object { get { return Instance?._object; } }
    public static BattleManager Battle { get { return Instance?.battle; } }
    public static DataManager Data { get { return Instance?.data; } }
    public static EventManager Event { get { return Instance?._event; } }

    public static InputManager Input { get { return Instance?.input; } }
    public static CoroutineManager Routine { get { return Instance?.routine; } }
    public static GameManager Game { get { return Instance?.game; } }


    [RuntimeInitializeOnLoadMethod()]
    public static void CreateManagers()
    {
        Instance.resource = new ResourceManager();
        Instance.pool = new PoolManager();
        Instance.ui = new UIManager();
        Instance.screen = new ScreenManager();
        Instance.scene = new SceneManager();
        Instance._object = new ObjectManager();
        Instance.battle = new BattleManager();
        Instance.data = new DataManager();
        Instance._event = new EventManager();

        Instance.input = InputManager.Instance;
        Instance.routine = CoroutineManager.Instance;
        Instance.game = GameManager.Instance;
    }
}
