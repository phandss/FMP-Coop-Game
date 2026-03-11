using UnityEngine;

public class ArrowTrap : TrapBase
{
    [SerializeField] private Transform _arrowSpawnPoint;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private float _projSpeed = 10f;


    public override void Activate()
    {
        if(_arrowPrefab == null || _arrowSpawnPoint == null)
        {
            Debug.LogError("ArrowTrap: Missing arrow prefab or spawn point.");
            return;
        }

        GameObject arrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, _arrowSpawnPoint.rotation);
        ArrowProjectile projectile = arrow.GetComponent<ArrowProjectile>();
        projectile.Fire(_arrowSpawnPoint.forward, _projSpeed);
    }
}
