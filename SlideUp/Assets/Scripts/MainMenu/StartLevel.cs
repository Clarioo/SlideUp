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
    [SerializeField] GameState gameState;
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
        PlayerState playerState = player.GetComponent<PlayerState>();

        // Setting Basic variables
        playerState.isAlive = true;
        gameState.isGameStarted = true;
        gameState.maxHeight = 3;
        gameUIControl.scoreText.text = "0";
        gameUIControl.fireStartTime = Time.time;
        genTerrain.generateID = 0;

    }
    
}
