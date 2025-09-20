using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMovement : MonoBehaviour
{
    private Vector3 _finishPoint;
    private NavMeshAgent _agent;
    
    private EnemySettings _settings;
    private void Awake()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _finishPoint = GameObject.FindGameObjectWithTag("Finish").gameObject.transform.position;
    }
    private void Start()
    {
        _agent.destination = _finishPoint;
        _settings = GetComponent<EnemySettings>();
        _agent.speed = _settings.GetSpeed();
        _agent.acceleration = _settings.GetAcceleration();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
