using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    bool jumpOn = false; // simple test
    float jumpPower = 1f;
    float tempJump;
    Transform playerTf;
    Vector3 tempVec;
    void Awake()
    {
        playerTf = transform;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!jumpOn)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
            {// click action
                StartCoroutine("JumpAction");
            }
        }
    }

    IEnumerator JumpAction()
    {// set jump action coroutine

        jumpOn = true; // jumping power varies according to pressing time or drag. but fixed this time
        tempVec = playerTf.position;

        tempJump = jumpPower;
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
        jumpOn = false;
    }
}
