using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private float speed;
    private float damage;
    private float lifeBullet;
    private Transform target;
    private Rigidbody2D RB2d;
    private Vector2 dir;

    private void Start()
    {
        RB2d = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1.5f);
    }

    public void GetData(Transform _target, float _damage, float _lifeBullet)
    {
        target = _target;
        damage = _damage;
        lifeBullet = _lifeBullet;
        dir =  target.position -transform.position;
    }

    private void FixedUpdate()
    {
        RB2d.velocity = dir.normalized * speed;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}