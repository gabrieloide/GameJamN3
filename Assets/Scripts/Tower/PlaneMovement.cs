using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    bool spawned;
    private void Start()
    {
        Destroy(gameObject, 15);
        spawned = true;
    }
    void Update()
    {
        PMovement();
        float d = Vector2.Distance(transform.position, FindObjectOfType<AirAttack>().target);
        if (d < 9)
        {
            if (spawned)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                spawned = false;
            }
        }
    }
    void PMovement()
    {
        //Avion moviendose
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }
}
