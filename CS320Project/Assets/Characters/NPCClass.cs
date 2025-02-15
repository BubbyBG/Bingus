using UnityEngine;

/*  This will contain the basic traits (fields/methods) of
    NPCs. Enemy-specific traits will be inherited from here.
*/
public class NPCClass : MonoBehaviour
{
    
    //Fields...
    //ALL OF THESE NEED TO BE INSTANTIATED. SerializeFields?
    public Vector3 NPCLocation;
    private Vector3 NPCVelocity;
    public float NPCRadius = 1;     //Update size as needed
    private float playerDistance = 5000;  //This will be determined using the player's coordinates and the NPC's coordinates.
    private Vector3 travelDestination;
    private Vector3 playerLocation;
    public bool isAlive;
    private bool pathingState = false;       //Keeping for now, but may not actually need the bools...
    private bool travellingState = false;
    private bool aggressiveState = false;
    private bool passiveState = true;
    private int moveSpeed = 2;  //Change to be an appropriate movespeed for character.
    private float aggressiveRange = 3;  //Update range


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start code here - include things like location, facing, starting velocity...
        isAlive = true;
        NPCVelocity = new Vector3(0,0,0);
        travelDestination = new Vector3(0,0,0); //Might be okay to leave?
        playerLocation = new Vector3(5000,5000,5000);    //Needs to be updated by getting actual player location.

    }

    // Update is called once per frame
    void Update()
    {
        //Update code here - behavior logic and time-based things will go here.
        //Assign behavior/state based on logic, call methods as necessary.
        if(isAlive)
        {
            //Find a way to get player location first and update the local Vector3 to reflect.
            playerDistance = Vector3.Distance(NPCLocation, playerLocation);
            
            //Transition to aggression
            if(playerDistance < aggressionRange)
            {
                if(!aggressiveState)
                {
                    ClearState();
                    aggressiveState = true;
                }

                Aggression();
            }

            //Transition to pathing/travelling (Going to Destination)
            else if(Vector3.Distance(travelDestination, NPCLocation) > NPCRadius;)
            {
                if(!(pathingState || travellingState))
                {
                    ClearState();
                    pathingState = true;
                    Pathfinding();
                }
                else if(pathingState)
                {
                    Pathfinding();
                }
                else
                {
                    Travel();
                }
            }

            else
            {
                ClearState();
                passiveState = true;
            }
        }
        else
        {
            //Cleanup/NPC Death
        }

    }

    //This method clears the state that the NPC is in.
    private void ClearState()
    {
        pathingState = false;
        travellingState = false;
        aggressiveState = false;
        passiveState = false;
    }

    //This method will control the aggressive response toward the player.
    private void Aggression()
    {
        while(aggressiveState)
        {
            //NPC should face player
        }
    }

    //This method will use nodes to find a path to the destination.
    private void Pathfinding()
    {
        while(pathingState)
        {
            //Create Nodes, find path to location
        }
    }

    //This method will facilitate movement along the path to the destination.
    private void Travel()
    {
        while(travellingState)
        {
            //Follow path from Pathfinding to destination
        }
    }

    private void Passive()
    {
        while(passiveState)
        {
            //Passive behavior (no velocity, change in facing, etc.)
        }
    }
}