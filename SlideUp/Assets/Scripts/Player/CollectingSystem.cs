using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingSystem : MonoBehaviour {

    public int collectedCoins;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collectedCoins++;
            Destroy(collision.gameObject);
        }
    }
}
