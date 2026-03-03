using UnityEngine;

public class TestBox : InteractObjectBase, IMoveable
{
    public override bool isInteractable => true;


    [SerializeField] private float _carryForce = 10f;

    public void OnDragStart(Vector3 pos)
    {

        if (!AttemptInteractLock())
        {
            return;
        }



        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
        rb.linearDamping = 10f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

    }

    public void OnDrag(Vector3 worldPos)
    {

        if(!isInteractLocked)
        {
            return;
        }

        rb.linearVelocity = (worldPos - rb.position) * _carryForce; ;
    }



    public void OnDragEnd()
    {
        if (!isInteractLocked)
        {
            return;
        }
        rb.useGravity = true;
        rb.linearDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.linearVelocity = Vector3.zero;
        UnlockInteract();

    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with box");
    }


}