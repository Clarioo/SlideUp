using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour {

    //
    public int coins;

    //Components
    MainMenuUIControl mainMenuUIControl;

	// Use this for initialization
	void Start () {
        mainMenuUIControl = GetComponent<MainMenuUIControl>();

        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
            PlayerPrefs.SetInt("Coins", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveCoins(int collectedCoins)
    {
        coins += collectedCoins;
        mainMenuUIControl.UpdateUI();
        PlayerPrefs.SetInt("Coins", coins);
    }
}
