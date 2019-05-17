using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	Camera mycam;
    PlayerMovement playerMovement;
    [SerializeField] private GameState gameState;
    // Use this for initialization
    void Start () {
		mycam = GetComponent<Camera>();
		
	}
	
	// Update is called once per frame
	void Update () {
        MovingCameraOnPlayer();
	}
    void MovingCameraOnPlayer()
    {
        if (target == null && gameState.isGameStarted) //Finding Player  
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            playerMovement = target.GetComponent<PlayerMovement>();
        }
        if (target & playerMovement != null && playerMovement.isAlive && target.position.y > gameState.maxHeight) //Centre Camera on Player
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, target.position.y - 2, 0), 0.1f) + new Vector3(0, 0.5f, -10f); // Second Vector is Camera offset
            gameState.maxHeight = target.position.y; //Locking camera on highest score in current game
        }
    }
    
}
