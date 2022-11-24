using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{

    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulate_Distance;

    [HideInInspector]
    public float step_Distance;


    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();
    }


    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        // if we are NOT on the ground
        if(!character_Controller.isGrounded)
        return;

        // accumulate distance is the value how far can we go
        // e.g. make a step or sprint, or move while crouching
        //until we play the footstep sound
        if(character_Controller.velocity.sqrMagnitude > 0){
            accumulate_Distance += Time.deltaTime;

            if(accumulate_Distance > step_Distance) {
                
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulate_Distance = 0f;
            }
        } else {
            accumulate_Distance = 0f;
        }
    }
}
