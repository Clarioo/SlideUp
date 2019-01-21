using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProperties : MonoBehaviour {

    GameObject player;
    ParticleSystem partSystem;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        partSystem = GetComponent<ParticleSystem>();
        
	}
	
	// Update is called once per frame
	void Update () {
	}
}
