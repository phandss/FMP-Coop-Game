using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] HumanHealth health;
    [SerializeField] private Transform[] checkpoints;
}
