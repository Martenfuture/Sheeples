using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepAgent : MonoBehaviour
{
    public int sheepGroupId;

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    public float timeBetweenShots = 5f;

    public GameObject sheepMeshObject;
    float timer;

    bool setAnimation = true;

    public bool isWaiting;

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

        if (setAnimation)
        {
            StartCoroutine(SetAnimationDelay());
        }

    }

    IEnumerator SetAnimationDelay()
    {
        setAnimation = false;
        yield return new WaitForSeconds(0.25f);
        float animationSpeed = gameObject.GetComponent<NavMeshAgent>().velocity.magnitude;
        if (animationSpeed < 0.5f)
        {
            animationSpeed = 1;
        }
        else
        {
            animationSpeed *= 0.35f;
        }

        gameObject.GetComponent<Animator>().SetFloat("movementSpeed", gameObject.GetComponent<NavMeshAgent>().velocity.magnitude);
        gameObject.GetComponent<Animator>().SetFloat("blendTreeSpeed", animationSpeed);
        setAnimation = true;
    }

    AudioClip RandomClip()
    {
        timeBetweenShots = Random.Range(20, 30);
        return audioClipArray[Random.Range(0, audioClipArray.Length)];
    }
}
