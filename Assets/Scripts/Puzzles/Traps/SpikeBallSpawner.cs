using UnityEngine;

public class SpikeBallSpawner : TrapBase
{
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject SpikeBallPrefab;



    public override void Activate()
    {

        if (_spawnPos == null || SpikeBallPrefab == null)
        {
            return;
        }
        GameObject ballPrefab = Instantiate(SpikeBallPrefab, _spawnPos.position, _spawnPos.rotation);
    }

}
