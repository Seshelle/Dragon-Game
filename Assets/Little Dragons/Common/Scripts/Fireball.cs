using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float Force = 10;
    public float Radius = 10;
    public GameObject explotion;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 2 && !other.gameObject.CompareTag("Player"))
        {
            Rigidbody impact = other.gameObject.GetComponent<Rigidbody>();

            if (impact)
            {
                impact.AddExplosionForce(100f * Force, transform.position, 100f * Radius);
            }

            Destroy(gameObject);
            //create fireball explotion after collides
            GameObject fireballexplotion = Instantiate(explotion);
            fireballexplotion.transform.position = transform.position;

            Destroy(fireballexplotion, 2f);
        }
    }
    
}
