using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    //Basic variables
    Vector2 movementSpeed;
    float collisionReductor = 1;
    float resolutionScaler = 20;
    
    //Swipe Vectors
    Vector2 moveVector;
    Vector2 dragVector;
    Vector2 startPoint, endPoint;

    //Components
    [SerializeField] MovementUIController movementUIController;
    [SerializeField] PlayerState playerState;
    Rigidbody2D rigidbodyMove;
    public SpriteRenderer sprRenderer;
    Animator anim;
    //Event Components
    GameObject mainCamera;
    GameObject eventHandler;

    
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
        if (playerState.isAlive)
        {
            if (Input.touchCount != 0 & playerState.isGrounded == true)
            {

                Touch moveTouch = Input.GetTouch(0); 
                //Getting Touch Swipe
                if (moveTouch.phase == TouchPhase.Began)
                {
                    startPoint = moveTouch.position;
                }
                dragVector = (moveTouch.position - startPoint) * resolutionScaler /Screen.height;//getting current swipe Vector
                movementUIController.GenerateViewFinder(dragVector);
                if (moveTouch.phase == TouchPhase.Ended)
                {
                    endPoint = moveTouch.position;
                    movementSpeed = (startPoint - endPoint) / Screen.height * 2*resolutionScaler;// speed = swipe vector/resolution*constance_multiplier
                    if (movementSpeed.magnitude > resolutionScaler)
                        rigidbodyMove.velocity = movementSpeed * (resolutionScaler/movementSpeed.magnitude);
                    else
                        rigidbodyMove.velocity = movementSpeed;
                    movementUIController.movementUIView.ResetViewFinder();
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

   
    
    private void OnCollisionStay2D(Collision2D collision) 
    {
        playerState.isGrounded = true;
        anim.SetBool("IsColliding", true);
        movementUIController.SetAnimationBasedOnCollisionType(collision, anim);
    }
    private void OnCollisionExit2D(Collision2D collision) //cancelling collider animation
    {
        playerState.isGrounded = false;
        anim.SetBool("IsColliding", false);
    }
    private void OnCollisionEnter2D(Collision2D collision) //Triggering death
    {
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            playerState.Die();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {

        }
        if (collision.gameObject.CompareTag("Border"))
        {
            InitializingFriction(collision);
        }
    }

    void InitializingFriction(Collision2D collision)
    {
        BoxCollider2D boxCollider = collision.gameObject.GetComponent<BoxCollider2D>();
        StartCoroutine(DecreaseMovementOnCollision(boxCollider.friction));
    }


    IEnumerator DecreaseMovementOnCollision(float friction)
    {
        rigidbodyMove.gravityScale = 5.1f - 4*friction;
        collisionReductor = friction/10;
        for(int decreaseTime = 0; decreaseTime<2*friction; decreaseTime++)
        {
            rigidbodyMove.gravityScale += collisionReductor;
            yield return new WaitForSeconds(0.2f);
        }
        rigidbodyMove.gravityScale = 5.1f;
    }

}
