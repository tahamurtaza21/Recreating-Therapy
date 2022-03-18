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
    DialoguePlay mydialoguePlay;

    [SerializeField] GameObject Umbrella;
    [SerializeField] GameObject UmbrellaSprite;

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
    }

    void Update()
    {
        Walk();
    }

    void Walk()
    {
        Vector2 playervelocity = new Vector2(moveInput.x  * moveSpeed, myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = playervelocity; 
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
            moveSpeed = 4f;
        }
    }
    void OnUmbrellaLetGo()
    {
        Umbrella.SetActive(false);
        UmbrellaSprite.SetActive(true);
        moveSpeed = 2f;
    }

}