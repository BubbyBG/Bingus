
/*
    this class will inherit the NPCClass to get a hold of the aggresiveState variable to determine a change in the rate of sanity decrease
    Won't be testable until an actual NPC is present as one is needed to be in the vicinity of the player
*/
public class SanityEffectNPC : NPCClass
{
    private SanityState sanityState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sanityState = FindAnyObjectByType<SanityState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(aggressiveState) {
            sanityState.rate = 5f;
        }
        
    }
}
