using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] private HumanHealth health;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private Transform playerPos;

    private int _currentCheckpointIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;



        health.OnDeath += RespawnAtCheckpoint;

    }

    public void SetCheckpoint(int index)
    {
        _currentCheckpointIndex = index;
        Debug.Log($"Checkpoint set to index: {_currentCheckpointIndex}");
    }


    private void RespawnAtCheckpoint()
    {
        Debug.Log($"Respawning at checkpoint index: {_currentCheckpointIndex}");
        if (_currentCheckpointIndex < 0 || _currentCheckpointIndex >= respawnPoints.Length)
        {
            Debug.LogWarning("Invalid checkpoint index. Respawning at the first checkpoint.");
            _currentCheckpointIndex = 0;
        }
        var cc = playerPos.GetComponent<CharacterController>();
        if(cc != null)
        {
            cc.enabled = false; // Disable CharacterController to avoid physics issues during teleportation
        }
        playerPos.position = respawnPoints[_currentCheckpointIndex].position;
        if (cc != null)
        {
            cc.enabled = true; // Re-enable CharacterController after teleportation
        }

        health.Respawn();
    }

    private void Update()
    {
        Debug.Log($"Current checkpoint index: {_currentCheckpointIndex}");
    }
}

