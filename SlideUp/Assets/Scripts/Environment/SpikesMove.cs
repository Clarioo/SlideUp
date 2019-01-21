using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Spikes Rotating
        var angles = transform.rotation.eulerAngles;
        angles.z += Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(angles); 
	}
}
