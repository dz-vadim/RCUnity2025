using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private uint health;
    [SerializeField] private float speed; 
    [SerializeField] private float acceleration;
    [SerializeField] private int coinsForKill;
    public uint GetHealth()
    {
        return health;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetAcceleration()
    {
        return acceleration;
    }
    
    public void Damage(uint damageValue)
    {
        if (damageValue > health)
        {
            return;
        }
        health -= damageValue;
        if (health <= 0)
        {
            FindObjectOfType<CoinController>().EarnCoin(coinsForKill);
            Destroy(gameObject);
        }
    }
}
