using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public bool isGameStarted;
    public float maxHeight;
    public string gameMode;


    public GameObject currentMapLocation;
    public GameObject currentMap;
    [SerializeField] GenerateTerrain generateTerrain;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetCurrentMap(string gamemodeType, int mapID)
    {
        Destroy(currentMapLocation.transform.GetChild(0).gameObject);
        currentMap = GameObject.Instantiate(Resources.Load<GameObject>("Maps/Map" + mapID + "." + gameMode), new Vector3(2.14f, -18, -2), Quaternion.identity);
        currentMap.transform.parent = currentMapLocation.transform;
        generateTerrain.LoadEnvironmentObjects(mapID);
    }
}
