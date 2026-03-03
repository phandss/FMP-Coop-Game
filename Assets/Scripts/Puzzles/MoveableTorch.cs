using UnityEngine;

public class MoveableTorch : InteractObjectBase, IMoveable
{
    public override bool isInteractable => true;


    private bool _beingCarried;

    [SerializeField] private float _carryForce = 10f;

    public void OnDragStart(Vector3 pos)
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

    public void OnDrag(Vector3 worldPos)
    {

        if (!_beingCarried)
        {
            return;
        }

        rb.linearVelocity = (worldPos - rb.position) * _carryForce; ;
    }



    public void OnDragEnd()
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
