using System;
using UnityEngine;
public class MobileControll : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    [SerializeField] private GameObject bulletPrefab;
    public bool moveLeft = false;
    public bool moveRight = false;

    private void Update()
    {
        if (moveLeft)
        {
            MoveLeft();
        }

        if (moveRight)
        {
            MoveRight();
        }
    }

    public void SpawnBullet()
    {
        Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
    }
    private void Move(float dir)
    {
        transform.Translate(dir * Time.fixedDeltaTime, 0f, 0f);
    }
    public void MoveLeft()
    {
        Move(-carSpeed);
    }
    public void MoveRight()
    {
        Move(carSpeed);
    }
    
    
}
