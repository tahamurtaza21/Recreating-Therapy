using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialoguePlay : MonoBehaviour
{
    public NPCConversation[] backGroundConversation;
    private int ConversationNumber = 0;

    [SerializeField] Color32 DialogueBoxColor;
    [SerializeField] Color32 DialogueTextColor;
    //public Rain RainController;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        ConversationManager.Instance.StartConversation(backGroundConversation[ConversationNumber]);
        ConversationNumber += 1;
        Debug.Log(ConversationNumber);
        Destroy(other.gameObject, 20);    
        ChangeColor();
    }

    void ChangeColor()  // this is to change the color of the dialogue text and panel when player talks to therapist
    {
        if(ConversationNumber > 2)
        {
            Debug.Log("Change Color");
            ConversationManager.Instance.DialogueText.color = DialogueTextColor;
            ConversationManager.Instance.DialogueBackground.color = DialogueBoxColor;
        }
    }
}
