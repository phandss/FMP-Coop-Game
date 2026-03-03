using UnityEngine;

public class MoveableTorch : InteractObjectBase
{
    public override bool isInteractable => true;
    public override bool isDraggable => true;

    private bool _beingCarried;

    [SerializeField] private float _carryForce = 10f;

    public override void OnDragStart(Vector3 pos)
    {

        if (!AttemptInteractLock())
        {
            return;
        }

        _beingCarried = true;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
        rb.linearDamping = 10f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

    }

    public override void OnDrag(Vector3 worldPos)
    {

        if (!_beingCarried)
        {
            return;
        }

        rb.linearVelocity = (worldPos - rb.position) * _carryForce; ;
    }



    public override void OnDragEnd()
    {
        if (!_beingCarried)
        {
            return;
        }
        _beingCarried = false;

        rb.useGravity = true;
        rb.linearDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.linearVelocity = Vector3.zero;
        UnlockInteract();

    }


}
