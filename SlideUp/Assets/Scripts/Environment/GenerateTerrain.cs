using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {


    public bool isGameStarted;
    //Borders
    public GameObject leftBorder, rightBorder;
    public GameObject ramp, stickwheel, spikes;
    float borderGenerationRange = 5f;

    int generateID;
	// Use this for initialization
	void Start () {
        StartCoroutine(GenerationTimer(1f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void GenerateBorders()
    {
        if (isGameStarted) // generating environment (if game is started)
        {
            //Borders and ramps
            GameObject generatedLeftBorder = GameObject.Instantiate(leftBorder, new Vector3(-borderGenerationRange, 40 + (16 * generateID), 0), Quaternion.identity);
            GameObject generatedRightBorder = GameObject.Instantiate(rightBorder, new Vector3(borderGenerationRange, 40 + (16 * generateID), 0), Quaternion.identity);
            GameObject generatedLeftBorderRamp = GameObject.Instantiate(ramp, generatedLeftBorder.transform.position + new Vector3(0, Random.Range(-4,4), 0), Quaternion.identity, generatedLeftBorder.transform);
            GameObject generatedRightBorderRamp = GameObject.Instantiate(ramp, generatedRightBorder.transform.position + new Vector3(0, Random.Range(-4, 4), 0), Quaternion.identity, generatedRightBorder.transform);
            //Obstacles
            if (generateID % 8 == 1 || generateID % 8 == 6)
            {
                GameObject generatedStickwheel = GameObject.Instantiate(stickwheel, new Vector3(Random.Range(-borderGenerationRange + 0.5f, borderGenerationRange - 0.5f), 34 + (6 * generateID), 0), Quaternion.identity);
            }
            else if(generateID % 8 == 4)
            {
                GameObject generatedSpikes = GameObject.Instantiate(spikes, new Vector3(Random.Range(-borderGenerationRange, borderGenerationRange), 150 + (8 * generateID), 0), Quaternion.identity);
            }
            else
            {
                GameObject generatedRamp = GameObject.Instantiate(ramp, new Vector3(Random.Range(-borderGenerationRange + 0.5f, borderGenerationRange - 0.5f), 24 + (16 * generateID), 0), Quaternion.identity);
                GameObject generatedRamp2 = GameObject.Instantiate(ramp, new Vector3(Random.Range(-borderGenerationRange + 0.5f, borderGenerationRange - 0.5f), 32 + (16 * generateID), 0), Quaternion.identity);

            }
            generateID++;
        }
    }
    IEnumerator GenerationTimer(float seconds) //Generating Coroutine
    {
        GenerateBorders();
        yield return new WaitForSeconds(seconds);
        StartCoroutine(GenerationTimer(seconds));
    }
}
