using UnityEngine;

public class CheckPointPointer : MonoBehaviour
{
    [SerializeField] private int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(checkpointIndex);
            Debug.Log("Checkpoint:"+ checkpointIndex+" reached!");
        }
    }
}
