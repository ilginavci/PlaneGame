using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public Rigidbody playerRb;
    public Animator playerAnimator;
    public float speed;
    Vector3 turnIndexs = new Vector3(0, 0, 0);
    public KeyCode up, down, tiltRight, tiltLeft, turnRight, turnLeft, attack;
    private void TurnRight()
    {
        playerAnimator.SetBool("RudderLeft", false);
        playerAnimator.SetBool("RudderIdle", false);
        playerAnimator.SetBool("RudderRight", true);
        turnIndexs.y= 0.2f;
    }
    private void TurnLeft()
    {
        playerAnimator.SetBool("RudderRight", false);
        playerAnimator.SetBool("RudderIdle", false);
        playerAnimator.SetBool("RudderLeft", true);
        turnIndexs.y = -0.2f;
    }
    private void StopTurn()
    {
        playerAnimator.SetBool("RudderIdle", true);
        playerAnimator.SetBool("RudderRight", false);
        playerAnimator.SetBool("RudderLeft", false);
        turnIndexs.y = 0;
    }
    private void TiltRight()
    {
        playerAnimator.SetBool("WingsLeft", false);
        playerAnimator.SetBool("WingsRight", true);
        playerAnimator.SetBool("WingsIdle", false);
        turnIndexs.z = -0.2f;
    }
    private void TiltLeft()
    {
        playerAnimator.SetBool("WingsRight", false);
        playerAnimator.SetBool("WingsLeft", true);
        playerAnimator.SetBool("WingsIdle", false);
        turnIndexs.z = 0.2f;
    }
    private void StopTilt()
    {
        playerAnimator.SetBool("WingsLeft", false);
        playerAnimator.SetBool("WingsRight", false);
        playerAnimator.SetBool("WingsIdle", true);
        turnIndexs.z = 0;
    }
    private void TiltUp()
    {
        playerAnimator.SetBool("ElevatorDown", false);
        playerAnimator.SetBool("ElevatorUp", true);
        playerAnimator.SetBool("ElevatorIdle", false);
        turnIndexs.x = -0.25f;
    }
    private void TiltDown()
    {
        playerAnimator.SetBool("ElevatorUp", false);
        playerAnimator.SetBool("ElevatorDown", true);
        playerAnimator.SetBool("ElevatorIdle", false);
        turnIndexs.x = 0.25f;
    }
    private void StopElevator()
    {
        playerAnimator.SetBool("ElevatorUp", false);
        playerAnimator.SetBool("ElevatorDown", false);
        playerAnimator.SetBool("ElevatorIdle", true);
        turnIndexs.x = 0;
    }
    // Update is called once per frame
    void Update()
    {
        RotatePlane();
        Move();
        if (Input.GetKeyDown(up))
        {
            TiltDown();
          
        }
        if (Input.GetKeyDown(down))
        {
            TiltUp();
        }
        if (Input.GetKeyDown(tiltLeft))
        {
            TiltLeft();
        }
        if(Input.GetKeyDown(tiltRight))
        {
            TiltRight();
        }
        if (Input.GetKeyDown(turnLeft))
        {
            TurnLeft();
        }
        if (Input.GetKeyDown(turnRight))
        {
            TurnRight();
        }
        if (!(Input.GetKey(up) || Input.GetKey(down)))
        {
            StopElevator();
            if (Input.GetKeyDown(KeyCode.W))
            {
                TiltDown();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                TiltUp();
            }
        }
        if (!(Input.GetKey(tiltLeft) || Input.GetKey(tiltRight)))
        {
            StopTilt();
        }
        if (!(Input.GetKey(turnLeft) || Input.GetKey(turnRight)))
        {
            StopTurn();
        }
    }
    void RotatePlane()
    {
        transform.Rotate(turnIndexs);
    }
    void Move()
    {
        Vector3 tempVelocity = new Vector3(0, -0.2f, 0);
        tempVelocity += transform.forward * speed;
        float slopeCheck = transform.rotation.eulerAngles.x % 180;
        slopeCheck = 90 - Mathf.Abs(90 - slopeCheck);
        if (slopeCheck > 85)
        {
            tempVelocity.y += (-slopeCheck / 5f);
            print(-slopeCheck/10f);
        }
        playerRb.velocity = tempVelocity;
    }
}
