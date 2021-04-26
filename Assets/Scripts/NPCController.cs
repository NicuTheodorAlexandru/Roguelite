using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Text text;
    public GameObject hintImage;
    public string[] textLines;
    private int textProgress;
    private bool isFocused;
    // Start is called before the first frame update
    void Start()
    {
        isFocused = false;
        textProgress = 0;
        text.text = textLines[0];
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFocused)
            return;
        if(Input.GetKeyDown(KeyCode.E) && hintImage.activeSelf)
        {
            hintImage.SetActive(false);
            text.gameObject.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.E) && text.gameObject.activeSelf)
        {
            if(textProgress < textLines.Length - 1)
            {
                textProgress++;
                text.text = textLines[textProgress];
            }
        }
    }

    private void ResetText()
    {
        text.text = textLines[0];
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
