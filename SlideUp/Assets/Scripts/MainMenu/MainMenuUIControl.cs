using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIControl : MonoBehaviour
{
    public int characterID;
    public int numberOfCharacters;
    string gamemode;
    //UI
    public Image characterLogo, unlockPanel, settingsPanel;
    public GameObject deathRunScore, deathRunTopScore, timeTrialTime, timeTrialTop;
    public GameObject mainMenuPanel;
    public Image touchLockPanel;
    public Image rightGate, leftGate;
    public Button unlock;
    public Text coinsText, characterCost;

    //GameData
    public CoinSystem coinSystem;
    public StartLevel startLevelData;
    public CameraFollow camFollow;
    public GenerateTerrain generateTerrain;
    // Use this for initialization
    void Start()
    {
        CheckAllPrefs();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonListeners();
    }
    public void UpdateCurrentCharacter(int switchCharacter)
    {
        characterID += switchCharacter;
        if (characterID < 1)
            characterID = numberOfCharacters;
        else if (characterID > numberOfCharacters)
            characterID = 1;
        characterLogo.sprite = Resources.Load<CharacterProperties>("GameObjects/Character" + characterID).characterTexture;
        if (PlayerPrefs.HasKey("Character" + characterID))
        {
            if(PlayerPrefs.GetString("Character" + characterID).Equals("Unlocked")){
                startLevelData.currentCharacterID = characterID;
                unlockPanel.gameObject.SetActive(false);
                unlock.gameObject.SetActive(false);
            }
            else
            {
                unlockPanel.gameObject.SetActive(true);
                unlock.gameObject.SetActive(true);
                characterCost.text = Resources.Load<CharacterProperties>("GameObjects/Character" + characterID).characterCost.ToString();
            }
        }
    }
    public void UnlockCharacter()
    {
        if(coinSystem.coins>= Resources.Load<CharacterProperties>("GameObjects/Character" + characterID).characterCost)
        {
            PlayerPrefs.SetString("Character" + characterID, "Unlocked");
            startLevelData.currentCharacterID = characterID;
            unlockPanel.gameObject.SetActive(false);
            unlock.gameObject.SetActive(false);
            coinSystem.SaveCoins(-Resources.Load<CharacterProperties>("GameObjects/Character" + characterID).characterCost);
            UpdateUI();
        }

    }
    public void UpdateUI()
    {
        coinsText.text = coinSystem.coins.ToString();
    }
    void ButtonListeners()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//End or Pause Game
        {
            Application.Quit();
        }
    }
    public void PlayButtonEventer()
    {
        StartCoroutine(OpenCloseGate(0.01f, "Start Game"));
        //MenuPanel

        mainMenuPanel.SetActive(false);
    }
    public void GamemodeEventer(string gamemodeType)
    {
        generateTerrain.gameMode = gamemodeType;
        gamemode = gamemodeType;
        switch (gamemodeType)
        {
            case "DeathRun":
                OpenClosePanels("SetDeathRun");
                break;
            case "TimeTrial":
                OpenClosePanels("SetTimeTrial");
                break;
        }
    }
    void CheckAllPrefs()
    {
        PlayerPrefs.SetString("Character1", "Unlocked");
        for(int i = 1; i<= numberOfCharacters; i++)
        {
            if(PlayerPrefs.HasKey("Character" + i) == false)
            {
                PlayerPrefs.SetString("Character" + i, "Locked");
            }
        }
    }
    public void OpenClosePanels(string eventType)
    {
        StartCoroutine(OpenCloseGate(0.01f, eventType));
    }
    IEnumerator OpenCloseGate(float openMS, string animationType)
    {
        touchLockPanel.gameObject.SetActive(true);
        for (int i = 0; i < 50; i++)
        {
            leftGate.rectTransform.localPosition += new Vector3(10f, 0, 0);
            rightGate.rectTransform.localPosition += new Vector3(-10f, 0, 0);
            yield return new WaitForSeconds(openMS);
        }
        switch (animationType)
        {
            case "Start Game":
                camFollow.transform.position = new Vector3(0, 10, -10);
                generateTerrain.SetCurrentMap(animationType, 1);//switch 1 to var mapID
                break;
            case "Open Settings":
                settingsPanel.gameObject.SetActive(true);
                break;
            case "Close Settings":
                settingsPanel.gameObject.SetActive(false);
                break;
            case "SetDeathRun":
                timeTrialTime.SetActive(false);
                timeTrialTop.SetActive(false);
                deathRunTopScore.SetActive(true);
                deathRunScore.SetActive(true);
                break;
            case "SetTimeTrial":
                timeTrialTime.SetActive(true);
                timeTrialTop.SetActive(true);
                deathRunTopScore.SetActive(false);
                deathRunScore.SetActive(false);
                break;
        }
        for (int i = 0; i < 50; i++)
        {
            leftGate.rectTransform.localPosition += new Vector3(-10f, 0, 0);
            rightGate.rectTransform.localPosition += new Vector3(10f, 0, 0);
            yield return new WaitForSeconds(openMS);
        }
        switch (animationType)
        {
            case "Start Game":
                startLevelData.StartGame();
                break;
            
        }
        touchLockPanel.gameObject.SetActive(false);
    }

}
