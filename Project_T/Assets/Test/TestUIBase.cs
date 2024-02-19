using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TestUIBase : MonoBehaviour
{
    protected Dictionary<Type, Dictionary<string, Object>> testObjectDictionary = new Dictionary<Type, Dictionary<string, Object>>();
    protected bool init = false;
    public virtual bool Init()
    {
        if (init)
            return false;
        //초기화 내용 적을 곳
        init = true;
        return true;
    }

    private void Awake()
    {
        Init();
    }

    protected void Bind<T>(Type _type) where T : Object
    {
        if (testObjectDictionary.ContainsKey(typeof(T))) return;
        string[] names = Enum.GetNames(_type);
        Dictionary<string, Object> objects = new Dictionary<string, Object>();
        testObjectDictionary.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects.Add(names[i], Util.FindChild(gameObject, names[i], true));

            else
                objects.Add(names[i], Util.FindChild<T>(gameObject, names[i], true));

            if (!objects.ContainsKey(names[i]) || objects[names[i]] == null)
                Debug.Log($"{names[i]}의 바인딩의 실패하였습니다.");
        }
    }

    protected void BindObject(Type _type) { Bind<GameObject>(_type); }
    protected void BindRect(Type _type) { Bind<RectTransform>(_type); }
    protected void BindImage(Type _type) { Bind<Image>(_type); }
    protected void BindText(Type _type) { Bind<TMP_Text>(_type); }
    protected void BindButton(Type _type) { Bind<Button>(_type); }
    protected void BindToggle(Type _type) { Bind<Toggle>(_type); }
    protected void BindSlider(Type _type) { Bind<Slider>(_type); }
    protected void BindInputField(Type _type) { Bind<TMP_InputField>(_type); }

    protected T Get<T>(string _name) where T : Object
    {
        if (!testObjectDictionary.TryGetValue(typeof(T), out Dictionary<string, Object> _objects))
            return null;

        return _objects[_name] as T;
    }

    protected GameObject GetObject(string _name) { return Get<GameObject>(_name); }
    protected RectTransform GetRect(string _name) { return Get<RectTransform>(_name); }
    protected TMP_Text GetText(string _name) { return Get<TMP_Text>(_name); }
    protected Button GetButton(string _name) { return Get<Button>(_name); }
    protected Image GetImage(string _name) { return Get<Image>(_name); }
    protected Toggle GetToggle(string _name) { return Get<Toggle>(_name); }
    protected Slider GetSlider(string _name) { return Get<Slider>(_name); }
    protected TMP_InputField GetInputField(string _name) { return Get<TMP_InputField>(_name); }

    public static void BindEvent(GameObject _go, Action _callback = null, Action<PointerEventData> _dragCallback = null, Action<PointerEventData> _dropCallback = null, Define.UIEventType _type = Define.UIEventType.Click)
    {
        UIEventHandler eventHandler = _go.GetOrAddComponent<UIEventHandler>();

        switch (_type)
        {
            case Define.UIEventType.Click:
                eventHandler.OnClickHandler -= _callback;
                eventHandler.OnClickHandler += _callback;
                break;

            case Define.UIEventType.Pressed:
                eventHandler.OnPressedHandler -= _callback;
                eventHandler.OnPressedHandler += _callback;
                break;

            case Define.UIEventType.PointerDown:
                eventHandler.OnPointerDownHandler -= _callback;
                eventHandler.OnPointerDownHandler += _callback;
                break;

            case Define.UIEventType.PointerUp:
                eventHandler.OnPointerUpHandler -= _callback;
                eventHandler.OnPointerUpHandler += _callback;
                break;

            case Define.UIEventType.BeginDrag:
                eventHandler.OnBeginDragHandler -= _dragCallback;
                eventHandler.OnBeginDragHandler += _dragCallback;
                break;

            case Define.UIEventType.Drag:
                eventHandler.OnDragHandler -= _dragCallback;
                eventHandler.OnDragHandler += _dragCallback;
                break;

            case Define.UIEventType.EndDrag:
                eventHandler.OnEndDragHandler -= _dragCallback;
                eventHandler.OnEndDragHandler += _dragCallback;
                break;

            case Define.UIEventType.Drop:
                eventHandler.OnDropHandler -= _dropCallback;
                eventHandler.OnDropHandler += _dropCallback;
                break;
        }
    }

}
