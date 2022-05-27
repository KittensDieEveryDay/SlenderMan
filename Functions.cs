using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Functions : MonoBehaviour
{
    GameManager gameManager;
    public GameObject aboutPanel;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SkipButton()
    {
        gameManager.SkipButton();
    }

    public void StartGame()
    {
        gameManager.StartGame();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("NewMainMenu");
    }
    public void LeaveOrRetry()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadMainMenu();
        }
    }

    public void FadeToStart()
    {
        gameManager.FadeToStart();
    }

    public void FadeToQuit()
    {
        gameManager.FadeToQuit();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenAboutPanel()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        GameObject.Find("AboutPanel").gameObject.SetActive(false);
    }

    public void EndOpenSlide()
    {
        gameObject.SetActive(false);
    }

    public void Tester()
    {
        print("TEST");
    }
}
