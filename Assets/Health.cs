using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public bool immuneToFire = false;
    public bool immuneToMelee = false;

    public void Damage(float damage, int type)
    {
        if (!(type == 1 && immuneToFire) && !(type == 2 && immuneToMelee))
        {
            BroadcastMessage("ReceiveDamage", damage);
        }
    }

}
