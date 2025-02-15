using UnityEngine;

/*  This will contain the basic traits (fields/methods) of
    NPCs. Enemy-specific traits will be inherited from here.

public class NPCClass : MonoBehaviour
{
    
    //Fields...
    //ALL OF THESE NEED TO BE INSTANTIATED. SerializeFields?
    public Vector3 NPCLocation;
    private Vector3 NPCVelocity;
    private float playerDistance = 5000;  //This will be determined using the player's coordinates and the NPC's coordinates.
    private Vector3 travelDestination;
    private Vector3 playerLocation;
    private bool pathingState = false;       //Keeping for now, but may not actually need the bools...
    private bool travellingState = false;
    private bool aggressiveState = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start code here - include things like location, facing, starting velocity...
        NPCVelocity = new Vector3(0,0,0);
        travelDestination = new Vector3(0,0,0); //Might be okay to leave?
        playerLocation = new Vector3(5000,5000,5000);    //Needs to be updated by getting actual player location.

    }

    // Update is called once per frame
    void Update()
    {
        //Update code here - behavior logic and time-based things will go here.
        //Assign behavior/state based on logic, call methods as necessary.
        ClearState();
        //Find a way to get player location first and update the local Vector3 to reflect.
        playerDistance = Distance(System.Numerics.Vector3 NPCLocation, System.Numerics.Vector3 playerLocation);
        if(playerDistance < aggressionRange)
        {
            aggressiveState = true;
            Aggression();
        }

        else
        {
            passiveState = true;
        }

    }

    //This method clears the state (all bools) that the NPC is in.
    private void ClearState()
    {
        pathingState = false;
        travellingState = false;
        aggressiveState = false;
    }

    private void Pathfinding()
    {
        //Work on this later...
    }

    private void Aggression()
    {
        //Add this later...
    }
}*/