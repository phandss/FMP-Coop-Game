using UnityEngine;


public abstract class InteractObjectBase : MonoBehaviour, IInteractable
{
    public abstract bool isInteractable { get; }

    protected bool isInteractLocked {get; private set;}

    protected Rigidbody rb { get; private set; }



    protected bool AttemptInteractLock()
    {
        if (isInteractLocked)
        {
            return false;
        }
        isInteractLocked = true;
        return true;

    }

    protected void UnlockInteract() => isInteractLocked = false;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();


    }


    public virtual void OnInteract()
    {

    }



    public virtual void OnHoverEnter(string buttonPrompt)
    {
        InteractPromptUI.Instance?.Show(buttonPrompt, transform);
    }



    public virtual void OnHoverExit()
    {
        InteractPromptUI.Instance?.Hide();
    }
}
