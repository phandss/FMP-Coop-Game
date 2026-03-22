using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    [SerializeField] private InteractDoor doorToLock;

    [SerializeField] private int CheckPointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorToLock.OnLockTrigger();
            CheckpointManager.Instance.SetCheckpoint(CheckPointIndex);
        }
    }
}
