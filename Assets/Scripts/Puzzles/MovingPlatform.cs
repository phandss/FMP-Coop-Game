using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed = 2f;

    private Transform origin;
    private Transform currentTarget;

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
        if(!isMoving)
        {
            return;
        }

        Transform destination;

        if(isReturning)
        {
            destination = _waypointPath.GetWayPoint(0);
        }
        else
        {
            destination = currentTarget;
        }
        //move towards destination
        transform.position = Vector3.MoveTowards(transform.position, destination.position, _speed * Time.deltaTime);

        if(transform.position == destination.position)
        {
            if(isReturning)
            {
                //reset to origin
                isReturning = false;
                isMoving = false;
            }
            else
            {
                //move to next waypoint
                targetIndex = _waypointPath.GetNextWaypointIndex(targetIndex);
                currentTarget = _waypointPath.GetWayPoint(targetIndex);
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
        isReturning = false;
        isMoving = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
        isReturning = true;
    }
}
