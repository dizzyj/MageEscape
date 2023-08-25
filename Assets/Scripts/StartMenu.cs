using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public static bool infoActive = false;
    public GameObject infoMenu;
    public GameObject startMenu;

    void Update() {
        if (infoActive) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                returnToStart();
            }
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadInfo() {
        infoMenu.SetActive(true);
        infoActive = true;
        startMenu.SetActive(false);
    }

    public void returnToStart() {
        infoMenu.SetActive(false);
        infoActive = false;
        startMenu.SetActive(true); 
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }
}
