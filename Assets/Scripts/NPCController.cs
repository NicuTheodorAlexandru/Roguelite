using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Text text;
    private NPCText npcText;
    public GameObject hintImage;
    public string[] textLines;
    private int textProgress;
    private bool isFocused;
    // Start is called before the first frame update
    void Start()
    {
        isFocused = false;
        textProgress = 0;
        //text.text = textLines[0];
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        npcText = new NPCText(text, textLines, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.paused)
            return;
        if (!isFocused)
            return;
        if(Input.GetKeyDown(KeyCode.E) && hintImage.activeSelf)
        {
            hintImage.SetActive(false);
            text.gameObject.SetActive(true);
            npcText.LoadNext();
        }
        else if(Input.GetKeyDown(KeyCode.E) && text.gameObject.activeSelf)
        {
            if(textProgress < textLines.Length - 1)
            {
                //textProgress++;
                //text.text = textLines[textProgress];
                npcText.LoadNext();
            }
        }
        npcText.Update();
    }

    private void ResetText()
    {
        //text.text = textLines[0];
        npcText.Reset();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isFocused = true;
        hintImage.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isFocused = false;
        hintImage.SetActive(false);
        ResetText();
        text.gameObject.SetActive(false);
        textProgress = 0;
    }
}
