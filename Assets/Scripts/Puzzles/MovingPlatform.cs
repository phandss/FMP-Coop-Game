using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed;
    
    private int _targetWaypointIndex;

    private Transform _prevWaypoint;
    private Transform _targetWaypoint;

    private float _timeToNextWaypoint;
    private float _elapsedTime;

    private void Start()
    {
        TargetNextWaypoint();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        float elapsedPercentage = _elapsedTime / _timeToNextWaypoint;
        transform.position = Vector3.Lerp(_prevWaypoint.position, _targetWaypoint.position, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        _prevWaypoint = _waypointPath.GetWayPoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWayPoint(_targetWaypointIndex);
        
        _elapsedTime = 0f;

        float distanceToNextWaypoint = Vector3.Distance(_prevWaypoint.position, _targetWaypoint.position);

        _timeToNextWaypoint = distanceToNextWaypoint / _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
