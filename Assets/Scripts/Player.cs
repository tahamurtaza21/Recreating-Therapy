using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    public NPCConversation backGroundConversation;

    private float moveAmount;
    void Update()
    {
        moveAmount = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        transform.Translate(moveAmount, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        ConversationManager.Instance.StartConversation(backGroundConversation);
    }



}
