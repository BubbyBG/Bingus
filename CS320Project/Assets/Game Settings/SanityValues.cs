using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


/*
    This script will store the needed sanity value info to be accessed across the game environment, all scripts that interact with the sanity state will
    need access to these values
*/
[CreateAssetMenu(fileName = "SanityValues", menuName = "Scriptable Objects/test")]
public class SanityValues : ScriptableObject
{
    public float maxSanity = 100f; //max sanity value possible
    public float currentSanity = 100f;  //set current sanity to max, will be changed later

    public void changeSanity(float amount) {
       
        
        currentSanity = Mathf.Clamp(currentSanity + amount, 0, maxSanity);
        
        if(currentSanity == 0) {
            Debug.Log("gone insane");
        }
        
    }

}
