using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isShake;
    public Transform target;
    public Vector3 offset;

    private Vector3 tempVecter;

    public CameraController()
    {
        isShake = false;
    }

    private void Update()
    {
        CheckCanFollow();
    }

    public void CheckCanFollow()
    {
        if (target != null)
        {
            FollowTarget();
        }
    }

    public void FollowTarget()
    {
        tempVecter.x = target.position.x + offset.x;
        tempVecter.y = target.position.y + offset.y;
        tempVecter.z = target.position.z + offset.z;
        transform.position = tempVecter;
    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
