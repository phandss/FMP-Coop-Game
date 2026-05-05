using UnityEngine;

public class EXPBarrelSpawner : TrapBase
{

    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject EXPBarrelPrefab;
    [SerializeField] private float _spawnAmount = 5f;

    private Bounds _bounds;

    private void Awake()
    {
        _bounds = GetComponent<Collider>().bounds;
    }


    public override void Activate()
    {

        if (_spawnPos == null || EXPBarrelPrefab == null)
        {
            return;
        }


        for (int i = 0; i < _spawnAmount; i++)
        {
            float offsetX = Random.Range(-_bounds.extents.x, _bounds.extents.x);
            float offsetZ = Random.Range(-_bounds.extents.z, _bounds.extents.z);
            float offsetY = Random.Range(-_bounds.extents.y, _bounds.extents.y);

            Instantiate(EXPBarrelPrefab, _spawnPos.position + new Vector3(offsetX, offsetY, offsetZ), Quaternion.identity);
        }
    }

}

