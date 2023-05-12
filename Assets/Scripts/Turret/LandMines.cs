using UnityEngine;

public class LandMines : MonoBehaviour
{
    public TowersData LandMineData;
    [SerializeField] GameObject ExplosionParticle;
    float damage;

    private void Start()
    {
        damage = LandMineData.damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Explosion de mina de tierra
            collision.GetComponent<Enemy>().Life -= damage;
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}