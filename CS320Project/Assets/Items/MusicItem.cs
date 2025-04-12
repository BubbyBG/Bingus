using UnityEngine;

public class MusicItem : MonoBehaviour 
{

    private GameObject player;
    private SanityState sanityState;
    private float cooldownTimer;
    private float musicTime = 1000f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sanityState = player.GetComponent<SanityState>(); //there will only be one sanity state so this shouldn't find anything else
        cooldownTimer = 0f;
    }

    void Update()
    {   

        
        
        if(cooldownTimer <= 0) {
            if (Input.GetKeyDown("f")) {
                
                
                cooldownTimer = musicTime;
                playMusic();
                sanityState.startSanityGain();
            }
            else
            {
                stopMusic();
                
                sanityState.startSanityLoss(); //if not pressing f and music off start sanity loss again
            }
        }
        else {
            cooldownTimer -= 120f * Time.deltaTime;
        }
        
    }

    
    public void playMusic() 
    {
        FindAnyObjectByType<AudioManager>().Play("hooppler");
    }

    public void stopMusic() 
    {   
        FindAnyObjectByType<AudioManager>().Stop("hooppler");
    }
    
}
