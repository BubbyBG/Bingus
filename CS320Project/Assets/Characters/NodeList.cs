using UnityEngine;


public class NodeList : MonoBehaviour
{
    //Fields
    //public Vector3 playerLocation;
    public Vector3 npcLocation; //Use this as location of startingNode.
    public PathNode node;       //Allows Instantiate to be called on node.
    public bool targetFound = false;
    public bool isUpdated = false;
    public float range = 10f;   //This range must match the NPCClass range. (NPCClass range can be deleted?)
    public PathNode[,] nodeArray;   //[x,y] are the coordinates, with NodeList being centered at [rangeInt,rangeInt]
    private float gridUnit;         //Consult Node Array Excel sheet for valid coordinates.
    private float locX;
    private float locY;
    private float locZ;
    private int i;      //i, j, min, and max are iteratables
    private int j;
    private int min;
    private int max;
    private int rangeInt = 10;


    void Start()
    {
        gridUnit = range/rangeInt;    //For spacing purposes
        locZ = transform.position.z;

        for(i=0; i<=(2*rangeInt); i++)
        {
            if(i <= rangeInt)
            {
                min = rangeInt - i;
                max = rangeInt + i;
            }
            else
            {
                min = i - rangeInt;
                max = (3*rangeInt) - i;
            }

            for (j=min;j<=max;j++)
            {
                nodeArray[i,j] = Instantiate(node); //Create node w/ [x,y] index to match its coords
                
                if(i <= rangeInt)
                {
                    locX = transform.position.x - (gridUnit *(rangeInt - i));
                }
                else
                {
                    locX = transform.position.x + (gridUnit *(i - rangeInt));
                }
                if(j <= rangeInt)
                {
                    locY = transform.position.y - (gridUnit *(rangeInt - j));
                }
                else
                {
                    locY = transform.position.y + (gridUnit *(j - rangeInt));
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

        for(i=0; i<=(2*rangeInt); i++)
        {
            if(i <= rangeInt)
            {
                min = rangeInt - i;
                max = rangeInt + i;
            }
            else
            {
                min = i - rangeInt;
                max = (3*rangeInt) - i;
            }

            for (j=min;j<=max;j++)
            {                
                if(i <= rangeInt)
                {
                    locX = transform.position.x - (gridUnit *(rangeInt - i));
                }
                else
                {
                    locX = transform.position.x + (gridUnit *(i - rangeInt));
                }
                if(j <= rangeInt)
                {
                    locY = transform.position.y - (gridUnit *(rangeInt - j));
                }
                else
                {
                    locY = transform.position.y + (gridUnit *(j - rangeInt));
                }

                nodeArray[i,j].transform.position = new Vector3(locX,locY,locZ);  //put it at those coords relative to NodeList
            }
        }

        isUpdated = true;
    }

    public bool ClearNodes()
    {
        int numDestroyed = 0;
        for(i=0; i<=(2*rangeInt); i++)
        {
            if(i <= rangeInt)
            {
                min = rangeInt - i;
                max = rangeInt + i;
            }
            else
            {
                min = i - rangeInt;
                max = (3*rangeInt) - i;
            }

            for (j=min;j<=max;j++)
            {
                DestroyImmediate(nodeArray[i,j]);
                numDestroyed++;
            }
        }
        return(numDestroyed == 221);        //Debug... to verify all nodes destroyed.
    }                                       //Because of gridUnit and rangeInt standardization, the total # of nodes should always be 221.

    public void NPCDeath()
    {
        if(ClearNodes())                    //Further Debug stuff. See above.
        {
            DestroyImmediate(this);
        }
    }
}