using UnityEngine;

public class TowerD : MonoBehaviour
{
    public TowersData towersData;

    [HideInInspector] public Transform target;
    public Transform nozzle;
    private string enemyTag = "Enemy";
    [HideInInspector] public int damage;
    public float range = 3f;
    public float fireRateCountDown = 0f;

    public GameObject bulletPrefab;
    [SerializeField] GameObject BulletParticle;
    public float timeToDestroy = 30f;
    [SerializeField] GameObject TurretLifeSlider;
    [SerializeField] float TurretLifeSliderOffset;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        Instantiate(TurretLifeSlider, transform.position + new Vector3(default
                                                                , TurretLifeSliderOffset,
                                                                  default), Quaternion.identity, transform);
        Destroy(gameObject, timeToDestroy);
        damage = towersData.damage;
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {
        fireRateCountDown -= Time.deltaTime;

        if (target == null)
            return;

        if (fireRateCountDown <= 0f)
        {
            fireRateCountDown = 1f / towersData.fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(BulletParticle, nozzle.position, transform.rotation);
        //SFX PARA CADA VEZ QUE LA TORRETA DISPARA

        TowerBullet bullet = ObjectPooling.instance.Shoot().GetComponent<TowerBullet>();
        bullet.transform.position = nozzle.position;

        Vector3 relativePos = target.position - nozzle.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.GetData(target, damage, towersData.lifeBullet);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
