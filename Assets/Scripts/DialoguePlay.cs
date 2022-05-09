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

    SpriteRenderer characterImage;
    [SerializeField] SpriteShapeRenderer platform;

    [SerializeField] Water2DMaterialScaler water;
    public bool atTherapist = false;
    GameObject Conversation;

    [SerializeField] float waveIntensityreduce = 0.2f;
    [SerializeField] float rainIntensityreduce = 0.1f;

    [SerializeField] float lightingBackgroundvalue = 0.07f;

    [SerializeField] float backgroundChangingSpeed = 0.20f;

    [SerializeField] GameObject Music;

    [SerializeField] GameObject RestartButton;

    Player playerScript;

    Animator playerAnimator;
    [SerializeField] Animator backGroundAnimator;

    [SerializeField] GameObject umbrella;

    [SerializeField] GameObject birds;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
        characterImage = GetComponent<SpriteRenderer>();
        //StartCoroutine(SunUp());
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Therapist" || other.gameObject.tag == "Triggers" || other.gameObject.tag == "Final")
        {
            ConversationNumber += 1;
            ConversationManager.Instance.StartConversation(backGroundConversation[ConversationNumber]);
            Destroy(other.gameObject);

            if(backGroundConversation[ConversationNumber].tag == "Therapist")
            {
                ChangeEnvironment();
                atTherapist = true;
                umbrella.SetActive(true);
                umbrella.transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            }

            else if(backGroundConversation[ConversationNumber].tag == "Final")
            {
                FinalSettings();    
            }
        }
    }

    void ChangeEnvironment()  // this is to change the color of the dialogue text and panel when player talks to therapist
    {
        //Debug.Log("Change Color");
        ConversationManager.Instance.DialogueText.color = DialogueTextColor;
        ConversationManager.Instance.DialogueBackground.color = DialogueBoxColor;
        rainControl.RainScript.RainIntensity -=  rainIntensityreduce; // Control rain after meeting therapist
        water.WaveSpeed -= waveIntensityreduce;
        StartCoroutine(BackgroundLight());
        StartCoroutine(CharacterLight());
        //BackgroundLighter();
        backgroundChangingSpeed += 0.1f;
    }

    void FinalSettings()
    {
        //Debug.Log("Final Settings");
        ConversationManager.Instance.DialogueText.color = DialogueTextColor;
        ConversationManager.Instance.DialogueBackground.color = DialogueBoxColor;
        rainControl.RainScript.RainIntensity =  0; // Control rain after meeting therapist
        StartCoroutine(BackgroundLight());
        StartCoroutine(CharacterLight());
        //BackgroundLighter();
        backgroundChangingSpeed += 0.1f;
        playerScript.enabled = false;
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isSadWalking", false);
        Music.GetComponent<AudioSource>().enabled = true;
        StartCoroutine(WaitThenRestart());
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


    IEnumerator WaitThenRestart()
    {
        while(ConversationManager.Instance.IsConversationActive)
        {
            yield return new WaitForSeconds(2f);
        }
        RestartButton.SetActive(true);
        playerAnimator.SetBool("Final",true);
        StartCoroutine(Birds());
    }

    IEnumerator Birds()
    {
        for (float i = birds.transform.position.x; i >= 224f; i -= 0.1f)
        {
            birds.transform.position = new Vector3(i, birds.transform.position.y, birds.transform.position.z);
            yield return new WaitForSeconds(0.2f);
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

    IEnumerator CharacterLight()
    {
        //Debug.Log(backgroundImage.color);
        //Debug.Log(backgroundImage.color.r + lightingBackgroundvalue);

        for(float i = characterImage.color.r;
        i <= backgroundChangingSpeed;
        i += 0.01f)
        {
            //Debug.Log(i);

            Color c = characterImage.color;
            c.r = i;
            c.g = i;
            c.b = i;

            characterImage.color = c;

            //Debug.Log(c);

            yield return new WaitForSeconds(0.02f);
        }
    }
}
