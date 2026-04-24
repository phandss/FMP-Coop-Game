using Unity.Cinemachine;
using UnityEngine;


public class CameraChangeZone : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0, 5, -10);

    private CinemachineFollow _follow;

    private void Awake()
    {
        _follow = FindAnyObjectByType<CinemachineCamera>().GetComponent<CinemachineFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _follow.FollowOffset = _cameraOffset;
        }
    }
}
