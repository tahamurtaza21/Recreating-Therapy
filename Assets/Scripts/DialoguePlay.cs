using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using DigitalRuby.RainMaker;
using RavingBots.Water2D;
public class DialoguePlay : MonoBehaviour
{
    public NPCConversation[] backGroundConversation;
    private int ConversationNumber = -1;
    [SerializeField] Color32 DialogueBoxColor;
    [SerializeField] Color32 DialogueTextColor;
    [SerializeField] Rain rainControl;

    [SerializeField] Water2DMaterialScaler water;
    public bool atTherapist = false;
    GameObject Conversation;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        ConversationNumber += 1;
        ConversationManager.Instance.StartConversation(backGroundConversation[ConversationNumber]);
        Debug.Log(ConversationNumber);
        Destroy(other.gameObject);    
        
        if(backGroundConversation[ConversationNumber].tag == "Therapist")
        {
            ChangeColor();
            atTherapist = true;
        }
    }

    void ChangeColor()  // this is to change the color of the dialogue text and panel when player talks to therapist
    {
        Debug.Log("Change Color");
        ConversationManager.Instance.DialogueText.color = DialogueTextColor;
        ConversationManager.Instance.DialogueBackground.color = DialogueBoxColor;
        rainControl.RainScript.RainIntensity -=  0.1f; // Control rain after meeting therapist
        water.WaveSpeed -= -0.2f;
    }
}
