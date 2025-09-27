using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private Transform target; //змінна для збергіання розміщення цілі
    [SerializeField] private float range; //змінна для налаштування дальності
    
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject[] gunBarrel;
    [SerializeField] private float countdown;
    private bool isSecondBarrel = false;
    private bool canShoot = true;
    
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
            if (canShoot)
            {
                if (isSecondBarrel) StartCoroutine(Shoot(0));
                else StartCoroutine(Shoot(1));
                canShoot = !canShoot;
            }
        }
    }
    IEnumerator Shoot(int barrelNumber)
    {
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel[barrelNumber].transform);
        bullet.transform.position = gunBarrel[barrelNumber].transform.position;
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.TakeForce(target);
        bullet.transform.parent = null;
        isSecondBarrel = !isSecondBarrel;
        
        yield return new WaitForSeconds(countdown);
        canShoot = !canShoot;
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
