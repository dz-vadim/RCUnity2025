using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private Transform target; //змінна для збергіання розміщення цілі
    [SerializeField] private float range; //змінна для налаштування дальності

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Start()
    {
        InvokeRepeating(nameof(FindTarget), 0f, 0.3f);
    }
    private void Update()
    {
        if (target)
        {
            transform.LookAt(target);
        }
    }
    private void FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject currentTarget = null;
        float distance = Mathf.Infinity;
        foreach (var target in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToEnemy < distance)
            {
                distance = distanceToEnemy;
                currentTarget = target;
            }
        }

        if (distance < range && currentTarget)
        {
            this.target = currentTarget.transform;
        }
        else
        {
            this.target = null;
        }
    }
}
