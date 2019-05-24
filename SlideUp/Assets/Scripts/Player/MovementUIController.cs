using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUIController : MonoBehaviour {

	[SerializeField]
	public MovementUIView movementUIView;
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
    public void SetAnimationBasedOnCollisionType(Collision2D colliderType, Animator anim)
    {
        BorderProperties borderProperties = colliderType.gameObject.GetComponent<BorderProperties>();
        if (borderProperties.borderType.Equals("Right"))
            anim.SetFloat("Border", 1f);
        else if (borderProperties.borderType.Equals("Left"))
            anim.SetFloat("Border", 0);
        else if (borderProperties.borderType.Equals("Ground") | borderProperties.borderType.Equals("Ramp"))
            anim.SetFloat("Border", 0.5f);
    }

}
