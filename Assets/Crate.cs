using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public GameObject[] gems;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveDamage(float damage)
    {
        foreach(GameObject item in gems)
        {
            Instantiate(item);
            item.transform.position = transform.position;
        }
        Destroy(gameObject);
    }

}
