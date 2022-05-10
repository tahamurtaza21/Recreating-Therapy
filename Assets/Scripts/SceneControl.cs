using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private int currentSceneIndex;
    [SerializeField] GameObject GameName;
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject InstructionButton;
    [SerializeField] GameObject ExitButton;
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject BackButton;

    void Start() 
    {
        GameName.SetActive(true);
        PlayButton.SetActive(true);
        InstructionButton.SetActive(true);
        ExitButton.SetActive(true);
        Instructions.SetActive(false);
        BackButton.SetActive(false);
    }

    public void LoadGame()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    public void LoadInstructions()
    {
        GameName.SetActive(false);
        PlayButton.SetActive(false);
        InstructionButton.SetActive(false);
        ExitButton.SetActive(false);
        Instructions.SetActive(true);
        BackButton.SetActive(true);
    }

    public void GoBack()
    {
        GameName.SetActive(true);
        PlayButton.SetActive(true);
        InstructionButton.SetActive(true);
        ExitButton.SetActive(true);
        Instructions.SetActive(false);
        BackButton.SetActive(false);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
