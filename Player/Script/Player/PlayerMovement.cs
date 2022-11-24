using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_Controller;

    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertial_Velocity;

    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        AppyGravity();

        character_Controller.Move(move_Direction);
    } // Move Player

    void AppyGravity()
    {   
        if(character_Controller.isGrounded)
        {
            vertial_Velocity -= gravity * Time.deltaTime;

            //Jump
            Jump();
        } else {

            vertial_Velocity -= gravity * Time.deltaTime;
        }
        move_Direction.y = vertial_Velocity * Time.deltaTime;
        
    } // Apply Gravity

    void Jump()
    {
        if(character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertial_Velocity = jump_Force;
        }
    } 
}
