using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {
	public Transform target;
    public bool isGameStarted;
    public float maxHeight;
	Camera mycam;
    PlayerMovement playerMovement;
    // Use this for initialization
    void Start () {
		mycam = GetComponent<Camera>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null && isGameStarted) //Finding Player  
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
            playerMovement = target.GetComponent<PlayerMovement>();
		}
		if (target & playerMovement != null && playerMovement.isAlive && target.position.y > maxHeight) //Centre Camera on Player
		{
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, target.position, 0.1f) + new Vector3(0,0.6f,-10); // Second Vector is Camera offset
            maxHeight = target.position.y; //Locking camera on highest score in current game
		}
	}
    
}
