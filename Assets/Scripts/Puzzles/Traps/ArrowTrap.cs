using UnityEngine;

public class ArrowTrap : TrapBase
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private float _projSpeed = 10f;
    [SerializeField] private int _spawnAmount = 3;
    [SerializeField] private int spacing = 1;

    [SerializeField] private Color _color = Color.red;
    [SerializeField] private float _length = 2f;

    private Bounds _bounds;

    private void Awake()
    {
        _bounds = GetComponent<Collider>().bounds;
    }

    public override void Activate()
    {
        if(_arrowPrefab == null)
        {
            Debug.LogError("ArrowTrap: Missing arrow prefab or spawn point.");
            return;
        }


        for (int i = 0; i < _spawnAmount; i++)
        {
            float offset = (i - (_spawnAmount - 1) / 2f) * spacing;
            Vector3 spawnPos = transform.position + transform.up * offset;
            GameObject arrow = Instantiate(_arrowPrefab, spawnPos, transform.rotation);
            arrow.GetComponent<ArrowProjectile>().Fire(transform.forward, _projSpeed);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        Vector3 start = transform.position;
        Vector3 end = start + transform.forward * _length;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, _length * 0.1f);
    }


}
