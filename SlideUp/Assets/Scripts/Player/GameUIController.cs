using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    int score;

    public GameObject player;
    public CameraFollow camFollow;
    public StartLevel startLevel;
    public Text ScoreText, topScore;
    public Image newRecordImage;
	// Use this for initialization
	void Start () {
        startLevel = GetComponent<StartLevel>();
        FillUI();
    }

    // Update is called once per frame
    void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if (player.transform.position.y > int.Parse(ScoreText.text))
            {
                score = (int)player.transform.position.y;
                ScoreText.text = score.ToString();
            }
        }
        
	}
    public void SaveScore()
    {

        startLevel.mainMenuPanel.SetActive(true); //enable main menu panel

        if (PlayerPrefs.HasKey("TopScore")) //setting top score if reached
        {
            if (score > PlayerPrefs.GetInt("TopScore"))
            {
                PlayerPrefs.SetInt("TopScore", score);
                topScore.text = "TOP: " + score.ToString();
                //newRecordImage.enabled = true; //to change when image ll be added
            }
            else
            {
                //newRecordImage.enabled = false;
            }
        }
        else
            PlayerPrefs.SetInt("TopScore", score);

        FillUI();
    }
    void FillUI()
    {
        if (PlayerPrefs.HasKey("TopScore"))
        {
            topScore.text = "TOP: " + PlayerPrefs.GetInt("TopScore").ToString(); // set topscore
        }

    }

   
}
