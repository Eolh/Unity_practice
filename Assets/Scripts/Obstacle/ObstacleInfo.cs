using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    public int highLevel = 0; // high level
    float height; // height


    Transform obsTf;

    void Awake()
    {
        obsTf = transform;
    }

    public void SetObstacle(int lv)
    {// call function, Set information (setting When active)
        height = 0.5f + 0.5f * lv; // basic 0.5f + increase 0.5f per level
        Vector3 tempVec = new Vector3(1f, height, 1f);
        obsTf.localScale = tempVec;
        Debug.Log(lv + "," + tempVec.y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("break : " + col.name);
    }
}
