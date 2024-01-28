using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovePos : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public float force;
    private Vector3 dir = Vector3.zero;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if(Managers.Input.joystickInputValue == Vector2.zero)
        {
            spriteRenderer.enabled = false;
            return;
        }

        if(!spriteRenderer.enabled)
            spriteRenderer.enabled = true;

        dir.x = Managers.Input.joystickInputValue.x;
        dir.y = Managers.Input.joystickInputValue.y;
        dir *= force;
        dir += offset;

        transform.position = Managers.Object.PlayerController.transform.position + dir;
    }
}
