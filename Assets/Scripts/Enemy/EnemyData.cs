using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    //Escribir mas stats de enemigos
    public float Life;
    public int score;
    public float Damage;
    public float Defense;
    public float MoveSpeed;
}
