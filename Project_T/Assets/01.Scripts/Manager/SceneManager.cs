using System;
using UnityEngine;

public class SceneManager
{
    private Transform sceneTrans;
    public Transform SceneTrans
    {
        get
        {
            if (sceneTrans == null)
            {
                GameObject go = GameObject.Find("@SceneTrans");
                if (go == null)
                    go = new GameObject(name: "@SceneTrans");
                sceneTrans = go.transform;
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
            return sceneTrans;
        }
    }
    private Define.Scene currentScene;
    private bool isLoading = false;
    private Action loadCallback;

    public void LoadScene(Define.Scene _scene, bool _isHasFade = true, Action _loadCallback = null)
    {
        if (!_isHasFade)
        {
            if (isLoading) return;
            isLoading = true;
            loadCallback = _loadCallback;
            Managers.Pool.Clear();
            Managers.UI.CloseAllPopupUI();

            RemoveScene(currentScene, () =>
            {
                currentScene = _scene;
                AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync($"Scene_{_scene}");
                async.completed += (_) =>
                {
                    AddScene(_scene, () => { loadCallback?.Invoke(); });
                    isLoading = false;
                };
            });
            return;
        }

        Managers.Screen.FadeIn(0.25f, () =>
        {
            if (isLoading) return;
            isLoading = true;
            loadCallback = _loadCallback;

            Managers.Pool.Clear();
            Managers.UI.CloseAllPopupUI();
            RemoveScene(currentScene, () =>
            {
                currentScene = _scene;
                AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync($"Scene_{_scene}");
                async.completed += (_) =>
                {
                    AddScene(_scene, () =>
                    {
                        isLoading = false; loadCallback?.Invoke();
                        Managers.Screen.FadeOut(0.25f);
                    });
                };
            });
        });
    }

    public void RemoveScene(Define.Scene _scene, Action _callback = null)
    {
        BaseScene bs = null;
        switch (_scene)
        {
            case Define.Scene.Test:
                bs = sceneTrans.GetComponent<TestScene>();
                break;
            
            case Define.Scene.Stage_One:
                bs = sceneTrans.GetComponent<TestScene>();
                break;

            case Define.Scene.Main:
                bs = SceneTrans.GetComponent<MainScene>();
                break;

            default:
                _callback?.Invoke();
                return;
        }

        if (bs != null)
        {
            bs.Clear();
            UnityEngine.Object.Destroy(bs);
        }
        _callback?.Invoke();
    }

    // ¾À Ãß°¡
    public void AddScene(Define.Scene _scene, Action _addSceneCallback)
    {
        BaseScene bs = null;
        //Managers.Data.LoadSceneData(addScene);
        switch (_scene)
        {
            //case Define.Scene.Stage:
            //    bs = SceneTrans.gameObject.AddComponent<StageScene>();
            //    break;

            case Define.Scene.Test:
                bs = SceneTrans.gameObject.AddComponent<TestScene>();
                break;

            case Define.Scene.Stage_One:
                bs = SceneTrans.gameObject.AddComponent<TestScene>();
                break;

            case Define.Scene.Main:
                bs = SceneTrans.gameObject.AddComponent<MainScene>();
                break;

            default:
                _addSceneCallback?.Invoke();
                return;
        }

        bs.Init(() =>
        {
            _addSceneCallback?.Invoke();
        });
    }

    public T GetActiveScene<T>() where T : BaseScene
    {
        return SceneTrans.GetComponent<T>() as T;
    }
}
