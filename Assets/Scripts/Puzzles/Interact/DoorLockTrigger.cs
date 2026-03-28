using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    [SerializeField] private InteractDoor doorToLock;

    [SerializeField] private int CheckPointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            CheckpointManager.Instance.SetCheckpoint(CheckPointIndex);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorToLock.OnLockTrigger();
        }
    }
}
