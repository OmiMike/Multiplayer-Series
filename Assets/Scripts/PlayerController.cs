using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputManager inputManager;

    [Header("Movement")]
    [SerializeField] float speed = 1.2f;
    [SerializeField] bool holdToRun;
    [SerializeField] bool isCrouch;
    Animator anim;
    Vector2 moveInput;
    Vector3 rootmotion;
    CharacterController cc;

    [Header("Jump")]
    [SerializeField] bool isJump;
    [SerializeField] float stepDown;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity;
    [SerializeField] float airControl;
    [SerializeField] float jumpDampTime;
    [SerializeField] Vector3 velocity;

    [Header("Weapon")]
    [SerializeField] Shooting weapon;

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

        Crouch(inputManager.crouching);
        Sprinting(inputManager.running);

        Vector3 speedByCharacter = new Vector3(moveInput.x, 0, moveInput.y);
        speedByCharacter = transform.rotation * speedByCharacter;
        cc.Move(speedByCharacter * Time.fixedDeltaTime);

        cc.Move(rootmotion);
        rootmotion = Vector3.zero;

        if (isJump)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }

        FireWeapon(inputManager.shooting);
        Reload(inputManager.reloading);
    }

    void OnAnimatorMove()
    {
        rootmotion += anim.deltaPosition; //Handling rootmotion by script
    }

    public void Crouch(KeyCode button)
    {
        if (Input.GetKeyDown(button))
            isCrouch = !isCrouch;

        if (Input.GetKeyDown(button))
            anim.SetBool("Crouch", isCrouch);
    }

    void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootmotion * speed;
        Vector3 stepDownAmount = Vector3.down * stepDown;

        cc.Move(stepForwardAmount + stepDownAmount);
        rootmotion = Vector3.zero;

        Jump(inputManager.jump);
    }

    void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;

        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();

        cc.Move(displacement);
        isJump = !cc.isGrounded;
        rootmotion = Vector3.zero;
        //anim.SetTrigger("Jump");
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * moveInput.y) + (transform.right * moveInput.x)) * (airControl / 100);
    }

    public void Jump(KeyCode button)
    {
        if (Input.GetKeyDown(button))
        {
            if (!isJump)
            {
                isJump = true;
                velocity = anim.velocity * jumpDampTime * speed;
                velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
                anim.SetTrigger("Jump");
            }
        }
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

    public void Reload(KeyCode button)
    {
        if (Input.GetKeyDown(button))
        {
            if (weapon.w.curClipAmount > 0 && weapon.w.curAmmo > 0 && weapon.w.curAmmo >= weapon.w.maxClipAmount)
            {
                weapon.w.curClipAmount = weapon.w.maxClipAmount;
                weapon.w.curAmmo -= weapon.w.maxClipAmount;
            }
            else if (weapon.w.curClipAmount > 0 && weapon.w.curAmmo > 0 && weapon.w.curAmmo < weapon.w.maxClipAmount && weapon.w.curClipAmount != weapon.w.maxClipAmount)
            {
                weapon.w.curClipAmount = weapon.w.curAmmo;
                weapon.w.curAmmo = 0;
            }
            else if (weapon.w.curClipAmount <= 0 && weapon.w.curAmmo > 0 && weapon.w.curAmmo >= weapon.w.maxClipAmount)
            {
                weapon.w.curClipAmount = weapon.w.maxClipAmount;
                weapon.w.curAmmo -= weapon.w.maxClipAmount;
            }
            else if (weapon.w.curClipAmount <= 0 && weapon.w.curAmmo > 0 && weapon.w.curAmmo < weapon.w.maxClipAmount)
            {
                weapon.w.curClipAmount = weapon.w.curAmmo;
                weapon.w.curAmmo = 0;
            }
            else if (weapon.w.curClipAmount <= 0 && weapon.w.curAmmo <= 0)
            {
                weapon.w.curAmmo = 0;
                weapon.w.curClipAmount = 0;
                return;
            }
        }
    }

    public void FireWeapon(KeyCode button)
    {
        if (Input.GetKeyDown(button))
        {
            if(weapon.w.curClipAmount > 0)
            {
                weapon.StartShooting();
            }
            else if (weapon.w.curClipAmount <= 0)
            {
                weapon.w.curClipAmount = 0;
                if(weapon.w.curAmmo <= 0)
                {
                    weapon.w.curAmmo = 0;
                }
            }
        } 

        if(weapon.isFiring)
            weapon.UpdateFiring(Time.deltaTime);

        if (Input.GetKeyUp(button))
            weapon.StopShooting();
    }
}
