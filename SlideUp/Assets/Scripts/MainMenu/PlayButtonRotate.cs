using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonRotate : MonoBehaviour {
    int rotationMultiplier = 10;
	// Use this for initialization
	void Start () {
        StartCoroutine(InduceRotationMultiplier(2));
	}
	
	// Update is called once per frame
	void Update () {
        ButtonRotation();
    }
    void ButtonRotation()
    {
        var angles = transform.rotation.eulerAngles;
        angles.z += Time.deltaTime * rotationMultiplier;
        transform.rotation = Quaternion.Euler(angles);
    }
    
    IEnumerator InduceRotationMultiplier(float overlay)
    {
        rotationMultiplier = Random.Range(-4, 4) * 10;
        overlay = Random.Range(1, 3);
        yield return new WaitForSeconds(overlay);
        StartCoroutine(InduceRotationMultiplier(overlay));
    }
}
