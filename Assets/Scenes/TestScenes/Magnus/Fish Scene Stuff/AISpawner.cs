using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AIObjects
{







    public string AIGroupName   { get { return m_aiGroupName; } }
    public GameObject objectPrefab{ get { return m_prefab ; } }
    public int maxAI { get { return m_maxAI; } }
    public int spawnRate { get { return m_spawnRate; } }
    public int spawnAmount { get { return m_maxSpawnAmount; } }
    public bool randomizeStats { get { return m_randomizeStats; } }

    public bool enableSpawner{ get { return m_enableSpawner; } }

    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;

    [SerializeField]
    private GameObject m_prefab;

    [Range(0f, 100f)]
    [SerializeField]
    private int m_maxAI;

    [Range(0f, 90f)]
    [SerializeField]
    private int m_spawnRate;

    [Range(0f, 90f)]
    [SerializeField]
    private int m_maxSpawnAmount;

    [SerializeField]
    private bool m_randomizeStats;


    [Header("Main Settings")]
    [SerializeField]
    private bool m_enableSpawner;


    public AIObjects (string Name, GameObject Prefab, int MaxAI, int SpawnRate, int SpawnAmount, bool RandomizeStats)
    {
        this.m_aiGroupName = Name;
        this.m_prefab = Prefab;
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_maxSpawnAmount = SpawnAmount;
        this.m_randomizeStats = randomizeStats;
    }

    public void setValues(int MaxAI, int SpawnRate, int SpawnAmount)
    {
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_maxSpawnAmount = SpawnAmount;
    }
}

public class AISpawner : MonoBehaviour
{
    public List<Transform> Waypoints = new List< Transform > ();

    public float spawnTimer { get { return m_SpawnTimer; } }
    public Vector3 spawnArea { get { return m_SpawnArea; } }

    [Header("Global Stats")]
    [Range(0f, 600f)]
    [SerializeField]
    private float m_SpawnTimer;
    [SerializeField]
    private Color m_SpawnColor = new Color(1.000f, 0.000f, 0.000f, 0.300f);
    [SerializeField]
    private Vector3 m_SpawnArea = new Vector3 (20f,10f,20f);




    public Transform[] WaypointsLinq;

    [Header("AI Groups Settings")]
    public AIObjects[] AIObject = new AIObjects[5];

    //Empty Game Object to keep our AI in 
    private GameObject m_AIGroupSpawn;



    void Start()
    {
        GetWaypoints();
        RandomiseGroups();
        CreateAIGroups();
        InvokeRepeating("SpawnNPC", 0.5f, spawnTimer);
    }

    
    void Update()
    {
        
    }

    void SpawnNPC()
    {
        for (int i = 0; i< AIObject.Count(); i++)
        {
            if (AIObject[i].enableSpawner && AIObject[i].objectPrefab != null)
            {
                GameObject tempGroup = GameObject.Find(AIObject[i].AIGroupName);
                if(tempGroup.GetComponentInChildren<Transform>().childCount <   AIObject[i].maxAI)
                {
                    for (int y=0; y< Random.Range(0,AIObject[i].spawnAmount); y++)
                    {
                Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);

                GameObject tempSpawn;
                tempSpawn = Instantiate(AIObject[i].objectPrefab, RandomPosition(), randomRotation);

                tempSpawn.transform.parent = tempGroup.transform;

                tempSpawn.AddComponent<AIMove>();
                    }
                }
            }
    }
}

public Vector3 RandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z)
            );
        randomPosition = transform.TransformPoint(randomPosition * .5f);
        return randomPosition;
    }

    public Vector3 RandomWaypoint()
    {
        int randomWP = Random.Range(0, (Waypoints.Count - 1));
        Vector3 randomWaypoint = Waypoints[randomWP].transform.position;
        return randomWaypoint;
    }


/// <summary>
///  Methode um zuf�llige Eigenschaften in die Gruppen zu bef�llen
/// </summary>
void RandomiseGroups()
    {
        for (int i = 0; i < AIObject.Count(); i++)
        {
            if( AIObject[i].randomizeStats)
            {
                //AIObject[i].maxAI = Random.Range(1, 30);
                // AIObject[i] = new AIObjects  (AIObject[i].AIGroupName, AIObject[i].objectPrefab, Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10), AIObject[i].randomizeStats);

                AIObject[i].setValues(Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10));
            }
        }
    }


    // Method for creating the empty worldObject groups
    void CreateAIGroups()
    {
        for (int i = 0; i < AIObject.Count(); i++ )
        {
            // Create a new game Object
            m_AIGroupSpawn = new GameObject(AIObject[i].AIGroupName);
            m_AIGroupSpawn.transform.parent = this.gameObject.transform;
        }
    }

    void GetWaypoints()
    {
        Transform[] wpList = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i <wpList.Length; i++)
        {
            if (wpList[i].tag == "Waypoint")
            {
                Waypoints.Add(wpList[i]);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_SpawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }



}
