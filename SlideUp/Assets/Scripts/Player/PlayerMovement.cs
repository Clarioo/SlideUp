using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    //Basic variables
    public bool isAlive;
    bool isGrounded;
    
    //Swipe Vectors
    Vector2 moveVector;
    Vector2 dragVector;
    Vector2 startPoint, endPoint;

    //Components
    Rigidbody2D rigidbodyMove;
    public SpriteRenderer sprRenderer;
    Animator anim;
    //Event Components
    GameObject mainCamera;
    GameObject eventHandler;

    //UI
    public GameObject shootArrow;
    public GameObject viewFinder;
    public SpriteRenderer viewFinderArrow;
	// Use this for initialization
	void Start () {
        rigidbodyMove = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Jump();
	}
    void Jump()
    {
        if (isAlive)
        {
            if (Input.touchCount != 0 & isGrounded == true)
            {

                Touch moveTouch = Input.GetTouch(0); 
                //Getting Touch Swipe
                if (moveTouch.phase == TouchPhase.Began)
                {
                    startPoint = moveTouch.position;
                }
                dragVector = (moveTouch.position - startPoint) * 20f /Screen.height;//getting current swipe Vector
                GenerateViewFinder(dragVector);// Function to Fix
                if (moveTouch.phase == TouchPhase.Ended)
                {
                    endPoint = moveTouch.position;
                    rigidbodyMove.velocity = (startPoint - endPoint)/Screen.height*35f; // speed = swipe vector/resolution*constance_multiplier
                    viewFinder.transform.rotation = Quaternion.identity;
                    viewFinderArrow.enabled = false;
                }
                
            }
            //setting fly animation 
            anim.SetFloat("Speed", rigidbodyMove.velocity.magnitude);
            if (rigidbodyMove.velocity.y < 0)
                sprRenderer.flipY = true;
            else
                sprRenderer.flipY = false;
        }
    }
    public void Die()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        eventHandler = GameObject.FindGameObjectWithTag("EventHandler");
        CameraFollow camFollow = mainCamera.GetComponent<CameraFollow>();
        GameUIController gameUIControl = eventHandler.GetComponent<GameUIController>();
        camFollow.isGameStarted = false;
        gameUIControl.SaveScore();

        Destroy(this.gameObject);
        
    }
   
    void GenerateViewFinder(Vector3 jumpVector) //To fix
    {
        viewFinderArrow.enabled = true;
        float shootRange = jumpVector.magnitude;
        viewFinder.transform.localScale = new Vector3(10, shootRange, 1);
        Vector3 vectorToTarget = jumpVector - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.x, -vectorToTarget.y) * Mathf.Rad2Deg;
        viewFinder.transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    private void OnCollisionStay2D(Collision2D collision) 
    {
        isGrounded = true;
        anim.SetBool("IsColliding", true);
        BorderProperties borderProperties = collision.gameObject.GetComponent<BorderProperties>();
        //setting animation for each type of obstacle
        if (borderProperties.borderType.Equals("Right"))
            anim.SetFloat("Border", 1f);
        else if (borderProperties.borderType.Equals("Left"))
            anim.SetFloat("Border", 0);
        else if (borderProperties.borderType.Equals("Ground") | borderProperties.borderType.Equals("Ramp"))
            anim.SetFloat("Border", 0.5f);
    }
    private void OnCollisionExit2D(Collision2D collision) //cancelling collider animation
    {
        isGrounded = false;
        anim.SetBool("IsColliding", false);
    }
    private void OnCollisionEnter2D(Collision2D collision) //Triggering death
    {
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            Die();
        }
    }

}
