using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSheepTrigger : MonoBehaviour
{
    public List<GameObject> colliderObjects;
    bool barking;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sheep")
        {
            SheepManager.instance.SetTargetDirection(transform.position, other.GetComponent<SheepAgent>().sheepGroupId, false);

            if (!colliderObjects.Contains(other.gameObject))
            {
                colliderObjects.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colliderObjects.Contains(other.gameObject) && other.tag == "Sheep")
        {
            colliderObjects.Remove(other.gameObject);
        }
    }


    public void DogBark()
    {
        if (!barking)
        {
            GetComponent<SphereCollider>().radius *= 2;
            barking = true;
            StartCoroutine(BarkDelay());
        }
    }

    IEnumerator BarkDelay()
    {
        yield return new WaitForSeconds(0.5f);
        List<int> SheepGroupIndexList = new List<int>();
        List<GameObject> SheepGroupList = new List<GameObject>();
        foreach (GameObject obj in colliderObjects)
        {
            int SheepGroupIndex = obj.GetComponent<SheepAgent>().sheepGroupId;
            if (!SheepGroupIndexList.Contains(SheepGroupIndex))
            {
                SheepGroupIndexList.Add(SheepGroupIndex);
                SheepGroupList.Add(obj);
            }
        }

        foreach (GameObject obj in SheepGroupList)
        {
            SheepManager.instance.MergeSheepGroup(obj.GetComponent<SheepAgent>().sheepGroupId);
        }
        GetComponent<SphereCollider>().radius /= 2;
        barking = false;
    }
}
