using UnityEngine;
using System.Collections;

public class ChangeTarget : MonoBehaviour {

    public Transform[] targets;
    public KeyCode key = KeyCode.T;
    int current;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key))
        {
            current++;
            GetComponent<UnityStandardAssets.Cameras.FreeLookCam>().SetTarget(targets[(current% targets.Length)]);
        }
	}
}
