using UnityEngine;
using System.Collections;

//  This will contain the basic traits (fields/methods) of
//  NPCs. Enemy-specific traits will be inherited from here.

public class NPCClass : MonoBehaviour
{ 
    
    //Fields...
    //SerializeFields?
    public Vector3 NPCLocation = new Vector3(0f,0f,0f);
    public Transform Player;    //Figure out how to assign to player object to get information
    protected Vector3 NPCVelocity = new Vector3(0f,0f,0f);
    public float NPCRadius = 1.5f;     //Update size as needed
    protected float playerDistance;
    protected float aggressionInterval = 0.2f; //Time interval for updating facing
    public GameObject player;
    protected Vector3 travelDestination = new Vector3(0f,0f,0f);
    protected Vector3 playerLocation = new Vector3(0f,0f,0f);
    public bool isAlive;
    protected bool pathingState = false;
    protected bool travellingState = false;
    protected bool aggressiveState = false;
    protected bool passiveState = true;
    protected int moveSpeed = 2;  //Change to be an appropriate movespeed for character.
    protected float trackingRange = 10f;  //Update range
    protected Coroutine AggressionRoutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start code here
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Update code here - behavior logic and time-based things will go here.
        //Assign behavior/state based on logic, call methods as necessary.
        if(isAlive)
        {
            //Find locations of player and NPC, then get distance
            playerLocation = player.transform.position;
            playerLocation = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
            NPCLocation = this.transform.position;
            //playerLocation = Player.position;
            playerDistance = Vector3.Distance(NPCLocation, playerLocation);
            
            //Transition to aggression
            if(playerDistance < trackingRange)
            {
                if(!aggressiveState)
                {
                    ClearState();
                    aggressiveState = true;
                }

                Aggression();
            }

            //Transition to pathing/travelling (Going to Destination)
            else if(Vector3.Distance(travelDestination, NPCLocation) > NPCRadius)
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
                    //travellingState will be assigned within Pathfinding method.
                }
                else
                {
                    //travellingState is already assigned at this point.
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
    protected void ClearState()
    {
        pathingState = false;

        travellingState = false;
        
        aggressiveState = false;
        if(AggressionRoutine != null)
        {
            StopCoroutine(AggressionRoutine);
        }
        
        passiveState = false;
    }

    //This method will control the aggressive response toward the player.
    protected void Aggression()
    {
        while(aggressiveState)
        {
            if(AggressionRoutine == null)
            {
                AggressionRoutine = StartCoroutine(FacePlayer());
            }
        }
        if(AggressionRoutine != null)
        {
            StopCoroutine(AggressionRoutine);
        }
    }

    //This method will use nodes to find a path to the destination.
    protected void Pathfinding()
    {
        while(pathingState)
        {
            //Create Nodes, find path to location
        }
    }

    //This method will facilitate movement along the path to the destination.
    protected void Travel()
    {
        while(travellingState)
        {
            //Follow path from Pathfinding to destination
        }
    }

    protected void Passive()
    {
        while(passiveState)
        {
            //Passive behavior (no velocity, change in facing, etc.)
        }
    }

    protected IEnumerator FacePlayer()
    {
        transform.LookAt(Player);
        yield return new WaitForSeconds(aggressionInterval);
    }
}