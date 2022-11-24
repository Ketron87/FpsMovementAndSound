using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    private Transform camera_Root;
    private float stand_Height = 1f;
    private float crouch_Height = 0.5f;

    private bool is_Crouching;
    
    private PlayerFootStep player_Footsteps;
    
    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;
    
    private float walk_Step_Distance = 0.5f;
    private float sprint_Step_Distance = 0.4f;
    private float crouch_Step_Distance = 0.6f;



    
    
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        camera_Root = transform.GetChild(0);

        player_Footsteps = GetComponentInChildren<PlayerFootStep>();
    }

    void Start()
    {
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
        player_Footsteps.step_Distance = walk_Step_Distance;

    }

    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint ()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.speed = sprint_Speed;

            player_Footsteps.step_Distance = sprint_Step_Distance;
            player_Footsteps.volume_Min = sprint_Volume;
            player_Footsteps.volume_Max = sprint_Volume;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.speed = move_Speed;

            player_Footsteps.step_Distance = walk_Step_Distance;
            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;
            
        }
    } // Sprint

    void Crouch ()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {   
            // if we are crouching - stand up
            if(is_Crouching) {

                camera_Root.localPosition = new Vector3(0f, stand_Height , 0f);
                playerMovement.speed = move_Speed;

                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;

                is_Crouching = false;

            } else {
                // if we are not crouching -crouch
                camera_Root.localPosition = new Vector3(0f, crouch_Height , 0f);
                playerMovement.speed = crouch_Speed;

                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Min = crouch_Volume;
                player_Footsteps.volume_Max = crouch_Volume;

                is_Crouching = true;



            }
            
        } // if we press C
    } // Crouch
}
