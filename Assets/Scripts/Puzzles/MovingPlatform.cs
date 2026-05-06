using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed = 2f;

    private Transform origin;
    private Transform currentTarget;
    private CharacterController _player;
    private Vector3 _lastpos;

    private int targetIndex = 1;

    private bool isMoving = false;
    private bool isReturning = false;



    private void Start()
    {
        origin = _waypointPath.GetWayPoint(0);
        currentTarget = _waypointPath.GetWayPoint(targetIndex);

    }

    private void Update()
    {
        if (!isMoving) return;

        _lastpos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, _speed * Time.deltaTime);

        if (_player != null)
        {
            Vector3 movement = transform.position - _lastpos;
            _player.Move(movement);
        }

        if (transform.position == currentTarget.position)
        {
            int nextIndex = _waypointPath.GetNextWaypointIndex(targetIndex);

            if (nextIndex <= targetIndex)
            {
                // reached the final waypoint, stop
                isMoving = false;
                return;
            }

            targetIndex = nextIndex;
            currentTarget = _waypointPath.GetWayPoint(targetIndex);
        }
    }



    public void PlatformReset()
    {
        Debug.Log("Resetting platform to origin");
        transform.position = origin.position;
        targetIndex = 1;
        currentTarget = _waypointPath.GetWayPoint(targetIndex);
        isMoving = false;
        isReturning = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        _player = other.GetComponent<CharacterController>();
        isReturning = false;
        isMoving = true;
    }

    private void OnTriggerExit(Collider other)
    {

        _player = null;

    }
}
