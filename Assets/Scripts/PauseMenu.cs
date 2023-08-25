using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject menuUI;
    public GameObject player;

    public static bool infoActive = false;
    public GameObject infoMenu;
    public GameObject pauseMenu;
    public GameObject menuMusicController;
    public GameObject deathMusicController;
    private GameObject levelMusicController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelMusicController = GameObject.FindGameObjectWithTag("LevelMusicController");
        ResumeLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelMusicController == null) {
            levelMusicController = GameObject.FindGameObjectWithTag("LevelMusicController");
            ResumeLevelMusic();
        }
        if (infoActive) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                ReturnToPause();
                Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        } 
    }

    void Pause() {
        if (menuMusicController != null) {
            menuMusicController.SetActive(true);
        }
        PauseLevelMusic();
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<PlayerAttack>().enabled = false;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;    
    }

    public void Resume() {
        if (menuMusicController != null) {
            menuMusicController.SetActive(false);
        }
        ResumeLevelMusic();
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        player.GetComponent<Player>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;    
    }

    public void Info() {
        infoMenu.SetActive(true);
        infoActive = true;
        pauseMenu.SetActive(false);
    }

    public void ReturnToPause() {
        infoMenu.SetActive(false);
        infoActive = false;
        pauseMenu.SetActive(true); 
    }

    public void QuitGame() {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Application.Quit();
    }

    public void ResumeLevelMusic()
    {
        if (levelMusicController != null) {
            levelMusicController.GetComponent<MusicLoopTransition>().ResumeMusic();
        }
    }

    public void PauseLevelMusic()
    {
        if (levelMusicController != null) {
            levelMusicController.GetComponent<MusicLoopTransition>().PauseMusic();
        }
    }

    public void RestartLevelMusic()
    {
        if (levelMusicController != null) {
            levelMusicController.GetComponent<MusicLoopTransition>().RestartMusic();
        }
    }

    public void PlayDeathMusic()
    {
        if (deathMusicController != null) {
            deathMusicController.SetActive(true);
        }
    }

    public void StopDeathMusic()
    {
        if (deathMusicController != null) {
            deathMusicController.SetActive(false);
        }
    }
}
