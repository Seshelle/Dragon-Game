using UnityEngine;
using System.Collections;

public class GemScript : MonoBehaviour {

    public GameObject particle;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value, Random.value, Random.value));

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        //remove gem and create particle effect when player picks it up
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject pickup = Instantiate(particle);
            pickup.transform.position = transform.position;
            Destroy(pickup, 2f);
        }
    }
}
