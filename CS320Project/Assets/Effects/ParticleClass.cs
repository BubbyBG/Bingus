using UnityEngine;

public class ParticleClass : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
