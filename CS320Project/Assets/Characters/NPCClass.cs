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
    public float playerDistance;
    [SerializeField]
    protected float aggressionInterval = 0.2f; //Time interval for updating facing
    public GameObject player;   //Assign to player object in Unity to get information
    [SerializeField]
    public Vector3 travelDestination = new Vector3(0f,0f,0f);
    public Vector3 playerLocation = new Vector3(0f,0f,0f);
    public bool isAlive = true;
    public int healthPoints = 10;   //Update HP as needed
    public bool pathingState = false;
    public bool travellingState = false;
    public bool aggressiveState = false;
    public bool passiveState = true;
    public bool testFlagA = false;
    public bool testFlagB = false;
    public bool testFlagC = false;
    [SerializeField]
    protected int moveSpeed = 2;  //Update speed as needed
    [SerializeField]
    public float trackingRange = 10f;  //Update range as needed
    [SerializeField]
    public float aggressionRange = 3f;   //Update range as needed
    [SerializeField]
    public float attackInterval = 1f; //Time between attacks - update as necessary
    public int dealtDamage = 1; //Update damage number as needed
    protected Coroutine AggressionRoutine;
    protected Coroutine BehaviorRoutine;
    public PathNode startingNode;
    public NodeList nodelist;
    //public GameObject nodelist;

    //Empty Constructor for testing purposes
    public NPCClass() {}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start code here
        isAlive = true;
        if(BehaviorRoutine == null)     //Begin behavioral coroutine
        {
            BehaviorRoutine = StartCoroutine("Behavior");
        }
        else                            //Destroy entity if it gets to this state
        {
            isAlive = false;
            Destroy(this,0.0f);
        }
        //Instantiate(nodelist,transform);
        nodelist = new NodeList();
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
        //UNCOMMENT playerDistance LINE AFTER TESTING!!!
        //playerDistance = Vector3.Distance(NPCLocation, playerLocation);
            
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
    protected void Aggression()
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
    public void Pathfinding()
    {
        while(pathingState)
        {
            //Create Nodes, find path to location
            /*if(startingNode == null)
            {
                startingNode = new PathNode();
            }*/
            if(nodelist.isEmpty)
            {
                nodelist.startingNode = new PathNode(this.transform.position, 0);
                nodelist.isEmpty = false;
                //nodelist.startingNode.Spread();
                nodelist.startingNode.north = nodelist.startingNode.SpreadNorth();
                nodelist.startingNode.south = nodelist.startingNode.SpreadSouth();
                nodelist.startingNode.east = nodelist.startingNode.SpreadEast();
                nodelist.startingNode.west = nodelist.startingNode.SpreadWest();
            }
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

    public void TestUpdate()
    {
        //This whole method is only for testing purposes. It is the same as Update, but with testFlags.
        //Assign behavior/state based on logic, call methods as necessary.

        if(healthPoints <= 0)   //Kill NPC if no HP - will be facillitated by Behavior Coroutine
        {
            isAlive = false;
        }

        //Find locations of player and NPC, then get distance
        //playerLocation = player.transform.position;
        //playerLocation = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        //NPCLocation = this.transform.position;
        //playerLocation = Player.position;
        //playerDistance = Vector3.Distance(NPCLocation, playerLocation);
            
        //Transition to aggression
        if(playerDistance < aggressionRange)
        {
            testFlagA = true;
            if(!aggressiveState)
            {
                ClearState();
                aggressiveState = true;
            }
        }

        //Transition to pathing/travelling (Tracking Player)
        else if((aggressionRange <= playerDistance) && (playerDistance < trackingRange))
        {
            testFlagB = true;
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
            testFlagC = true;
            ClearState();
            passiveState = true;
        }
        
    }

    public void TestStart()
    {
        //Start code here
        isAlive = true;
        if(BehaviorRoutine == null)     //Begin behavioral coroutine
        {
            BehaviorRoutine = StartCoroutine("Behavior");
        }
        else                            //Destroy entity if it gets to this state
        {
            isAlive = false;
            Destroy(this,0.0f);
        }
    }
}