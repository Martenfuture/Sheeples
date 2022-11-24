using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    public float Speed;
    public Vector2 SpeedRandom;
    float newSpeed;
    float oldSpeed = 2;
    float fadeTime;
    Collider agentCollider;
    bool waiting;
    public Collider AgentCollider { get { return agentCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
        newSpeed = Random.Range(SpeedRandom.x, SpeedRandom.y);
    }

    private void Update()
    {
        fadeTime += Time.deltaTime * 0.5f;
        Speed = Mathf.Lerp(oldSpeed, newSpeed, fadeTime);
        if (newSpeed <= Speed && !waiting) StartCoroutine(RandomNewTime());
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
        //Debug.DrawRay(transform.position, velocity);
    }

    IEnumerator RandomNewTime()
    {
        waiting = true;
        yield return new WaitForSeconds(5);
        newSpeed = Random.Range(SpeedRandom.x, SpeedRandom.y);
        waiting = false;
    }
}
