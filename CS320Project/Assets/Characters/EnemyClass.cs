using UnityEngine;

/*  This will contain traits and behaviors more specific to
    enemies. This class inherits traits from NPCClass.
*/
public class EnemyClass : NPCClass
{
    
    //Fields...

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start code here
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Update code here
        ClearState();
        //Find a way to get player location first and update the local Vector3 to reflect.
        playerDistance = Distance(System.Numerics.Vector3 NPCLocation, System.Numerics.Vector3 playerLocation);
        if(playerDistance < aggressionRange)
        {
            aggressiveState = true;
            Aggression();
        }

        else if(playerDistance < trackingRange) //Unique to EnemyClass
        {
            pathingState = true;
            travelDestination = playerLocation;
            Pathfinding();
        }

        else
        {
            passiveState = true;
        }
    }

    /*private void ClearState() //Currently no functionality beyond NPCClass's ClearState method.
    {
        base.ClearState();
        aggressiveState = false;
    }*/


}