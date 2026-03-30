using UnityEngine;

public class DestroyWall : InteractObjectBase
{
    public override bool isInteractable => true;

    public override void OnInteract()
    {
        Debug.Log("Destroying wall");
        Destroy(gameObject);
    }
}
