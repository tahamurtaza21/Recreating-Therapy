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

    [SerializeField] GameObject Umbrella;
    [SerializeField] GameObject UmbrellaSprite;

    float playerGravityAtStart;

    void Awake() 
    {
        UmbrellaSprite.SetActive(false);    
    }

    void Start() 
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        umbrellaAnimator = GetComponentInChildren<Animator>();
        myrigidbody2D = GetComponent<Rigidbody2D>();
        mydialoguePlay = GetComponent<DialoguePlay>(); 
        playerGravityAtStart = myrigidbody2D.gravityScale; 
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Walk();
        FlipSprite();
        Climb();
    }

    void Walk()
    {
        Vector2 playervelocity = new Vector2(moveInput.x  * moveSpeed, myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = playervelocity; 
    }

    void Climb()
    {
        if(!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myrigidbody2D.gravityScale = playerGravityAtStart;
            //playerAnimator.SetBool("isClimbing", false);
            return;
        }

        myrigidbody2D.gravityScale = 0f;
        Vector2 climbvelocity = new Vector2(myrigidbody2D.velocity.x, moveInput.y * moveSpeed);
        myrigidbody2D.velocity = climbvelocity;
        
        bool playerHasVerticalSpeed = Mathf.Abs(myrigidbody2D.velocity.y) > Mathf.Epsilon;
        //playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnUmbrellaPress()
    {
        UmbrellaSprite.SetActive(false);
        if(mydialoguePlay.atTherapist == true)
        {
            Umbrella.SetActive(true);
            UmbrellaSprite.SetActive(false);
            moveSpeed = 3f;
        }
    }
    void OnUmbrellaLetGo()
    {
        Umbrella.SetActive(false);
        UmbrellaSprite.SetActive(true);
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