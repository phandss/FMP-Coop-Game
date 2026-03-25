using UnityEngine;

public class InteractSwitch : InteractObjectBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private InteractDoor door;
    [SerializeField] private string _animParam = "LeverUp";



    public override bool isInteractable
    {   
        get { return !isInteractLocked; }
    }

    public override void OnInteract()
    {
        Debug.Log("Switch attempt open");
        if (!AttemptInteractLock())
        { 
            return;
        }

        if(animator != null)
        {
            animator.SetBool(_animParam, true);
        }

        if(door != null)
        {
            door.SwitchOpen();
            Debug.Log("Switch opened door");
        }
    }


}
