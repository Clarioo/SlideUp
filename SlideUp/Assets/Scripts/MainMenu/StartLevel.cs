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
    public void PlayButtonEventer()
    {
        StartCoroutine(OpenCloseGate(0.01f));
        //MenuPanel
        
        mainMenuPanel.SetActive(false);
    }
    public void StartGame()
    {
        // Generating player
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("GameObjects/Character" + currentCharacterID));
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        // Setting Basic variables
        playerMovement.isAlive = true;
        camFollow.isGameStarted = genTerrain.isGameStarted = true;
        camFollow.maxHeight = 0;
        gameUIControl.ScoreText.text = "0";

    }
    IEnumerator OpenCloseGate(float openMS)
    {
        for(int i = 0; i < 50; i++)
        {
            leftGate.rectTransform.localPosition += new Vector3(10f, 0, 0);
            rightGate.rectTransform.localPosition += new Vector3(-10f, 0, 0);
            yield return new WaitForSeconds(openMS);
        }
        camFollow.transform.position = new Vector3(0, 12, -10);
        for (int i = 0; i < 50; i++)
        {
            leftGate.rectTransform.localPosition += new Vector3(-10f, 0, 0);
            rightGate.rectTransform.localPosition += new Vector3(10f, 0, 0);
            yield return new WaitForSeconds(openMS);
        }
        StartGame();
    }
}
