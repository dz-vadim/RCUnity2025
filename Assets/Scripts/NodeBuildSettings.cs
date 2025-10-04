using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuildSettings : MonoBehaviour
{
    private GameObject structure;

    public GameObject StructureInfo()
    {
        return structure;
    }

    public void StartBuild(GameObject[] structures, int structureIndex, float height)
    {
        if (!structure)
        {
            Vector3 position = transform.position;
            position.y += height;
            structure = Instantiate(structures[structureIndex], 
                position, Quaternion.identity);
        }
    }
}
