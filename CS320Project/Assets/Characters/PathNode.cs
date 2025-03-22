using UnityEngine;
    
        //CAUTION: THIS WILL CURRENTLY SPREAD WITHOUT ENDING.
        //Once complete, it will stop upon finding the player or if the player moves out of range.
        //For now, do not use this.

public class PathNode : MonoBehaviour
{
    //Fields
    public Vector3 nodeLocation;
    //public bool connectNorth = false;
    //public bool connectSouth = false;
    //public bool connectEast = false;
    //public bool connectWest = false;
    public int generation = 0;
    public PathNode north;      //The PathNode immediately to the North of this one
    public PathNode south;
    public PathNode east;
    public PathNode west;
    public NodeList nodeList;   //The NodeList that responsible for this set of PathNodes
    public bool inPath;

    //Constructors
    public PathNode()
    {
        //Should return a PathNode of generation 0 with all adjacent nodes being null.
    }

    public PathNode(PathNode north, PathNode south, PathNode east, PathNode west, NodeList nodeList, int generation)
    {
        this.north = north;
        this.south = south;
        this.east = east;
        this.west = west;
        this.nodeList = nodeList;
        this.generation = generation + 1; 
    }


    void Start()
    {
        nodeLocation = this.transform.position;
        //Add deletion if too close to solid, non-player object

        //After deletion code, spawn PathNodes in 4 directions.
        //Spread();
    }

    void Update()
    {

    }

    private void Spread()
    {
        if(north == null)
        {
            north = SpreadNorth();
        }
        if(south == null)
        {
            south = SpreadSouth();
        }
        if(east == null)
        {
            east = SpreadEast();
        }
        if(west == null)
        {
            west = SpreadWest();
        }
    }

    //ADD COORDINATES FOR ALL NEW NODES
    private PathNode SpreadNorth()
    {
        PathNode newNorth = new PathNode(null,this,null,null,this.nodeList,generation);
        return newNorth;
    }
    
    private PathNode SpreadSouth()
    {
        PathNode newSouth = new PathNode(this,null,null,null,this.nodeList,generation);
        return newSouth;
    }

    private PathNode SpreadEast()
    {
        PathNode newEast = new PathNode(null,null,null,this,this.nodeList,generation);
        return newEast;
    }

    private PathNode SpreadWest()
    {
        PathNode newWest = new PathNode(null,null,this,null,this.nodeList,generation);
        return newWest;
    }
}