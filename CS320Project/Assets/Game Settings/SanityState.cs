using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class SanityState : MonoBehaviour
{

    [SerializeField]private SanityValues sanityValues;
    [SerializeField]private Slider sanitySlider;

    private IEnumerator coroutine;
    public float rate; //rate variable to increase or decreases speed of sanity meter
    public bool gainingSanity = false; //true if gaining sanity, false if not gaining
    public bool losingSanity = false; //true if losing sanity, false if not losing
    private bool isFPressed = false;

    public Volume post_process_volume;
    private Vignette vignette;
    private FilmGrain grain;
    public PlayerArms player_arms;
    public MusicItem music_player;
    public GameObject music_item;

    void Start()
    {
        post_process_volume.profile.TryGet(out vignette);
        post_process_volume.profile.TryGet(out grain);

        rate = 1f;
        sanitySlider.value = sanityValues.maxSanity;
        sanityValues.currentSanity = sanitySlider.value;
        //music_player = FindAnyObjectByType<MusicItem>();
        player_arms = FindAnyObjectByType<PlayerArms>();
        startSanityLoss();
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
        if( sanityValues.currentSanity == 0.0f) {

        }
        
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
        vignette.intensity.value = 1f;
        vignette.smoothness.value = 0.7f;
        grain.intensity.value = 1f;
        grain.response.value = 1f;


        
    }
    IEnumerator gainSanity(float rate) {
       
        while(sanityValues.currentSanity < sanityValues.maxSanity) {
            sanityValues.changeSanity(0.1f);
            sanitySlider.value = sanityValues.currentSanity * rate;
            yield return null;
        }
        vignette.intensity.value = 0f;
        vignette.smoothness.value = 0f;
        grain.intensity.value = 0f;
        grain.response.value = 0f;

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
