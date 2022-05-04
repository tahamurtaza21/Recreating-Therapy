using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Restart : MonoBehaviour
{
    string String = "The End";
    [SerializeField] TextMeshProUGUI endingText;
    // Update is called once per frame

    //private bool Active = false;
    
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
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
