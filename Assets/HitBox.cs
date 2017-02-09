using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    public float damage;

    private float cooldown = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && cooldown <= 0)
        {
            other.GetComponent<Health>().Damage(damage, 2);
            cooldown = 0.5f;
        }
    }

}
