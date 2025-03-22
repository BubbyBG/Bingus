using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//I am testing the Update() method. More specifically, I am testing the TestUpdate() method,
//which is an exact copy of Update(), but with some added flags for testing and the ability
//to be called by a test method. I was having some trouble with getting yield return statements
//to cause Update() to be called, and this allowed me to get around that. The White Box Tests
//are designed to achieve branch coverage.

public class PlayModeTests
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    //My Black Box Tests were designed to make sure that the code goes to the correct
    //operation based on the player's distance. Because the distance calculation is done
    //using the Vector3.Distance() method, which always returns a positive value, a
    //negative value was not necessary to test for.
    [UnityTest]
    public IEnumerator LessThanAggressionRange()    //This is a Black Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = (npc.aggressionRange / 2);
        npc.TestUpdate();
        Assert.IsTrue(npc.testFlagA);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EdgeIsAggressionRange()    //This is a Black Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = npc.aggressionRange;
        npc.TestUpdate();
        Assert.IsTrue(npc.testFlagB);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MidRange()    //This is a Black Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = ((npc.aggressionRange + npc.trackingRange)/2);
        npc.TestUpdate();
        Assert.IsTrue(npc.testFlagB);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EdgeIsTrackingRange()    //This is a Black Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = npc.trackingRange;
        npc.TestUpdate();
        Assert.IsTrue(npc.testFlagC);
        yield return null;
    }

    [UnityTest]
    public IEnumerator GreaterThanTrackingRange()    //This is a Black Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = (npc.trackingRange + 5f);
        npc.TestUpdate();
        Assert.IsTrue(npc.testFlagC);
        yield return null;
    }

    //My White Box Tests were designed to get branch coverage of the Update()
    //(or rather the TestUpdate()) method. The main behavior of the class is run
    //by a coroutine that acts on boolean states. By testing the effectiveness of
    //the expected updates to boolean states from the Update() method, I can ensure
    //that the coroutine has the correct boolean values to work with.
    [UnityTest]
    public IEnumerator HPLessThanZero()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.healthPoints = -1;
        npc.TestUpdate();
        Assert.IsFalse(npc.isAlive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HPIsZero()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.healthPoints = 0;
        npc.TestUpdate();
        Assert.IsFalse(npc.isAlive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HPGreaterThanZero()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.healthPoints = 1;
        npc.TestUpdate();
        Assert.IsTrue(npc.isAlive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator BranchATrue()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = (npc.aggressionRange / 2);
        npc.aggressiveState = false;
        npc.TestUpdate();
        Assert.IsTrue(npc.aggressiveState);
        yield return null;
    }

    [UnityTest]
    public IEnumerator BranchAFalse()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = (npc.aggressionRange / 2);
        npc.aggressiveState = true;
        npc.TestUpdate();
        Assert.IsTrue(npc.aggressiveState);
        yield return null;
    }

    [UnityTest]
    public IEnumerator BranchBTrue()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = ((npc.aggressionRange + npc.trackingRange)/2);
        npc.pathingState = false;
        npc.travellingState = false;
        npc.TestUpdate();
        Assert.IsTrue(npc.pathingState);
        yield return null;
    }

    [UnityTest]
    public IEnumerator BranchBFalse()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = ((npc.aggressionRange + npc.trackingRange)/2);
        npc.pathingState = false;
        npc.travellingState = true;
        Vector3 testVector = new Vector3(12.2f,6f,-37.1234f);
        Vector3 origin = new Vector3(0f,0f,0f);
        npc.playerLocation = testVector;
        npc.TestUpdate();
        Assert.AreEqual(Vector3.Distance(origin,testVector),Vector3.Distance(origin,npc.travelDestination),0.001);
        yield return null;
    }

    [UnityTest]
    public IEnumerator BranchC()    //This is a White Box Test
    {
        var npc = new NPCClass();
        npc.playerDistance = (npc.trackingRange + 5f);
        npc.aggressiveState = true;
        npc.pathingState = true;
        npc.travellingState = true;
        npc.passiveState = false;
        npc.TestUpdate();
        Assert.IsTrue((!(npc.aggressiveState || npc.pathingState || npc.travellingState)) && npc.passiveState);
        yield return null;
    }


    //My Integration Test will check that the NPCClass spawns a PathNode while in the pathingState
    //that should then begin spawning more PathNodes.
    [UnityTest]
    public IEnumerator PathNodeIntegration()
    {
        var npc = new NPCClass();
        npc.pathingState = true;
        npc.TestStart();            //COMMENT OUT FOR REAL TEST
        npc.TestUpdate();           //COMMENT OUT FOR REAL TEST
        //npc.Pathfinding();        //UNCOMMENT FOR REAL TEST
        yield return new WaitForSeconds(2f);
        Assert.IsNotNull(npc.startingNode);
        yield return null;
    }


    [UnityTest]
    public IEnumerator MultiState()     //This is a White Box Test (Not for Branch Coverage)
    {                                   //It tests whether the npc is ever in more than 1 state.
        var npc = new NPCClass();
        npc.playerDistance = (npc.aggressionRange / 2);
        npc.TestUpdate();
        Assert.IsTrue(StateChecker(npc));

        npc.playerDistance = ((npc.aggressionRange + npc.trackingRange)/2);
        npc.TestUpdate();
        Assert.IsTrue(StateChecker(npc));

        npc.playerDistance = (npc.trackingRange + 5f);
        npc.TestUpdate();
        Assert.IsTrue(StateChecker(npc));
        yield return null;
    }

    public bool StateChecker(NPCClass npc)  //This is not a test, but is used by the MultiState() test.
    {
        int stateCount = 0;
        if(npc.pathingState)
        {
            stateCount += 1;
        }
        if(npc.travellingState)
        {
            stateCount += 1;
        }
        if(npc.aggressiveState)
        {
            stateCount += 1;
        }
        if(npc.passiveState)
        {
            stateCount += 1;
        }
        return(stateCount == 1);
    }
}
