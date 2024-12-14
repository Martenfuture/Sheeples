using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MainMenuAreaSheepSpawner : MonoBehaviour
{
    public int sheepCount;
    public GameObject agentPrefab;
    public GameObject spawArea;
    public float baseSpeed;

    public Texture[] sheepTextures;
    public float[] sheepTexturesWeight;

    private List<GameObject> sheepList = new List<GameObject>();



    private void Start()
    {
        for (int i = 0; i < sheepCount; i++)
        {
            Vector3 spawnPosition = RandomPointInBounds(spawArea.GetComponent<Collider>().bounds);
            GameObject newAgent = Instantiate(
                agentPrefab,
                spawnPosition,
                Quaternion.Euler(Vector3.up * Random.Range(0, 0)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.GetComponent<NavMeshAgent>().speed = baseSpeed + Random.Range(-0.5f, 1f);
            newAgent.GetComponent<SheepAgent>().sheepMeshObject.GetComponent<Renderer>().materials[0].SetTexture("_BaseMap", sheepTextures[SheepCore.RandomWeightArrayIndex(sheepTexturesWeight)]);
            sheepList.Add(newAgent);
            SetDestination(newAgent);
        }
    }
    private void Update()
    {
        foreach (GameObject sheep in sheepList)
        {
            if (sheep.GetComponent<NavMeshAgent>().remainingDistance < 1 && !sheep.GetComponent<SheepAgent>().isWaiting)
            {
                StartCoroutine(NextDestinationDelay(sheep));
            }
        }
    }

    public void SetDestination(GameObject sheep)
    {
        bool pathFound = false;
        Vector3 targePosition = RandomPointInBounds(spawArea.GetComponent<Collider>().bounds);
        NavMeshPath path = new NavMeshPath();
        while (!pathFound)
        {
            if (NavMesh.CalculatePath(sheep.transform.position, targePosition, NavMesh.AllAreas, path))
            {
                pathFound = true;
            }
            else
            {
                targePosition = RandomPointInBounds(spawArea.GetComponent<Collider>().bounds);
            }
        }
        sheep.GetComponent<NavMeshAgent>().SetDestination(targePosition);
        sheep.GetComponent<NavMeshAgent>().speed = baseSpeed + Random.Range(-0.5f, 1f);
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    IEnumerator NextDestinationDelay(GameObject sheep)
    {
        sheep.GetComponent<SheepAgent>().isWaiting = true;
        yield return new WaitForSeconds(Random.Range(0,3));
        SetDestination(sheep);
        sheep.GetComponent<SheepAgent>().isWaiting = false;
    }
}
