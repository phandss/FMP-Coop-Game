using UnityEngine;

public class InteractSwitch : InteractObjectBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private InteractDoor door;
    [SerializeField] private string _animParam = "LeverUp";

    private bool switchState;

    public override bool isInteractable
    {
        get { return isInteractLocked; }
    }


    public override void OnInteract()
    {
        if (!AttemptInteractLock())
        {
            return;
        }

        switchState = !switchState;

        if (animator)
        {
            animator.SetBool(_animParam, switchState);
        }

        if(door != null)
        {
            if (switchState)
            {
                //door.OnSwitchActivate();

                Debug.Log("Switch activated");
            }
            
        }
    }


}
