using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUIView : MonoBehaviour {

    [SerializeField] GameObject viewFinder;
    [SerializeField] GameObject shootArrow;
    [SerializeField] SpriteRenderer viewFinderArrow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DisplayCalculatedViewfinder(float shootRange, float angle)
    {
       viewFinderArrow.enabled = true;
       viewFinder.transform.localScale = new Vector3(10, shootRange, 1);
       viewFinder.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void ResetViewFinder()
    {
        viewFinder.transform.rotation = Quaternion.identity;
        viewFinderArrow.enabled = false;
    }
}
