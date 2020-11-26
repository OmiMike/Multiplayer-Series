using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputManager inputManager;

    [SerializeField] float speed;
    [SerializeField] bool holdToRun;
    Animator anim;
    Vector2 moveInput;
    Vector3 rootmotion;
    CharacterController cc;

    void Start()
    {
        anim = GetComponent<Animator>(); //Get the Animator Component
        cc = GetComponent<CharacterController>(); //Get the Character Controller Component
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal") * speed; //We are getting the Horizontal movement
        moveInput.y = Input.GetAxis("Vertical") * speed; //We are getting the Vertical movement

        anim.SetFloat("InputX", moveInput.x); //We are setting the float from the horizontal movement
        anim.SetFloat("InputY", moveInput.y); //We are setting the float from the vertical movement

        Sprinting(inputManager.running);
    }

    void OnAnimatorMove()
    {
        rootmotion += anim.deltaPosition; //Handling rootmotion by script
    }

    void FixedUpdate()
    {
        cc.Move(rootmotion); //Move the characters position
        rootmotion = Vector3.zero; //Reset the rootmotion
    }

    public void Sprinting(KeyCode button)
    {
        if (Input.GetKeyDown(button))
        {
            holdToRun = true;
            speed = 3.5f;
            anim.SetBool("Sprint", true);
        }

        if (Input.GetKeyUp(button))
        {
            holdToRun = false;
            speed = 1.9f;
            anim.SetBool("Sprint", false);
        }
    }
}
