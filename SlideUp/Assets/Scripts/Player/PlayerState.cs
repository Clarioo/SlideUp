using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public bool isAlive;
    public bool isGrounded;

    GameObject mainCamera;
    GameObject eventHandler;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Die()
    {
        //Finding objects
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        eventHandler = GameObject.FindGameObjectWithTag("EventHandler");

        //Get objects Components
        CameraFollow camFollow = mainCamera.GetComponent<CameraFollow>();
        GameUIController gameUIControl = eventHandler.GetComponent<GameUIController>();
        CoinSystem coinSystem = eventHandler.GetComponent<CoinSystem>();
        CollectingSystem collectingSystem = GetComponent<CollectingSystem>();
        GenerateTerrain generateTerrain = eventHandler.GetComponent<GenerateTerrain>();

        //Set Variables
        camFollow.isGameStarted = false;
        //Save score and Collected Coins
        coinSystem.SaveCoins(collectingSystem.collectedCoins);
        switch (generateTerrain.gameMode)
        {
            case "DeathRun":
                gameUIControl.SaveDeathRunScore();
                break;
            case "TimeTrial":
                gameUIControl.SaveTimeTrialScore();
                break;

        }

        Destroy(this.gameObject);

    }
    public bool IsPlayerOnSecureDistance(int currentMaxGenerateHeight)
    {
        if (currentMaxGenerateHeight < gameObject.transform.position.y)
            return true;
        else
            return false;
    }
}
