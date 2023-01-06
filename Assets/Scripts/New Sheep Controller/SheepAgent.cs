using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAgent : MonoBehaviour
{
    public int sheepGroupId;

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    public float timeBetweenShots = 5f;

    public GameObject sheepMeshObject;
    float timer;

    private void Start()
    {
        timeBetweenShots = Random.Range(5, 30);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenShots)
        {
            audioSource.PlayOneShot(RandomClip(), 0.5f);
            timer = 0;
        }
    }

    AudioClip RandomClip()
    {
        timeBetweenShots = Random.Range(20, 30);
        return audioClipArray[Random.Range(0, audioClipArray.Length)];
    }
}
