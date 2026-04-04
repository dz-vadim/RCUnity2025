using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefab;
    Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
        InvokeRepeating(nameof(SpawnItem), 0, 1f);
    }

    private void SpawnItem()
    {
        int index = Random.Range(0, itemPrefab.Length);
        spawnPosition.x = Random.Range(-1.5f, 1.5f);
        GameObject item = Instantiate(itemPrefab[index], spawnPosition, Quaternion.identity);
        Rigidbody rb = item.GetComponent<Rigidbody>();
        Vector3 direction = transform.position + Vector3.up * 10f;
        direction -= spawnPosition;
        rb.AddForce(direction, ForceMode.Impulse);
        rb.angularVelocity = Random.insideUnitSphere * 5f;
    }
}
