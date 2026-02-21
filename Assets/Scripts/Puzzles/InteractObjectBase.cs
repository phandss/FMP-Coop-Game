using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
public abstract class InteractObjectBase : MonoBehaviour, IInteractable
{
    public abstract bool IsClickable { get; }
    public abstract bool IsDraggable { get; }


    protected Rigidbody rb { get; private set; }

    private InteractPromptUI _prompt;



    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _prompt = GetComponent<InteractPromptUI>();

        if(_prompt == null)
        {
            Debug.Log("no interact prompt");
        }
    }


    public virtual void OnClick()
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

    public virtual void OnHoverEnter(InputAction interactAction)
    {
        _prompt?.Show(interactAction);
    }

    public virtual void OnHoverEnter()
    {
        _prompt?.Show(null);
    }

    public virtual void OnHoverExit()
    {
        _prompt?.Hide();
    }
}
