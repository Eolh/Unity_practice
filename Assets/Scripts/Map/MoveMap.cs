using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{

    public GameObject tile; // floor GameObject(Prefabs)
    //private GameObject[] tiles;
    private int tileNum = 3;

    struct TileStruct // create struct to using tiles
    {
        public GameObject obj;
        public Transform tf;
        public bool active;
        public Vector3 pos;
    }
    private TileStruct[] tiles;

    private Vector3 tileCenterVec; // center point
    private float tileGap = 81.9f; // block length
    private float tileEndPoint = -60f;

    private Vector3 tempVec; // temp vector

    private float speed = 0.5f; // background or Object moving speed
    private int lastTileNum = 0;

    // add obstacle
    public GameObject obstacle;
    private int obsNum = 10;
    struct ObstacleStruct
    {
        public GameObject obj;
        public bool active;
        public int parentTileNum;
        public ObstacleInfo info; // obs infomation script
    }
    private ObstacleStruct[] obss;
    //end add obstacle code

    void Awake()
    {
        tileCenterVec = new Vector3(0, -5.1f, 0);
        CreateTiles();
        CreateObss(); // creating Obstacle
    }

    void FixedUpdate()
    {// move block specific speed 

        for (int i = 0; i < tileNum; i++)
        {
            tiles[i].pos.x -= speed;
            if (tiles[i].pos.x > tileEndPoint)
            {// Point invisible to the screen (nomarlitic)
                tiles[i].tf.position = tiles[i].pos;
            }
            else
            {// over endPoint -> move last block  
                DeleteObs(i); // remove get over obstacle 

                tiles[i].pos = tiles[lastTileNum].pos;
                tiles[i].pos.x += tileGap;
                if (lastTileNum > i) // if lastTileNum larger than i, add to 0.5f decrease position
                {
                    tiles[i].pos.x -= 0.5f;
                }

                tiles[i].tf.position = tiles[i].pos; // change the physical location
                AddedObs(i, 1); // hold on to one obstacle first -> i position change

                lastTileNum = i; // change last block number 
            }
        }
    }

    // tileN = tile number , obsN = obstacle count
    void AddedObs(int tileN, int obsN) // call this function when create obs in background
    {// adding obstacle to applicable tile
        tempVec.x = tiles[tileN].pos.x; // block center test 
        tempVec.y = 0;
        tempVec.z = 0;

        for (int i = 0; i < obsNum; i++)
        {
            if (!obss[i].active)
            {// meet nonActive state obstacle
                obss[i].obj.SetActive(true); // set active
                obss[i].active = true;
                obss[i].obj.transform.position = tempVec;
                obss[i].info.SetObstacle(Random.Range(0, 9)); // Temporarily add random value
                obss[i].obj.transform.SetParent(tiles[tileN].tf); // change parent
                obss[i].parentTileNum = tileN;
                break; // when create one obstacle, don't need loop & create 
            }
        }
    }

    void DeleteObs(int tileN) // this function call when tile remove
    {
        for (int i = 0; i < obsNum; i++)
        {
            if (obss[i].active)
            {
                if (obss[i].parentTileNum == tileN)
                {
                    obss[i].obj.transform.parent = null; // remove parent -> return move down the obss object 
                    obss[i].parentTileNum = -1; // not exist value
                    obss[i].obj.SetActive(false); // set nonActive
                    obss[i].active = false;
                }
            }
        }
    }

    void CreateTiles()
    {// create repeat using tiles(Object pool)
        tempVec = tileCenterVec; // center point for create position 

        tiles = new TileStruct[tileNum]; // loop 3times
        for (int i = 0; i < tileNum; i++)
        {// Setting basic information and position 

            // Instantiate(Create Object, created Position, angle)
            tiles[i].obj = Instantiate(tile, tempVec, Quaternion.identity) as GameObject; // create
            tiles[i].tf = tiles[i].obj.transform;
            tiles[i].pos = tiles[i].tf.position;
            tiles[i].active = true; // nonActive

            tempVec.x += tileGap; // create next block set increase tileGap position 
        }
        lastTileNum = 2; // initial position  0,1,2
    }

    // create obstacle 
    void CreateObss()
    {// create repeat using tiles(Object pool)

        obss = new ObstacleStruct[obsNum]; // repeat obsNum
        for (int i = 0; i < obsNum; i++)
        {// Setting basic information and position
            obss[i].obj = Instantiate(obstacle, Vector3.zero, Quaternion.identity) as GameObject; // create
            obss[i].active = false;
            obss[i].parentTileNum = -1;
            obss[i].info = obss[i].obj.GetComponent<ObstacleInfo>(); // get component(script)
            obss[i].obj.SetActive(false); // initial state is nonActive
        }
    }
    // end create obstacle 
}
