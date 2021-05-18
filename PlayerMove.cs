using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Basic Rigidbody based FPS movement created by Ariel Michaeli for the Unity game engine (-_-)


public class PlayerMove : MonoBehaviour
{
    //var registration

    //Rigidbody Manager
    Rigidbody rb;

    //Movement speed
    float BaseVelocity = 0.0235f;

    float JumpDirectionForce = 250;

    //bool for is the player standing on the ground
    bool AmGrouded;

    //Jump direction bools
    bool JumpForward;
    bool JumpBackwards;
    bool JumpRight;
    bool jumpLeft;

    // Start is called before the first frame update
    void Start()
    {
        //getting the Rigidbody component into the Rigidbody manager:
        rb = GetComponent<Rigidbody>();

        //Jump vars Config:
        AmGrouded = true;
        JumpForward = false;
        JumpBackwards = false;
        JumpRight = false;
        jumpLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get key function is holding the button and GetKeyDown is input for pressing it.

        //forward
        if (Input.GetKey(KeyCode.W) && AmGrouded == true)
        {
            transform.Translate(Vector3.forward * BaseVelocity);
            Debug.Log("W");
        }

        //backwards
        if (Input.GetKey(KeyCode.S) && AmGrouded == true)
        {
            transform.Translate(Vector3.back * BaseVelocity);
            Debug.Log("S");
        }

        //RightSide
        if (Input.GetKey(KeyCode.D) && AmGrouded == true)
        {
            transform.Translate(Vector3.right * BaseVelocity);
            Debug.Log("D");
        }

        //LeftSide
        if (Input.GetKey(KeyCode.A) && AmGrouded == true)
        {
            transform.Translate(Vector3.left * BaseVelocity);
            Debug.Log("A");
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && AmGrouded == true)
        {
            //adding force in the Y axis to jump
            rb.AddForce(0, 300, 0);
            //changing AmGrounded bool to false
            AmGrouded = false;
            //if player is crouching, reverting the scale
            transform.localScale = new Vector3(1, 1.25f, 1);
            

        //jumpDirections:

            //forward
            if (Input.GetKey(KeyCode.W) && JumpForward == false)
            {
                rb.AddRelativeForce(Vector3.forward * JumpDirectionForce);
                JumpForward = true;

                //now for realism, after the player jumps to one direction. we disable the option to go to another direction in the same jump
                JumpBackwards = true;
                jumpLeft = true;
                JumpRight = true;
            }

            //backwards
            if (Input.GetKey(KeyCode.S) && JumpBackwards == false)
            {
                rb.AddRelativeForce(Vector3.back * JumpDirectionForce);
                JumpBackwards = true;

                //now for realism, after the player jumps to one direction. we disable the option to go to another direction in the same jump
                JumpForward = true;
                jumpLeft = true;
                JumpRight = true;
            }

            //Right
            if (Input.GetKey(KeyCode.D) && JumpRight == false)
            {
                rb.AddRelativeForce(Vector3.right * JumpDirectionForce);
                JumpRight = true;

                //now for realism, after the player jumps to one direction. we disable the option to go to another direction in the same jump
                JumpForward = true;
                JumpBackwards = true;
                jumpLeft = true;
            }

            //Left
            if (Input.GetKey(KeyCode.A) && jumpLeft == false)
            {
                rb.AddRelativeForce(Vector3.left * JumpDirectionForce);
                jumpLeft = true;

                //now for realism, after the player jumps to one direction. we disable the option to go to another direction in the same jump
                JumpForward = true;
                JumpBackwards = true;
                JumpRight = true;
            }
        }

        //crouching
        if (Input.GetKeyDown(KeyCode.LeftControl) && AmGrouded == true)
        {
            //changing the scale of the player
            transform.localScale = new Vector3(1, 0.75f, 1);
            //changing the speed of the player
            BaseVelocity = 0.0170f;
        }

        //crouch revert
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            //reverting the scale of the player
            transform.localScale = new Vector3(1, 1.25f, 1);
            //reverting the speed
            BaseVelocity = 0.0235f;
        }

        //sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //speed change
            BaseVelocity = 0.04f;
            //revert scale if in crouch
            transform.localScale = new Vector3(1, 1.25f, 1);
        }
        //sprinting revert
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //reverting speed
            BaseVelocity = 0.0235f;
        }
    }


    //getting if the Player touches something (in most cases the ground)
    private void OnCollisionEnter(Collision collision)
    {
        //if the bool "AmGrounded" is true it will allow the ability to jump or in other words: "if the player is not in the air, he can jump"
        AmGrouded = true;

        //reset all of the jump direction bools:
        JumpForward = false;
        JumpBackwards = false;
        JumpRight = false;
        jumpLeft = false;
    }
}
