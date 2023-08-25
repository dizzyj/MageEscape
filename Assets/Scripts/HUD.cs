using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject redIcon, blueIcon, greenIcon, yellowIcon, HealthBar, youDied, pressE, pauseMenu, E;
    private void Start()
    {
        redIcon.SetActive(false);
        blueIcon.SetActive(false);
        greenIcon.SetActive(false);
        yellowIcon.SetActive(false);
        youDied.SetActive(false);
        pressE.SetActive(false);
        E.SetActive(false);
    }
    public void SetKey(Keys k)
    {
        switch (k)
        {
            case Keys.Red:
                redIcon.SetActive(true);
                break;
            case Keys.Blue:
                blueIcon.SetActive(true);
                break;
            case Keys.Yellow:
                yellowIcon.SetActive(true);
                break;
            case Keys.Green:
                greenIcon.SetActive(true);
                break;
        }
    }
    public void UnsetKey(Keys k)
    {
        switch (k)
        {
            case Keys.Red:
                redIcon.SetActive(false);
                break;
            case Keys.Blue:
                blueIcon.SetActive(false);
                break;
            case Keys.Yellow:
                yellowIcon.SetActive(false);
                break;
            case Keys.Green:
                greenIcon.SetActive(false);
                break;
        }
    }
    public void UnsetAllKeys()
    {
        redIcon.SetActive(false);
        blueIcon.SetActive(false);
        yellowIcon.SetActive(false);
        greenIcon.SetActive(false);
    }
    public void UpdateHealthBar(float input)
    {
        print("Healthbar input = " + input);
        HealthBar.GetComponent<Slider>().value = input;
    }
    public void UIDeath()
    {
        youDied.SetActive(true);
        pressE.SetActive(true);
        pauseMenu.GetComponent<PauseMenu>().PauseLevelMusic();
        pauseMenu.GetComponent<PauseMenu>().PlayDeathMusic();
    }
    public void UIRespawn()
    {
        youDied.SetActive(false);
        pressE.SetActive(false);
        pauseMenu.GetComponent<PauseMenu>().RestartLevelMusic();
        pauseMenu.GetComponent<PauseMenu>().StopDeathMusic();
    }
    public void DisplayE()
    {
        E.SetActive(true);
    }
    public void HideE()
    {
        E.SetActive(false);
    }
}
