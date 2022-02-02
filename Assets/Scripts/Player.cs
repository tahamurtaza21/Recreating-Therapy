using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private float moveAmount;
    void Update()
    {
        moveAmount = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        transform.Translate(moveAmount, 0, 0);
    }
}