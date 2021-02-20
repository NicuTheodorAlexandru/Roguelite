using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;

    public void OptionsButtonPressed()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void MainMenuButtonPressed()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGameButtonPressed()
    {
        Application.Quit();
    }

    public void StartGameButtonPressed()
    {
        SceneManager.LoadScene("game");
    }
}
