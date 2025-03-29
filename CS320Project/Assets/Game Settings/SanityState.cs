using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class SanityState : MonoBehaviour
{

    [SerializeField]private SanityValues sanityValues;
    [SerializeField]private Slider sanitySlider;

    private IEnumerator coroutine;
    public float rate; //rate variable to increase or decreases speed of sanity meter
    public bool gainingSanity = false; //true if gaining sanity, false if not gaining
    public bool losingSanity = false; //true if losing sanity, false if not losing
    private bool isFPressed = false;

    public PlayerArms player_arms;
    public MusicItem music_player;
    public GameObject music_item;

    void Start()
    {
        rate = 1f;
        sanitySlider.value = sanityValues.maxSanity;
        sanityValues.currentSanity = sanitySlider.value;
        //music_player = FindAnyObjectByType<MusicItem>();
        player_arms = FindAnyObjectByType<PlayerArms>();
    }

    // Update is called once per frame
    void Update()
    {
        //key to start sanity loss for testing purposes, full implementation will have sanity always decreasing unless music playing
        /*
        if(Input.GetKeyDown("o")) {
            startSanityLoss();
        }
        */
        isFPressed = Input.GetKeyDown("f");
        playMusic(isFPressed);
        
    }

    public void startSanityGain() {
        
        StopAllCoroutines();
        if(Time.timeScale == 1f) {
            gainingSanity = true;
            losingSanity = false;
            
            StartCoroutine(gainSanity(rate));
        }
        else
            gainingSanity = false;
    }
    public void startSanityLoss() {

        StopAllCoroutines();
        if(Time.timeScale == 1f) {
            losingSanity = true;
            gainingSanity = false;
            StartCoroutine(loseSanity(rate));
        }
        else
            losingSanity = false;
    }

    IEnumerator loseSanity(float rate) {
       
        while(sanityValues.currentSanity > 0) {
            sanityValues.changeSanity(-0.01f);
            sanitySlider.value = sanityValues.currentSanity * rate;
           
            yield return null;
        }
        
    }
    IEnumerator gainSanity(float rate) {
       
        while(sanityValues.currentSanity < sanityValues.maxSanity) {
            sanityValues.changeSanity(0.1f);
            sanitySlider.value = sanityValues.currentSanity * rate;
            yield return null;
        }
    }
   

    public void playMusic(bool isFPressed) {

        if(player_arms.GetHeldItem() != music_item) {
            FindAnyObjectByType<AudioManager>().Stop("Creep");
            print("starting sanity loss");
            startSanityLoss();
            //print("workjing");
        }
        else if (music_player.held && isFPressed) {
                FindAnyObjectByType<AudioManager>().Play("Creep");
                print("starting sanity gain");
                startSanityGain();
        }
    }
  
}
