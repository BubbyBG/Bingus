using UnityEngine;
using System.Collections;

/*  This will contain traits and behaviors more specific to
    enemies. This class inherits traits from NPCClass.

public class EnemyClass : NPCClass
{
    
    //Fields...
    private float aggressionRange = 3f;   //Update range
    private float attackInterval = 1f; //Time between attacks - update as necessary
    public int dealtDamage = 0;

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
            //Find locations of player and NPC, then get distance
            NPCLocation = this.transform.position;
            playerLocation = Player.position;
            playerDistance = Vector3.Distance(NPCLocation, playerLocation);
            
            //Transition to aggression
            if(playerDistance < aggressionRange)
            {
                if(!aggressiveState)
                {
                    base.ClearState();
                    aggressiveState = true;
                }

                Aggression();
            }
            
            //Transition to pathing/travelling (Tracking Player)
            else if((aggressionRange < playerDistance) && (playerDistance < trackingRange))
            {
                if(!(pathingState || travellingState))
                {
                    base.ClearState();
                    pathingState = true;
                    travelDestination = playerLocation;
                    base.Pathfinding();
                }
                else if(pathingState)
                {
                    travelDestination = playerLocation;
                    base.Pathfinding();
                }
                else
                {
                    base.Travel();
                }
            }

            else
            {
                base.ClearState();
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
        while(aggressiveState)
        {
            if(AggressionRoutine == null)
            {
                AggressionRoutine = StartCoroutine(AttackPlayer());
            }
        }
        if(AggressionRoutine != null)
        {
            StopCoroutine(AggressionRoutine)
        }
    }

    private IEnumerator AttackPlayer()
    {
        transform.LookAt(Player);
        //This is where attack will occur.
        yield return new WaitForSeconds(attackInterval);
    }


