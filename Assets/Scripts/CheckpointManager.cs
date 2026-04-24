using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] private HumanHealth health;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private Transform playerPos;

    private static int _savedCheckpointIndex = 0;
    private int _currentCheckpointIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _currentCheckpointIndex = _savedCheckpointIndex;

        health.OnDeath += RespawnAtCheckpoint;

    }

    public void SetCheckpoint(int index)
    {
        _currentCheckpointIndex = index;
        _savedCheckpointIndex = index;

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
            cc.enabled = false; 
        }
        playerPos.position = respawnPoints[_currentCheckpointIndex].position;
        if (cc != null)
        {
            cc.enabled = true; 
        }

        health.Respawn();
    }


}

