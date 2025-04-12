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

    public GameObject music_object;
    public ItemClass music_player;
   // public GameObject music_object;

    private float pp_rate = 0.001f;

    private GameObject currItem;
    void Start()
    {

        post_process_volume = GetComponentInChildren<Volume>();
       
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
        currItem = player_arms.GetHeldItem();
        music_player = currItem.GetComponent<ItemClass>();
        
        stopMusic();
        if( sanityValues.currentSanity == 0.0f) {
            vignette.intensity.value = Mathf.Min(vignette.intensity.value + pp_rate, 1);
            vignette.intensity.value = Mathf.Min(vignette.intensity.value + pp_rate, 1);
            grain.intensity.value =  Mathf.Min(grain.intensity.value + pp_rate, 1);
    
        }
        else if( sanityValues.currentSanity == sanityValues.maxSanity) {
            vignette.intensity.value = Mathf.Max(vignette.intensity.value - pp_rate, 0);
            vignette.intensity.value = Mathf.Max(vignette.intensity.value - pp_rate, 0);
            grain.intensity.value =  Mathf.Max(grain.intensity.value - pp_rate, 0);
           
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
        


        
    }
    IEnumerator gainSanity(float rate) {
       
        while(sanityValues.currentSanity < sanityValues.maxSanity) {
            sanityValues.changeSanity(0.1f);
            sanitySlider.value = sanityValues.currentSanity * rate;
            yield return null;
        }
       

    }
   

    
    public void stopMusic() {

        
        if(music_player.type!= ItemClass.itemType.musicBox || !(music_player.inWorldSpace)) {
            
            FindAnyObjectByType<AudioManager>().Stop("hooppler");
            
            startSanityLoss();
            
        }
    }
    
  
}
