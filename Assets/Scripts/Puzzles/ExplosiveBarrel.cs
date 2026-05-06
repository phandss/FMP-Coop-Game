using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject Barrel, Explosion;

    private AudioSource source;

    [SerializeField]private float range;
    [SerializeField]private float _maxDamage;
    [SerializeField] private float _explosionThreshold;

    [SerializeField] private float _vfxDuration = 3f;


    private bool _hasExploded = false;

    private void Awake()
    {
        Barrel.SetActive(true);
        Explosion.SetActive(false);

        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasExploded)
        {
            return;
        }
        if (collision.relativeVelocity.magnitude >= _explosionThreshold)
        {
            Explode();
            _hasExploded = true;
        }
    }


    public void Explode()
    {
        _hasExploded = true;

        Barrel.SetActive(false);
        Explosion.SetActive(true);

        source.spatialBlend = 1f;
        source.Play();

        Collider[] players = Physics.OverlapSphere(transform.position, range);

        foreach (Collider player in players)
        {
            HumanHealth health = player.GetComponent<HumanHealth>();

            if(health != null)
            {
                float falloff = 1f - Mathf.Clamp01(Vector3.Distance(transform.position, player.transform.position) / range);

                health.TakeDamage(_maxDamage * falloff);
            }
        }

        Destroy(gameObject, _vfxDuration);
    }

    private void Update()
    {
        
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, range);
    //}
}
