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

    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;

    [SerializeField]
    private GameObject m_prefab;

    [Range(0f, 30f)]
    [SerializeField]
    private int m_maxAI;

    [Range(0f, 20f)]
    [SerializeField]
    private int m_spawnRate;

    [Range(0f, 10f)]
    [SerializeField]
    private int m_maxSpawnAmount;

    [SerializeField]
    private bool m_randomizeStats;


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
    }

    
    void Update()
    {
        
    }
    /// <summary>
    ///  Methode um zufällige Eigenschaften in die Gruppen zu befüllen
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
}
