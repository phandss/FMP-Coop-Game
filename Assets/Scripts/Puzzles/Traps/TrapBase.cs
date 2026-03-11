using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    [SerializeField] public bool canReactivate = false;

    public abstract void Activate();

    public virtual void Deactivate()
    {
        
    }
}
