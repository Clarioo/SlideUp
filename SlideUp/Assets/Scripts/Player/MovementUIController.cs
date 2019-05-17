using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUIController : MonoBehaviour {

	[SerializeField]
	private MovementUIView movementUIView;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GenerateViewFinder(Vector3 jumpVector)
	{
		float shootRange = jumpVector.magnitude;
		Vector3 vectorToTarget = jumpVector - transform.position;
		float angle = Mathf.Atan2(jumpVector.x, -jumpVector.y) * Mathf.Rad2Deg;
        movementUIView.DisplayCalculatedViewfinder(shootRange, angle);

	}
}
