using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Dictionary<Define.LobbyCharacterState, int> animHash = new Dictionary<Define.LobbyCharacterState, int>();

    public bool isMovindLobbyObject = false;

    public void Init()
    {
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);
        sr = Util.FindChild<SpriteRenderer>(gameObject, _recursive: true);
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();

        animHash.Add(Define.LobbyCharacterState.Idle, Animator.StringToHash("0_idle"));
        animHash.Add(Define.LobbyCharacterState.MoveEffect, Animator.StringToHash("5_Skill_Magic"));
        animHash.Add(Define.LobbyCharacterState.Walk, Animator.StringToHash("1_Run"));
    }

    public void MoveToStage(Action _callback)
    {
        if (isMovindLobbyObject) return;
        isMovindLobbyObject = true;
        StartCoroutine(MoveToStageRoutine(_callback));
    }

    private IEnumerator MoveToStageRoutine(Action _callback)
    {
        anim.Play(animHash[Define.LobbyCharacterState.MoveEffect]);
        yield return new WaitForSeconds(0.667f);

        anim.Play(animHash[Define.LobbyCharacterState.Walk]);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        rb.velocity = new Vector2(5, 0);
        yield return new WaitUntil(() => !sr.isVisible);

        isMovindLobbyObject = false;
        _callback?.Invoke();
        Managers.Object.ClearLobbyCharacterController(this);
    }

    public void MoveToMain(Action _callback = null)
    {
        if (isMovindLobbyObject) return;
        isMovindLobbyObject = true;
        StartCoroutine(MoveToMainRoutine(_callback));
    }

    private IEnumerator MoveToMainRoutine(Action _callback)
    {
        anim.Play(animHash[Define.LobbyCharacterState.Walk]);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        rb.velocity = new Vector2(5, 0);
        yield return new WaitUntil(() => transform.position.x >= 0);

        rb.velocity = Vector2.zero;
        anim.Play(animHash[Define.LobbyCharacterState.Idle]);
        isMovindLobbyObject = false;
        _callback?.Invoke();
    }
}
