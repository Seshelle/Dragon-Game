using UnityEngine;
using System.Collections;

public class EnterFan : MonoBehaviour {

    public float forceAmount;
    public enum DropOff
    {
        none = 1, linear = 2, square = 3
    }
    public DropOff dropOffFunction = DropOff.none;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DragonController>())
        {
            other.GetComponent<DragonController>().SuspendStall(true);
        }
    }

	void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            float force = forceAmount;
            if (dropOffFunction == DropOff.linear)
            {
                force = force * (1 - Vector3.Distance(other.transform.position, transform.position)
                    / GetComponent<CapsuleCollider>().height);
            }
            //other.GetComponent<DragonController>().AddVector(transform.up, force);
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DragonController>())
        {
            other.GetComponent<DragonController>().SuspendStall(false);
        }
    }
}
