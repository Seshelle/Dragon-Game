using UnityEngine;
using System.Collections;

public class EnterWater : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DragonController>())
        {
            other.GetComponent<DragonController>().IsInWater = true;
        }
        if (other.CompareTag("MainCamera"))
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
            RenderSettings.fogDensity = 0.1f;
        }
        
    }   
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DragonController>())
        {
            other.GetComponent<DragonController>().IsInWater = false;
        }
        if (other.CompareTag("MainCamera"))
        {
            RenderSettings.fog = false;
        }
    }



}
