using System;
using Codice.CM.Common.Checkin.Partial.SameData;
using UnityEngine;


public class NodeList : MonoBehaviour
{
    //Fields
    //public Vector3 playerLocation;
    public Vector3 npcLocation; //Use this as location of startingNode.
    public PathNode node;
    public bool targetFound = false;
    public bool isUpdated = false;
    public float range = 10f;   //This range must match the NPCClass range. (NPCClass range can be deleted?)
    public PathNode[,] nodeArray;   //[x,y] are the coordinates, with NodeList being at [10,10]
    private float gridUnit;         //Consult Node Array Excel sheet for valid coordinates.
    private float locX;
    private float locY;
    private float locZ;
    private int i;
    private int j;
    private int min;
    private int max;


    void Start()
    {
        gridUnit = range/10;    //For spacing purposes
        locZ = transform.position.z;

        for(i=0; i<21; i++)
        {
            if(i <= 10)
            {
                min = 10 - i;
                max = i + 11;
            }
            else
            {
                min = i - 10;
                max = 31 - i;
            }

            for (j=min;j<max;j++)
            {
                nodeArray[i,j] = Instantiate(node); //Create node w/ [x,y] index to match its coords
                
                if(i <= 10)
                {
                    locX = transform.position.x - (gridUnit *(10 - i));
                }
                else
                {
                    locX = transform.position.x + (gridUnit *(i - 10));
                }
                if(j <= 10)
                {
                    locY = transform.position.y - (gridUnit *(10 - j));
                }
                else
                {
                    locY = transform.position.y + (gridUnit *(j - 10));
                }

                nodeArray[i,j].transform.position = new Vector3(locX,locY,locZ);  //put it at those coords relative to NodeList
                nodeArray[i,j].xCoord = i;
                nodeArray[i,j].yCoord = j;
            }
        }
    }

    void Update()
    {

    }

    public void UpdateNodes()
    {
        locZ = transform.position.z;

        for(i=0; i<21; i++)
        {
            if(i <= 10)
            {
                min = 10 - i;
                max = i + 11;
            }
            else
            {
                min = i - 10;
                max = 31 - i;
            }

            for (j=min;j<max;j++)
            {                
                if(i <= 10)
                {
                    locX = transform.position.x - (gridUnit *(10 - i));
                }
                else
                {
                    locX = transform.position.x + (gridUnit *(i - 10));
                }
                if(j <= 10)
                {
                    locY = transform.position.y - (gridUnit *(10 - j));
                }
                else
                {
                    locY = transform.position.y + (gridUnit *(j - 10));
                }

                nodeArray[i,j].transform.position = new Vector3(locX,locY,locZ);  //put it at those coords relative to NodeList
            }
        }

        isUpdated = true;
    }
}