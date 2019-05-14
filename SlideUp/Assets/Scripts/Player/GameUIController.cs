using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    int score;
    int complete;
    float time;

    int mapHeight = 10;
    
    public float fireStartTime;

    public GameObject player;
    public CameraFollow camFollow;
    public GenerateTerrain generateTerrain;
    public StartLevel startLevel;
    public Text scoreText, timeText, topScore, timeTrialTopTime, timeTrialComplete;
    public Image newRecordImage;
	// Use this for initialization
	void Start () {
        startLevel = GetComponent<StartLevel>();
        FillUI("DeathRun");
    }

    // Update is called once per frame
    void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if (player.transform.position.y > int.Parse(scoreText.text))
            {
                if (generateTerrain.gameMode.Equals("DeathRun")) {
                    score = (int)player.transform.position.y;
                    scoreText.text = score.ToString();
                }
                else if (generateTerrain.gameMode.Equals("TimeTrial"))
                {
                time = (int)(Time.time - fireStartTime);
                timeText.text = time.ToString() + "s";
                }
            }
        }
        
	}
    public void SaveDeathRunScore()
    {

        startLevel.mainMenuPanel.SetActive(true); //enable main menu panel

        if (PlayerPrefs.HasKey("DeathRunTopScore1")) //setting top score if reached
        {
            if (score > PlayerPrefs.GetInt("DeathRunTopScore1"))
            {
                PlayerPrefs.SetInt("DeathRunTopScore1", score);
                topScore.text = score.ToString();
                //newRecordImage.enabled = true; //to change when image ll be added
            }
            else
            {
                //newRecordImage.enabled = false;
            }
        }
        else
            PlayerPrefs.SetInt("DeathRunTopScore1", score);

        FillUI("DeathRun");
    }
    public void SaveTimeTrialScore()
    {
        startLevel.mainMenuPanel.SetActive(true);
        if (PlayerPrefs.HasKey("TimeTrialTopComplete1"))
        {
            if (PlayerPrefs.HasKey("TimeTrialTopTime1"))
            {
                if(score/mapHeight == PlayerPrefs.GetInt("TimeTrialTopComplete1"))
                {
                    
                    if(time < PlayerPrefs.GetInt("TimeTrialTopTime1")){
                        PlayerPrefs.SetInt("TimeTrialTopComplete1", score / mapHeight);
                        PlayerPrefs.SetInt("TimeTrialTopTime1", (int)time);
                        time = (int)time;
                        timeTrialTopTime.text = time.ToString() + "s";
                        complete = score / mapHeight;
                        timeTrialComplete.text = complete.ToString() + "%";
                    }
                }
                else if(score / mapHeight > PlayerPrefs.GetInt("TimeTrialTopComplete1")){
                    PlayerPrefs.SetInt("TimeTrialTopComplete1", score / mapHeight);
                    PlayerPrefs.SetInt("TimeTrialTopTime1", (int)time);
                    time = (int)time;
                    timeTrialTopTime.text = time.ToString() + "s";
                    complete = score / mapHeight;
                    timeTrialComplete.text = complete.ToString() + "%";
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("TimeTrialTopComplete1", 0);
            PlayerPrefs.SetInt("TimeTrialTopTime1", 1000);
        }
        FillUI("TimeTrial");
    }
    
    void FillUI(string gameMode)
    {
        if (gameMode.Equals("DeathRun") & PlayerPrefs.HasKey("DeathRunTopScore1"))
        {
            topScore.text = PlayerPrefs.GetInt("DeathRunTopScore1").ToString(); // set topscore
        }
        if (gameMode.Equals("TimeTrial") & PlayerPrefs.HasKey("TimeTrialTopTime1"))
        {
            timeTrialComplete.text = PlayerPrefs.GetInt("TimeTrialTopComplete1").ToString();
            timeTrialTopTime.text = PlayerPrefs.GetInt("TimeTrialTopTime1").ToString();
        }

    }

   
}
