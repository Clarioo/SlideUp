using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {

    public float generationStartPosY;
    public int generateID;
    float borderGenerationRange = 5f;
   
    //ENVIRONMENT
    //Borders
    public GameObject leftBorder, rightBorder;
    //Obstacles
    public GameObject ramp, stickwheel, spikes;
    //Coins and Bonuses
    public GameObject coin;

    //Components
    PlayerMovement playerMove;
    PlayerState playerState;
    [SerializeField] GameState gameState; 


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void LoadEnvironmentObjects(int mapID)
    {
        leftBorder = Resources.Load<GameObject>("EnvironmentObjects/MAP" + mapID + "/LeftBorder");
        rightBorder = Resources.Load<GameObject>("EnvironmentObjects/MAP" + mapID + "/RightBorder");
        ramp = Resources.Load<GameObject>("EnvironmentObjects/MAP" + mapID + "/Ramp");
        stickwheel = Resources.Load<GameObject>("EnvironmentObjects/MAP" + mapID + "/Stick");
        spikes = Resources.Load<GameObject>("EnvironmentObjects/MAP" + mapID + "/Spike");
        StartCoroutine(GenerationTimer(1f));

    }
    void GenerateBorders()
    {
        if (gameState.isGameStarted & gameState.gameMode.Equals("DeathRun")) // generating environment (if game is started)
        {
            int borderHeight = 16;
            int currentMaxBuildHeight = 72 + borderHeight * generateID;
            int generatingSecureDistance = 72;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerState = player.GetComponent<PlayerState>();
                playerMove = player.GetComponent<PlayerMovement>();
                if (playerState.IsPlayerOnSecureDistance(currentMaxBuildHeight - generatingSecureDistance))//Checking that player went up after last building
                {
                    //Borders and ramps
                    GameObject generatedLeftBorder = GameObject.Instantiate(leftBorder, new Vector3(-borderGenerationRange, generationStartPosY + (16 * generateID), 0), Quaternion.identity, gameState.currentMap.transform);
                    GameObject generatedRightBorder = GameObject.Instantiate(rightBorder, new Vector3(borderGenerationRange, generationStartPosY + (16 * generateID), 0), Quaternion.identity, gameState.currentMap.transform);
                     for (int rampID = -1; rampID < 2; rampID++)
                    {
                        GameObject generatedRamp = GameObject.Instantiate(ramp, new Vector3(Random.Range(-borderGenerationRange + 0.5f, borderGenerationRange - 0.5f), 28 + (16 * generateID) + rampID * 4, 0), Quaternion.identity, gameState.currentMap.transform);

                    }
                    //Obstacles
                    if (generateID % 8 == 1 || generateID % 8 == 6)
                    {
                        GameObject generatedStickwheel = GameObject.Instantiate(stickwheel, new Vector3(Random.Range(-borderGenerationRange + 0.5f, borderGenerationRange - 0.5f), generationStartPosY + 18 + (6 * generateID), 0), Quaternion.identity, gameState.currentMap.transform);
                    }
                    else if (generateID % 8 == 4)
                    {
                        GameObject generatedSpikes = GameObject.Instantiate(spikes, new Vector3(Random.Range(-borderGenerationRange, borderGenerationRange), generationStartPosY + 50 + (8 * generateID), 0), Quaternion.identity, gameState.currentMap.transform);
                    }
                    //Coins and bonuses
                    if (generateID % 6 == 2)
                    {
                        GameObject generatedCoin = GameObject.Instantiate(coin, new Vector3(Random.Range(-borderGenerationRange + 0.5f, borderGenerationRange - 0.5f), generationStartPosY + (8 * generateID), 0), Quaternion.identity, gameState.currentMap.transform);
                    }
                    generateID++;
                }
            }
            
        }
            
    }
    IEnumerator GenerationTimer(float seconds) 
    {
        GenerateBorders();
        yield return new WaitForSeconds(seconds);
        StartCoroutine(GenerationTimer(seconds));
    }
}
