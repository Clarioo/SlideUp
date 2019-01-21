using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevel : MonoBehaviour {


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
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("GameObjects/Player"));
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        // Setting Basic variables
        camFollow.isGameStarted = genTerrain.isGameStarted = true;
        playerMovement.isAlive = true;
        camFollow.maxHeight = 0;
        camFollow.gameObject.transform.position = new Vector3(0, 12, -10);
        gameUIControl.ScoreText.text = "0";

        //Hiding MenuPanel
        mainMenuPanel.SetActive(false);
    }
    
}
