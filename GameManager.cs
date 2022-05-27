using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Pause Menu")]
    public bool isPaused;
    [SerializeField] GameObject pauseMenu;

    [Header("Intro Slides")]
    public bool isIntroPlaying;
    public GameObject slides;
    public GameObject collectNotesTxt;

    [Header("Game Progress")]
    public float notesCollected;

    AudioSource ads;
    Animator anm;

    // Start is called before the first frame update
    void Start()
    {
        ads = GetComponent<AudioSource>();

        isPaused = false;
        isIntroPlaying = true;

        notesCollected = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isIntroPlaying)
        {
            SkipButton();
        }
    }

    public void SkipButton()
    {
        slides.SetActive(false);
        ads.volume = 0.3f;
        collectNotesTxt.SetActive(true);
    }

    public void StartGame()
    {
        isIntroPlaying = false;
    }

    public void FadeToStart()
    {
        anm.SetBool("Start", true);
    }

    public void FadeToQuit()
    {
        anm.SetBool("Quit", true);
    }

    public void PauseMenu()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void Tester() // checker
    {
        print("HI");
    }

}
