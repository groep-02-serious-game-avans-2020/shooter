using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class TargetController : MonoBehaviour
{
    public int amountOfTargets = 5;
    public float targetOffset = 3;
    public GameObject targetPrefab;

    private List<GameObject> targets;

    void Start()
    {
        float width = (amountOfTargets - 1) * targetOffset;
        float middlePoint = width / 2;

        targets = new List<GameObject>();

        // Create a new list of spawnpoints
        List<Vector3> spawnPoints = new List<Vector3>();

        // Calculate the first spawnpoint based on the width and middlepoint
        Vector3 firstSpawnPoint = new Vector3(-middlePoint, transform.position.y, transform.position.z);
        spawnPoints.Add(firstSpawnPoint);

        // Calculate the other spawnpoints from the first one
        for(int i = 0; i < amountOfTargets -1; i++)
        {
            spawnPoints.Add(new Vector3(spawnPoints[i].x + targetOffset, transform.position.y, transform.position.z));
        }

        for(int i = 0; i < spawnPoints.Count(); i++)
        {
            // Instantiate new target
            GameObject currentTarget = Instantiate(targetPrefab, spawnPoints[i], gameObject.transform.rotation);

            // Set target as child
            currentTarget.transform.parent = gameObject.transform;

            // Set the target number
            currentTarget.GetComponent<Target>().SetTargetNumber(i + 1);

            targets.Add(currentTarget);
        }
    }

    void Update()
    {

    }
}

    
