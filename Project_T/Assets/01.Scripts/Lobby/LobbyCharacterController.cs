using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyCharacterController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Dictionary<Define.LobbyCharacterState, int> animHash = new Dictionary<Define.LobbyCharacterState, int>();

    public void Init()
    {
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);
        sr = Util.FindChild<SpriteRenderer>(gameObject, _recursive: true);
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();

        animHash.Add(Define.LobbyCharacterState.Idle, Animator.StringToHash("0_idle"));
        animHash.Add(Define.LobbyCharacterState.MoveEffect, Animator.StringToHash("5_Skill_Magic"));
        animHash.Add(Define.LobbyCharacterState.MoveToStage, Animator.StringToHash("1_Run"));
    }

    public void MoveToStage(Action _callback)
    {
        StartCoroutine(MoveToStageRoutine(_callback));
    }

    private IEnumerator MoveToStageRoutine(Action _callback)
    {
        anim.Play(animHash[Define.LobbyCharacterState.MoveEffect]);
        yield return new WaitForSeconds(0.667f);

        anim.Play(animHash[Define.LobbyCharacterState.MoveToStage]);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        rb.velocity = new Vector2(5, 0);
        while (sr.isVisible)
        {
            yield return null;
        }

        _callback?.Invoke();
    }
}
