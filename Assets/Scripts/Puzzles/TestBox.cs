using UnityEngine;

public class TestBox : InteractObjectBase
{
    public override bool isInteractable => true;
    public override bool isDraggable => true;

    private bool _beingCarried;

    public override void OnDragStart(Vector3 pos)
    {

        if (!AttemptInteractLock())
        {
            return;
        }

        _beingCarried = true;
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

    }

    public override void OnDrag(Vector3 worldPos)
    {

        if (!_beingCarried)
        {
            return;
        }

        rb.MovePosition(worldPos);
    }



    public override void OnDragEnd()
    {
        if (!_beingCarried)
        {
            return;
        }
        _beingCarried = false;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.None;
        UnlockInteract();

    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with box");
    }


}