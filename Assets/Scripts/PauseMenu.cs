using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public static bool paused = false;

    public void MainMenuButtonPressed()
    {
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene("mainmenu");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        paused = true;
    }

    public void Unpause()
    {
        pauseUI.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!paused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }
}
