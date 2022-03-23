using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using DigitalRuby.RainMaker;
using RavingBots.Water2D;
using UnityEngine.U2D;

public class DialoguePlay : MonoBehaviour
{
    public NPCConversation[] backGroundConversation;
    private int ConversationNumber = -1;
    [SerializeField] Color32 DialogueBoxColor;
    [SerializeField] Color32 DialogueTextColor;
    [SerializeField] Rain rainControl;

    [SerializeField] SpriteRenderer backgroundImage;
    [SerializeField] SpriteShapeRenderer platform;

    [SerializeField] Water2DMaterialScaler water;
    public bool atTherapist = false;
    GameObject Conversation;

    [SerializeField] float waveIntensityreduce = 0.2f;
    [SerializeField] float rainIntensityreduce = 0.1f;

    [SerializeField] float lightingBackgroundvalue = 0.07f;

    [SerializeField] float backgroundChangingSpeed = 0.37f;

    [SerializeField] GameObject Sun;


    void Start()
    {
        //StartCoroutine(SunUp());
        //StartCoroutine(BackgroundLight());
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        ConversationNumber += 1;
        ConversationManager.Instance.StartConversation(backGroundConversation[ConversationNumber]);
        Debug.Log(ConversationNumber);
        Destroy(other.gameObject);    
        
        if(backGroundConversation[ConversationNumber].tag == "Therapist")
        {
            ChangeEnvironment();
            atTherapist = true;
        }

        else if(backGroundConversation[ConversationNumber].tag == "Final")
        {
            FinalSettings();    
        }

    }

    void ChangeEnvironment()  // this is to change the color of the dialogue text and panel when player talks to therapist
    {
        Debug.Log("Change Color");
        ConversationManager.Instance.DialogueText.color = DialogueTextColor;
        ConversationManager.Instance.DialogueBackground.color = DialogueBoxColor;
        rainControl.RainScript.RainIntensity -=  rainIntensityreduce; // Control rain after meeting therapist
        water.WaveSpeed -= waveIntensityreduce;
        StartCoroutine(BackgroundLight());
        //BackgroundLighter();
        backgroundChangingSpeed += 0.1f;
    }

    void FinalSettings()
    {
        ConversationManager.Instance.DialogueText.color = DialogueTextColor;
        ConversationManager.Instance.DialogueBackground.color = DialogueBoxColor;
        rainControl.RainScript.RainIntensity =  0; // Control rain after meeting therapist
        water.WaveSpeed = 0;
        StartCoroutine(BackgroundLight());
        //BackgroundLighter();
        StartCoroutine(SunUp());
        backgroundChangingSpeed += 0.1f;
    }

    void BackgroundLighter()
    {
        backgroundImage.color = new Color
        (backgroundImage.color.r + lightingBackgroundvalue, 
        backgroundImage.color.g + lightingBackgroundvalue,
        backgroundImage.color.b + lightingBackgroundvalue);

        platform.color = new Color
        (platform.color.r + lightingBackgroundvalue, 
        platform.color.g + lightingBackgroundvalue,
        platform.color.b + lightingBackgroundvalue);
        
        Debug.Log(backgroundImage.color);
    }

    IEnumerator SunUp()
    {
        for (float i = Sun.transform.position.y; i <= 25f; i += 0.1f)
        {
            Sun.transform.position = new Vector3(Sun.transform.position.x, i, Sun.transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BackgroundLight()
    {
        //Debug.Log(backgroundImage.color);
        //Debug.Log(backgroundImage.color.r + lightingBackgroundvalue);

        for(float i = backgroundImage.color.r;
        i <= backgroundChangingSpeed;
        i += 0.01f)
        {
            //Debug.Log(i);

            Color c = backgroundImage.color;
            c.r = i;
            c.g = i;
            c.b = i;
            
            Color p = platform.color;
            p.r += 0.003f;
            p.g += 0.003f;
            p.b += 0.003f;

            platform.color = p;
            backgroundImage.color = c;

            //Debug.Log(c);

            yield return new WaitForSeconds(0.02f);
        }
    }
}
