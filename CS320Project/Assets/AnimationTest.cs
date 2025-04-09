using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    //private Animation animRecoil;
    public Animator animator;
    //public AnimationClip fireAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            print(animator.enabled);
            print(animator.isActiveAndEnabled);

            animator.Play("Fire", 0 , 0.25f);
        }
    }
}
