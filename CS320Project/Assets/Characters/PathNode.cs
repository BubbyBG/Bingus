using UnityEngine;
    
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
    public bool inPath = false;
    public bool hasNotSpreadNorth = true;  //Can't check a Monobehaviour for being null, this is a workaround.
    public bool hasNotSpreadSouth = true;
    public bool hasNotSpreadEast = true;
    public bool hasNotSpreadWest = true;

    //Constructors
    public PathNode()
    {
        //Should return a PathNode of generation 0 with all adjacent nodes being null.
    }

    public PathNode(Vector3 nodeLocation, int generation)
    {
        this.transform.position = nodeLocation;
        this.generation += generation;
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

    public void Spread()
    {
        if(hasNotSpreadNorth)
        {
            north = SpreadNorth();
            hasNotSpreadNorth = false;
        }

        if(hasNotSpreadSouth)
        {
            south = SpreadSouth();
            hasNotSpreadSouth = false;
        }

        if(hasNotSpreadEast)
        {
            east = SpreadEast();
            hasNotSpreadEast = false;
        }

        if(hasNotSpreadWest)
        {
            west = SpreadWest();
            hasNotSpreadWest = false;
        }
    }

    public PathNode SpreadNorth()
    {
        PathNode newNorth = new PathNode(nodeLocation + Vector3.forward, 1);
        newNorth.hasNotSpreadSouth = false;
        newNorth.south = this;
        return newNorth;
    }
    
    public PathNode SpreadSouth()
    {
        PathNode newSouth = new PathNode(nodeLocation + Vector3.back, 1);
        newSouth.hasNotSpreadNorth = false;
        newSouth.north = this;
        return newSouth;
    }

    public PathNode SpreadEast()
    {
        PathNode newEast = new PathNode(nodeLocation + Vector3.right, 1);
        newEast.hasNotSpreadWest = false;
        newEast.west = this;
        return newEast;
    }

    public PathNode SpreadWest()
    {
        PathNode newWest = new PathNode(nodeLocation + Vector3.left, 1);
        newWest.hasNotSpreadEast = false;
        newWest.east = this;
        return newWest;
    }
}