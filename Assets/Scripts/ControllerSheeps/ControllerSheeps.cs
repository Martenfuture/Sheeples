using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerSheeps : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject SheepPrefab;
    public GameObject ParticlePrefab;
    public int SheepCount;

    public List<GameObject> SheepList1;
    public List<GameObject> SheepList2;
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
                foreach (GameObject sheep in SheepListSelected) sheep.GetComponent<NavMeshAgent>().SetDestination(mouseClickPosition);
                StartCoroutine(ParticleSleep());
                Debug.Log(mouseClickPosition);
            }
        }
    }

    public void SplitSheepList()
    {
        int i = 0;
        List<GameObject> _Sheeplist1 = new List<GameObject>();
        foreach (GameObject sheep in SheepList1)
        {
            if (i > SheepList1.Count / 2) SheepList2.Add(sheep);
            else _Sheeplist1.Add(sheep);
            i++;
        }

        SheepList1 = _Sheeplist1;
    }

    public void SelectSheepList1()
    {
        SheepListSelected = SheepList1;
    }

    public void SelectSheepList2()
    {
        SheepListSelected = SheepList2;
    }

    IEnumerator ParticleSleep()
    {
        GameObject particleEffect = Instantiate(ParticlePrefab, mouseClickPosition, Quaternion.identity, transform);

        yield return new WaitForSeconds(5);

        Destroy(particleEffect);
    }
}
