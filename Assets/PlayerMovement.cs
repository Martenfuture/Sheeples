using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    public float movementSpeed = 10f;
    
    
    public static PlayerMovement instance = null;

   




    private bool disabled = false;


    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        instance = this;
    }


    void Update()
    {

        if (!disabled) ControllPlayer();


    }


    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");



        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        if (movement != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 20f;
        }
        else
        {
            movementSpeed = 10f;
        }




    }
    

    

   

    public void Disable()
    {
        disabled = true;
        animator.SetBool("isMoving", false);
    }

    public void EnableMovement()
    {
        disabled = false;
    }

}
