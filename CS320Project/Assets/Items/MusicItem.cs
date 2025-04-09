using UnityEngine;

public class MusicItem : ItemClass //inherits from ItemClass
{

    private SanityState sanityState;
    
    void Start()
    {
       
        sanityState = FindAnyObjectByType<SanityState>(); //there will only be one sanity state so this shouldn't find anything else
        
    }

    void Update()
    {   
        //print("workjing");
        if (inWorldSpace && Input.GetKeyDown("f")) {
            print("starting sanity gain");
            sanityState.startSanityGain();
        }
        if(!inWorldSpace) {
            print("starting sanity loss");
            sanityState.startSanityLoss();
        }
    }
}
