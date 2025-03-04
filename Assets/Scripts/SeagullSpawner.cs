using UnityEngine;
using System.Collections.Generic;

public class SeagullSpawner : MonoBehaviour
{
    public GameObject seagullPrefab; // Prefab for the seagulls
    private List<Vector3> spawnPositions = new List<Vector3>(); // Original spawn positions
    private List<GameObject> currentSeagulls = new List<GameObject>(); // Active seagulls list

    void Start()
    {
        // Record all initial child positions in the spawner object
        foreach (Transform child in transform)
        {
            spawnPositions.Add(child.position);

            // Instantiate seagulls at initial positions, only if they don't already exist
            GameObject seagull = Instantiate(seagullPrefab, child.position, Quaternion.identity, transform);
            currentSeagulls.Add(seagull);
        }
    }

    public void RespawnSeagulls()
    {
        // Destroy old seagulls only if they exist
        foreach (GameObject seagull in currentSeagulls)
        {
            if (seagull != null) Destroy(seagull);
        }

        currentSeagulls.Clear();

        // Respawn seagulls at their original positions
        foreach (Vector3 position in spawnPositions)
        {
            GameObject seagull = Instantiate(seagullPrefab, position, Quaternion.identity, transform);
            currentSeagulls.Add(seagull);
        }
    }
}
