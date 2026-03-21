using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    [SerializeField] private InteractDoor doorToLock;
    [SerializeField]private string playerTag = "Player";
    [SerializeField] private int CheckPointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            doorToLock.OnLockTrigger();
            CheckpointManager.Instance.SetCheckpoint(CheckPointIndex);
        }
    }
}
