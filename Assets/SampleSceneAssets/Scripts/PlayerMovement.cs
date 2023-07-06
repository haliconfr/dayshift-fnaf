using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float sprintSpeed = 16f;
    float bobOriginalSpeed, bobOriginalAmount;
    public float currentSpeed;
    public float gravity = -10f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public AudioSource walkSound;
    public ViewBobbing bobbing;
    public bool moving;
    Vector3 velocity;

    void Start()
    {
        bobOriginalSpeed = bobbing.bobbingSpeed;
        bobOriginalAmount = bobbing.bobbingAmount;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = speed;
    }

    void Update()
    {
        float x;
        float z;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S)){
            currentSpeed = sprintSpeed;
            bobbing.bobbingSpeed = bobbing.bobbingSpeed*1.5f;
            bobbing.bobbingAmount = bobbing.bobbingAmount*1.5f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            currentSpeed = speed;
            bobbing.bobbingSpeed = bobOriginalSpeed;
            bobbing.bobbingAmount = bobOriginalAmount;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            moving = true;
            if(!walkSound.isPlaying){
               walkSound.Play();
            }
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)){
            walkSound.Pause();
            moving = false;
        }
    }
}