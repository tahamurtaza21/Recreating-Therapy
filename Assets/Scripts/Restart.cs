using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Restart : MonoBehaviour
{
    string String = "The End";
    string String1 = "The game you played right now aims to show how therapy is conducted for a patient with anxiety. There are many forms of therapy but often it is just a conversation that can help clarify the patient's state of mind. The umbrella plays the role of the therapist as they provide you with means to protect yourself from the overwhelming environment around you. I hope you enjoyed it! Please give feedback upon leaving!";
    [SerializeField] TextMeshProUGUI endingText;
    [SerializeField] TextMeshProUGUI extraText;
    // Update is called once per frame
    
    void Start() 
    {        
        StartCoroutine(AutoType());
    }

    IEnumerator AutoType()
    {
        foreach(char letter in String.ToCharArray())
        {
            endingText.text += letter;
            yield return new WaitForSeconds(1.5f);
        }

        foreach(char letter in String1.ToCharArray())
        {
            extraText.text += letter;
            yield return new WaitForSeconds(0.07f);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
