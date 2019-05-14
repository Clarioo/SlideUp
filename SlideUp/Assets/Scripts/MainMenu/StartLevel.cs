using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevel : MonoBehaviour {

    //GameData
    public int currentCharacterID;

    //UI Components
    public Image rightGate, leftGate;
    public GameObject mainMenuPanel;
    public CameraFollow camFollow;
    public GameUIController gameUIControl;
    public GenerateTerrain genTerrain;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        // Generating player
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("GameObjects/Character" + currentCharacterID));
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        // Setting Basic variables
        playerMovement.isAlive = true;
        camFollow.isGameStarted = genTerrain.isGameStarted = true;
        camFollow.maxHeight = 3;
        gameUIControl.scoreText.text = "0";
        gameUIControl.fireStartTime = Time.time;
        genTerrain.generateID = 0;

    }
    
}
