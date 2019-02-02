using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIControl : MonoBehaviour
{
    public int characterID;
    public int numberOfCharacters;
    //UI
    public Image characterLogo, unlockPanel;
    public Button unlock;
    public Text coinsText, characterCost;
    //GameData
    public CoinSystem coinSystem;
    public StartLevel startLevelData;
    // Use this for initialization
    void Start()
    {
        CheckAllPrefs();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

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
            coinSystem.coins -= Resources.Load<CharacterProperties>("GameObjects/Character" + characterID).characterCost;
            UpdateUI();
        }

    }
    void UpdateUI()
    {
        coinsText.text = coinSystem.coins.ToString();
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
}
