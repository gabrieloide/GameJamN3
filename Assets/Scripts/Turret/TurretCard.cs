using System.Collections;
using UnityEngine;

public abstract class TurretCard : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    [SerializeField] protected TowersData towersData;
    public float BulletPen
    {
        get { return towersData.bulletPen; }
        set { towersData.bulletPen = value; }
    }
    public float Damage
    {
        get { return towersData.damage; }
        set { towersData.damage = value; }
    }
    [SerializeField] public float fireRateCountDown = 0f;

    [HideInInspector] protected Transform target;

    [SerializeField] protected Transform nozzle;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float range = 3f;
    [SerializeField] protected GameObject BulletParticle;
    [SerializeField] GameObject TurretLifeSlider;
    [SerializeField] float TurretLifeSliderOffset;
    LayerMask NormalCardLM() => LayerMask.GetMask("Path");
    public float timeToDestroy = 30f;

    protected void Start()
    {
        StartCoroutine(UpdateTargets());
        Instantiate(TurretLifeSlider, transform.position + new Vector3(default
                                                                , TurretLifeSliderOffset,
                                                                  default),
                                                                  Quaternion.identity, transform);
        Destroy(gameObject, timeToDestroy);
        ChangeTurretDirection();
    }
    IEnumerator UpdateTargets()
    {
        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
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
            yield return new WaitForSeconds(0.5f);
        }
    }
    protected void Update()
    {
        fireRateCountDown -= Time.deltaTime;
        if (target == null)
            return;

        if (fireRateCountDown <= 0f)
        {
            fireRateCountDown = 1f / towersData.fireRate;
            TurretShoot();
        }
    }
    void ChangeTurretDirection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, NormalCardLM());
        transform.localScale = hit ? Vector2.one : new Vector2(-1, 1);
    }
    public abstract void TurretShoot();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}