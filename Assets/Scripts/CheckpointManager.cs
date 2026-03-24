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
    }


    private void RespawnAtCheckpoint()
    {
        if(_currentCheckpointIndex < 0 || _currentCheckpointIndex >= respawnPoints.Length)
        {
            Debug.LogWarning("Invalid checkpoint index. Respawning at the first checkpoint.");
            _currentCheckpointIndex = 0;
        }

        playerPos.position = respawnPoints[_currentCheckpointIndex].position ;
        health.Respawn();
    }

}

