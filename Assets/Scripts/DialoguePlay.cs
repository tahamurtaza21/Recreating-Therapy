using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialoguePlay : MonoBehaviour
{
    public NPCConversation[] backGroundConversation;

    private int ConversationNumber = 0;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        ConversationManager.Instance.StartConversation(backGroundConversation[ConversationNumber]);
        ConversationNumber += 1;
        Destroy(other.gameObject);    
        ChangeColor();
    }

    void ChangeColor()  // this is to change the color of the dialogue text and panel when player talks to therapist
    {
        if(ConversationNumber > 2)
        {
            ConversationManager.Instance.DialogueText.color = Color.cyan;
            ConversationManager.Instance.DialogueBackground.color = Color.green; 
        }
    }
}
