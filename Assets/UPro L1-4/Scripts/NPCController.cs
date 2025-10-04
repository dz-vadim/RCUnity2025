using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent  agent;
    private Transform destination;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = GameObject.FindGameObjectWithTag("Finish").transform;
        agent.SetDestination(destination.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
