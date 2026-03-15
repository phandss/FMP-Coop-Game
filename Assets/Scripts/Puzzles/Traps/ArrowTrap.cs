using UnityEngine;

public class ArrowTrap : TrapBase
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private float _projSpeed = 10f;

    [SerializeField] private Color _color = Color.red;
    [SerializeField] private float _length = 2f;

    public override void Activate()
    {
        if(_arrowPrefab == null)
        {
            Debug.LogError("ArrowTrap: Missing arrow prefab or spawn point.");
            return;
        }

        GameObject arrow = Instantiate(_arrowPrefab, transform.position, transform.rotation);
        ArrowProjectile projectile = arrow.GetComponent<ArrowProjectile>();
        projectile.Fire(transform.forward, _projSpeed);
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
