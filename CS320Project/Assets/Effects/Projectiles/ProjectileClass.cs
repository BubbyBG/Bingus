using UnityEngine;

public class ProjectileClass : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 posFrom, Vector3 dirTo)
    {
        Debug.DrawRay(posFrom, dirTo, Color.white, 100f);
        RaycastHit hit;
        if (Physics.Raycast(posFrom, dirTo, out hit, 100f))
        {
            
            print(hit.transform.gameObject.ToString());
            Debug.DrawLine(hit.point, hit.point + hit.normal * 2f);
            ItemClass hitThing = hit.transform.GetComponent<ItemClass>();
            if (hitThing != null)
            {
                //create particle effect
                
            }
        }
    }
}
