using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animation animRecoil;
    public AnimationClip clip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animRecoil = transform.GetChild(0).GetComponent<Animation>();
        animRecoil.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animRecoil.Play();
        }
    }
}
