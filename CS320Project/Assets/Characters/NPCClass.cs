using UnityEngine;
using System.Collections;

//  This will contain the basic traits (fields/methods) of
//  NPCs. Enemy-specific traits will be inherited from here.

public class NPCClass : MonoBehaviour
{ 
    
    //Fields...
    public Vector3 NPCLocation;
    public Transform Player;    //Assign to Transform of player object in Unity to get information
    protected Vector3 NPCVelocity = new Vector3(0f,0f,0f);
    public float NPCRadius = 1.5f;     //Update size as needed
    protected float playerDistance;
    [SerializeField]
    protected float aggressionInterval = 0.2f; //Time interval for updating facing
    public GameObject player;   //Assign to player object in Unity to get information
    [SerializeField]
    protected Vector3 travelDestination = new Vector3(0f,0f,0f);
    protected Vector3 playerLocation = new Vector3(0f,0f,0f);
    public bool isAlive;
    public int healthPoints = 10;   //Update HP as needed
    protected bool pathingState = false;
    protected bool travellingState = false;
    protected bool aggressiveState = false;
    protected bool passiveState = true;
    [SerializeField]
    protected int moveSpeed = 2;  //Update speed as needed
    [SerializeField]
    protected float trackingRange = 10f;  //Update range as needed
    [SerializeField]
    protected float aggressionRange = 3f;   //Update range as needed
    [SerializeField]
    protected float attackInterval = 1f; //Time between attacks - update as necessary
    public int dealtDamage = 1; //Update damage number as needed
    protected Coroutine AggressionRoutine;
    protected Coroutine BehaviorRoutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start code here
        isAlive = true;
        if(BehaviorRoutine == null)     //Begin behavioral coroutine
        {
            BehaviorRoutine = StartCoroutine(Behavior());
        }
        else                            //Destroy entity if it gets to this state
        {
            isAlive = false;
            Destroy(this,0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update code here - behavior logic and time-based things will go here.
        //Assign behavior/state based on logic, call methods as necessary.

        if(healthPoints <= 0)   //Kill NPC if no HP - will be facillitated by Behavior Coroutine
        {
            isAlive = false;
        }

        //Find locations of player and NPC, then get distance
        //playerLocation = player.transform.position;
        playerLocation = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        NPCLocation = this.transform.position;
        //playerLocation = Player.position;
        playerDistance = Vector3.Distance(NPCLocation, playerLocation);
            
        //Transition to aggression
        if(playerDistance < aggressionRange)
        {
            if(!aggressiveState)
            {
                ClearState();
                aggressiveState = true;
            }
        }

        //Transition to pathing/travelling (Tracking Player)
        else if((aggressionRange < playerDistance) && (playerDistance < trackingRange))
        {
            if(!(pathingState || travellingState))
            {
                ClearState();
                travelDestination = playerLocation;
                pathingState = true;
            }
            else
            {
                travelDestination = playerLocation;
            }
        }
        
        else
        {
            ClearState();
            passiveState = true;
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
    private void Aggression()
    {
        //Enemy should face and attack player.
        while(aggressiveState)
        {
            if(AggressionRoutine == null)
            {
                AggressionRoutine = StartCoroutine(AttackPlayer());
            }
        }
        if(AggressionRoutine != null)
        {
            StopCoroutine(AggressionRoutine);
        }
    }

    private IEnumerator AttackPlayer()
    {
        transform.LookAt(Player);
        //This is where attack will occur.
        yield return new WaitForSeconds(attackInterval);
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

    protected void Behavior()
    {
        while(isAlive)
        {
            //Should never be in more than one state at a time
            if(aggressiveState)
            {
                Aggression();
            }

            if(pathingState)
            {
                Pathfinding();
            }

            if(travellingState)
            {
                Travel();
            }

            if(passiveState)
            {
                Passive();
            }
        }
        
        Destroy(this,0.0f); //Only gets here if no HP - kill NPC (automatically ends coroutines)
    }
}