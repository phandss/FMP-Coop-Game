using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
public abstract class InteractObjectBase : MonoBehaviour, IInteractable
{
    public abstract bool isInteractable { get; }
    public abstract bool isDraggable { get; }

    protected bool isInteractLocked {get; private set;}

    protected Rigidbody rb { get; private set; }

    private InteractPromptUI _prompt;

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
        _prompt = GetComponent<InteractPromptUI>();

    }


    public virtual void OnInteract()
    {

    }

    public virtual void OnDragStart(Vector3 hitpoint)
    {

    }

    public virtual void OnDrag(Vector3 worldPos)
    {
        
    }

    public virtual void OnDragEnd()
    {
        
    }

    public virtual void OnHoverEnter(string buttonPrompt)
    {
        _prompt?.Show(buttonPrompt);
    }



    public virtual void OnHoverExit()
    {
        _prompt?.Hide();
    }
}
