using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SanityState : MonoBehaviour
{

    [SerializeField]private SanityValues sanityValues;
    [SerializeField]private Slider sanitySlider;

    private IEnumerator coroutine;
    public float rate; //rate variable to increase or decreases speed of sanity meter
    
    void Start()
    {
        rate = 1f;
        sanitySlider.value = sanityValues.maxSanity;
        sanityValues.currentSanity = sanitySlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        //key to start sanity loss for testing purposes, full implementation will have sanity always decreasing unless music playing
        if(Input.GetKeyDown("o")) {
            startSanityLoss();
        }
        
    }

    public void startSanityGain() {
        StopAllCoroutines();
        StartCoroutine(gainSanity(rate));
    }
    public void startSanityLoss() {
            StopAllCoroutines();
            StartCoroutine(loseSanity(rate));
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
  
}
