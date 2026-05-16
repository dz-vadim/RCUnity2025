using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed;
    [SerializeField] private int price;
    [SerializeField] private GameObject coinFx;
    private MeshRenderer mesh;
    private SphereCollider coinCollider;

    void Awake()
    {
        coinCollider = GetComponent<SphereCollider>();
        mesh = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
    
    public int GetPrice()
    {
        return price;
    }

    private void Respawn()
    {
        mesh.enabled = true;
        coinCollider.enabled = true;
    }

    public void PickUp()
    {
        mesh.enabled = false;
        coinCollider.enabled = false;
        int randTime = Random.Range(5, 8);
        Instantiate(coinFx,transform.position,Quaternion.identity);
        Invoke("Respawn", randTime);
    }
}
