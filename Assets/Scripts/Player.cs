using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private float moveAmount;
    private Animator animator;

    void Start() 
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        moveAmount = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(moveAmount, 0, 0);
    }
}