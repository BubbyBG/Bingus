using UnityEngine;

public class ProjectileSlowClass : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed;
    }

    public void Shoot(Vector3 posFrom, Vector3 dirTo)
    {
        moveDirection = dirTo;
    }
}
