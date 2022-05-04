using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 3f;
    private float moveAmount;
    private Animator playerAnimator;
    private Animator umbrellaAnimator;
    private Rigidbody2D myrigidbody2D;

    private CapsuleCollider2D bodyCollider; 
    DialoguePlay mydialoguePlay;

    bool UmbrellaUsing = false;

    [SerializeField] GameObject umbrella;

    float playerGravityAtStart;

    void Start() 
    {
        umbrella.SetActive(false);
        playerAnimator = gameObject.GetComponent<Animator>();
        umbrellaAnimator = umbrella.GetComponent<Animator>();
        myrigidbody2D = GetComponent<Rigidbody2D>();
        mydialoguePlay = GetComponent<DialoguePlay>(); 
        playerGravityAtStart = myrigidbody2D.gravityScale; 
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Walk();
        usingUmbrella();
        FlipSprite();
        Climb();
    }

    void Walk()
    {
        Vector2 playervelocity = new Vector2(moveInput.x  * moveSpeed, myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = playervelocity;         
    }

    void usingUmbrella()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody2D.velocity.x) > Mathf.Epsilon;

        umbrella.transform.position = new Vector3
        (this.transform.position.x + 0.1f,
        this.transform.position.y + 0.1f,
        this.transform.position.z);

        if(UmbrellaUsing == true && playerHasHorizontalSpeed)
        {
            //Debug.Log("Using Umbrella");

            playerAnimator.SetBool("isSadWalking",false);
            playerAnimator.SetBool("isWalking",true);
            playerAnimator.SetBool("Umbrella",true);
        }
        else if(UmbrellaUsing == false && playerHasHorizontalSpeed)
        {
            //Debug.Log("Not Using Umbrella");
            
            playerAnimator.SetBool("isSadWalking",true);
            playerAnimator.SetBool("isWalking",false);
            playerAnimator.SetBool("Umbrella",false);
        }
        else if(UmbrellaUsing == true && playerHasHorizontalSpeed == false)
        {
            playerAnimator.SetBool("isSadWalking",false);
            playerAnimator.SetBool("isWalking",false);
            playerAnimator.SetBool("Umbrella",true);
        }
        else // for idle
        {
            playerAnimator.SetBool("isSadWalking",false);
            playerAnimator.SetBool("isWalking",false);
            playerAnimator.SetBool("Umbrella",false);
        }
    }
    void Climb()
    {
        if(!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myrigidbody2D.gravityScale = playerGravityAtStart;
            //playerAnimator.SetBool("isClimbing", false);
            //umbrella.SetActive(true);
            return;
        }

        myrigidbody2D.gravityScale = 0f;
        Vector2 climbvelocity = new Vector2(myrigidbody2D.velocity.x, moveInput.y * moveSpeed);
        myrigidbody2D.velocity = climbvelocity;
        
        bool playerHasVerticalSpeed = Mathf.Abs(myrigidbody2D.velocity.y) > Mathf.Epsilon;
        //playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        //umbrella.SetActive(false);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    void OnUmbrellaPress()
    {
        if(mydialoguePlay.atTherapist == true)
        {
            UmbrellaUsing = true;
            Debug.Log("therapist");
            Debug.Log(UmbrellaUsing);
            umbrellaAnimator.SetBool("isPressingDown", true);
            //umbrella.SetActive(true);
            moveSpeed = 3f;
            Debug.Log(transform.position);
            Debug.Log(umbrella.transform.position);
        }
    }
    void OnUmbrellaLetGo()
    {
        UmbrellaUsing = false;
        umbrellaAnimator.SetBool("isPressingDown", false);
        Debug.Log(UmbrellaUsing);
        //umbrella.SetActive(false);
        moveSpeed = 1.5f;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody2D.velocity.x), 1f);
        }
    }
}