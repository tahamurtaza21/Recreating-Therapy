using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if(ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                ConversationManager.Instance.PressSelectedOption();
            }
        }
    }
}