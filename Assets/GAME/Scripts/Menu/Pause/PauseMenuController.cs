using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button pauseButton;
    public Button resumeButton;
    public Button homeButton;

    bool isPaused = false;
    private void Awake()
    {
        pauseMenu.SetActive(false);
        
        pauseButton.onClick.AddListener(
            () =>
            {
                SetPause(true);
            });
        
        resumeButton.onClick.AddListener(
            () =>
            {
                SetPause(false);
            });
        
        homeButton.onClick.AddListener(
            () =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
            );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!isPaused);
        }
    }

    public void SetPause(bool m)
    {
        if (m)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        
        isPaused = m;
    }
}
