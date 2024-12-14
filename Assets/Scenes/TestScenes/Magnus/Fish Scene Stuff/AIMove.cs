using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    private AISpawner m_AIManager;


    private bool m_hasTarget = false;
    private bool m_isTurning;

    private Vector3 m_waypoint;
    private Vector3 m_lastWaypoint = new Vector3(0f, 0f, 0f);

    private Animator m_animator;
    private float m_speed;


    private float m_scale;

    private Collider m_collider;

    Quaternion m_rotation;
   


    // Start is called before the first frame update
    void Start()
    {
        m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
        m_animator = GetComponent<Animator>();

        SetupNPC();
        FindTarget();
    }
    // Update is called once per frame

    void SetupNPC()
    {
        float m_scale = Random.Range(0f, 4f);
        transform.localScale += new Vector3(m_scale * 1.5f, m_scale, m_scale);

        if(transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().enabled == true)
        {
            m_collider = transform.GetComponent<Collider>();

        }
        else if (transform.GetComponentInChildren<Collider>() != null && transform.GetComponentInChildren<Collider>().enabled == true)
        {
            m_collider = transform.GetComponentInChildren<Collider>();
        }

    }

    void Update()
    {
        if (transform.position == m_waypoint)
        {
            FindTarget();
        }
        RotateNPC(m_waypoint, m_speed);
        transform.position = Vector3.MoveTowards(transform.position, m_waypoint, m_speed * Time.deltaTime);

        ColliderNPC();
    }

    private void FindTarget(float start = 1f, float end = 7f)
    {
        m_lastWaypoint = m_waypoint;
        bool isWaypointSame = false;
        while (!isWaypointSame)
        {
            m_waypoint = m_AIManager.RandomWaypoint();
            if (m_waypoint != m_lastWaypoint)
            {
                isWaypointSame = true;
            }
        }
        m_speed = Random.Range(start, end);
        m_animator.speed = m_speed;
    }

    void ColliderNPC()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
        {     
            if (hit.collider == m_collider | hit.collider.tag == "waypoint")
            {
                return;
            }
            int randomNum = Random.Range(1, 100);
            if (randomNum < 40)
                m_hasTarget = false;
            Debug.Log(hit.collider.transform.parent.name + " " + hit.collider.transform.parent.position);
        }

    }



    void RotateNPC (Vector3 waypoint, float currentSpeed)
    {
        float TurnSpeed = currentSpeed * Random.Range(1f, 3f);
        Vector3 LookAt = waypoint - transform.position;
        m_rotation = Quaternion.Slerp(m_rotation, Quaternion.LookRotation(LookAt), TurnSpeed * Time.deltaTime);
        transform.rotation = m_rotation * Quaternion.Euler(0, -90, 0);
    }
}
