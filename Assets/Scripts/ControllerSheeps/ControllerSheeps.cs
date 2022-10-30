using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerSheeps : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject SheepPrefab;
    public int SheepCount;

    public List<GameObject> SheepList1;
    List<GameObject> SheepList2;
    List<GameObject> SheepListSelected;

    Ray ray;
    RaycastHit hitData;
    Vector3 mouseClickPosition;

    private void Start()
    {
        for (int i = 0; i < SheepCount; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5));
            GameObject sheep = Instantiate(SheepPrefab, spawnPosition, Quaternion.identity, transform);
            SheepList1.Add(sheep);
        }
        SheepListSelected = SheepList1;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitData, 1000, layerMask))
            {
                mouseClickPosition = hitData.point;
                foreach (GameObject sheep in SheepList1) sheep.GetComponent<NavMeshAgent>().SetDestination(mouseClickPosition);
                Debug.Log(mouseClickPosition);
            }
        }
    }
}
