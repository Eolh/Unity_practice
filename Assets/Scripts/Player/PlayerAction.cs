using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    bool jumpOn = false; // simple test
    float jumpPower = 3.5f;
    float tempJump;
    Transform playerTf;
    Vector3 tempVec;

    Animator playerAni;

    void Awake()
    {
        playerTf = transform;
        playerAni = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumpOn)
        {
            if ((Input.GetMouseButtonDown(0))) // press button
            {
                StartCoroutine("CheckButtonDownSec");
            }
            else if ((Input.GetMouseButtonUp(0))) // release button
            {// click action
                StartCoroutine("JumpAction");
            }
        }
    }

    float checkTime;
    IEnumerator CheckButtonDownSec()
    {// press on button time check coroutine
        checkTime = 0;
        while (!jumpOn)
        {
            yield return new WaitForSeconds(0.04f); // time check per 0.04sec 
            checkTime += 0.04f;
        }
    }

    IEnumerator JumpAction()
    {// set jump action coroutine

        jumpOn = true; // jumping power varies according to pressing time or drag. but fixed this time
        tempVec = playerTf.position;

        if (checkTime > 0.35f) // charge time maximum
        {
            checkTime = 0.35f;
        }

        Debug.Log("chkTime : " + checkTime);
        if (checkTime > 0.15f)
        { // 0.16 ~
            if (checkTime < 0.25f)
            {
                playerAni.SetTrigger("jump1"); // on trigger -> start animation 
            }
            else
            {
                playerAni.SetTrigger("jump2"); // high jump
            }

            yield return new WaitForSeconds(0.15f); // jump next to end animation 
        }
        tempJump = jumpPower * checkTime; // power up than preexistence button, apply checkTime 
        tempVec.y += tempJump;
        playerTf.position = tempVec;

        while (tempVec.y > 0)
        {
            yield return new WaitForSeconds(0.03f);
            tempJump -= 0.05f;
            tempVec.y += tempJump;
            playerTf.position = tempVec;
        }
        tempVec.y = 0;
        playerTf.position = tempVec;

        if (checkTime < 0.25f)
        {
            playerAni.SetTrigger("drop1"); // on trigger -> start animation
        }
        else
        {
            playerAni.SetTrigger("drop2"); // high jump
        }

        jumpOn = false;
    }
}
