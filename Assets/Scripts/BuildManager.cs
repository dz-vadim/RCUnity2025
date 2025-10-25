using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private Color hoverColor = Color.blue;
    [SerializeField] private Color startColor;
    private GameObject tempObject;
    [SerializeField] private GameObject[] turrets;
    private bool canBuild = false;
    private int turretIndex;
    private int cost;

    private void Start()
    {
        startColor = GameObject.FindGameObjectWithTag("Node").
            GetComponent<MeshRenderer>().material.color;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Node" && canBuild)
            {
                if (tempObject)
                {
                    tempObject.GetComponent<MeshRenderer>().material.color = startColor;
                }
                tempObject = hit.collider.gameObject;
                tempObject.GetComponent<MeshRenderer>().material.color = hoverColor;
                if (Input.GetMouseButtonDown(0))
                {
                    tempObject.GetComponent<NodeBuildSettings>().StartBuild(turrets, turretIndex, 0.34f, cost);
                    canBuild = false;
                }
            }
        }
    }

    public void SetBuildTurret(int buildIndex, int buildCost)
    {
        turretIndex = buildIndex;
        cost = buildCost;
        canBuild = true;
    }
}
