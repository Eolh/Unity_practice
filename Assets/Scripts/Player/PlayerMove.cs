using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed = 0.5f; // speed
    private Transform playerTf; // Reference
    private Vector3 playerPos; // player position

    void Awake()
    {
        playerTf = transform; // player transform from this script     
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine("MoveTheBall"); // start MoveTheBall coroutine
    }

    IEnumerator MoveTheBall()
    {
        playerPos = playerTf.position; // initial position check&save
        while (true) // infinite loop
        {
            yield return new WaitForSeconds(0.02f); // wait 0.02sec
            playerPos.x += speed; // increase pos.x value player position 
            playerTf.position = playerPos; // apply position changed value
        }
    }

}
