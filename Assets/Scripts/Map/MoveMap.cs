using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{

    public GameObject tile; // floor GameObject(Prefabs)

    void Awake()
    {
        CreateTiles();
    }

    private GameObject[] tiles;
    private int tileNum = 3;

    void CreateTiles()
    {// create repeat using tiles
        tiles = new GameObject[tileNum]; // using 3 times 
        for (int i = 0; i < tileNum; i++) // loop 3 tiems
        {
            // Instantiate(Create Object, created Position, angle)
            tiles[i] = Instantiate(tile, Vector3.zero, Quaternion.identity) as GameObject; // create
            tiles[i].SetActive(false); // nonActive
        }
    }

}
