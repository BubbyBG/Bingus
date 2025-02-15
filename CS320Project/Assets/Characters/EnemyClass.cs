using UnityEngine;

/*  This will contain traits and behaviors more specific to
    enemies. This class inherits traits from NPCClass.
*/
public class EnemyClass : NPCClass
{
    
    //Fields...
    private float trackingRange = 10;   //Update range

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start code here
        base.Start();
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

            //Transition to pathing/travelling (Tracking Player)
            else if((aggressiveRange < playerDistance) && (playerDistance < trackingRange))
            {
                if(!(pathingState || travellingState))
                {
                    ClearState();
                    pathingState = true;
                    travelDestination = playerLocation;
                    Pathfinding();
                }
                else if(pathingState)
                {
                    travelDestination = playerLocation;
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

    private void Aggression()
    {
        //Enemy should face and attack player.
    }

    private int AttackPlayer()
    {
        //This will return a damage int
        return(0);
    }

}