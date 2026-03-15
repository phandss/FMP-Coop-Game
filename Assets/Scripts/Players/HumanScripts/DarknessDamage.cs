using FischlWorks_FogWar;
using UnityEngine;

public class DarknessDamage : MonoBehaviour
{
    [SerializeField] private csFogWar _warFog;
    [SerializeField] private float _damagePerSecond = 10f;
    [SerializeField] private float _damageInterval = 1f;

    private HumanHealth _humanHealth;
    private float _damageTimer;


    private void Awake()
    {
        _humanHealth = GetComponent<HumanHealth>();
        //_warFog = GetComponent<csFogWar>();

    }

    private void Update()
    {
        if(_warFog == null || _humanHealth == null || _humanHealth.IsDead || _warFog.CheckVisibility(transform.position, 0))
        {
            return;
        }

        if(!_warFog.CheckWorldGridRange(transform.position))
        {
            return;
        }

        _damageTimer += Time.deltaTime;

        if(_damageTimer >= _damageInterval)
        {
            _humanHealth.TakeDamage(_damagePerSecond);
            _damageTimer = 0f;
        }
    }

    
}
