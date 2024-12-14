using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MainMenuPathSheepSpawner : MonoBehaviour
{
    public int sheepCount;
    public GameObject agentPrefab;
    public GameObject StartArea;
    public GameObject EndArea;
    public float baseSpeed;

    public Texture[] sheepTextures;
    public float[] sheepTexturesWeight;


    private void Start()
    {
        for (int i = 0; i < sheepCount; i++)
        {
            Vector3 spawnPosition = RandomPointInBounds(StartArea.GetComponent<Collider>().bounds);
            GameObject newAgent = Instantiate(
                agentPrefab,
                spawnPosition,
                Quaternion.Euler(Vector3.up * Random.Range(0, 0)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.GetComponent<NavMeshAgent>().speed = baseSpeed + Random.Range(-0.5f, 0.5f);
            newAgent.GetComponent<SheepAgent>().sheepMeshObject.GetComponent<Renderer>().materials[0].SetTexture("_BaseMap", sheepTextures[SheepCore.RandomWeightArrayIndex(sheepTexturesWeight)]);

            StartCoroutine(WalkDelay(Random.Range(0, sheepCount), newAgent));
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    IEnumerator WalkDelay(float delayTime, GameObject sheep)
    {
        yield return new WaitForSeconds(delayTime);

        sheep.GetComponent<NavMeshAgent>().SetDestination(RandomPointInBounds(EndArea.GetComponent<Collider>().bounds));
    }

    public void ReachEndArea(GameObject sheep)
    {
        sheep.transform.position = RandomPointInBounds(StartArea.GetComponent<Collider>().bounds);
        sheep.GetComponent<NavMeshAgent>().SetDestination(RandomPointInBounds(EndArea.GetComponent<Collider>().bounds));
    }
}
