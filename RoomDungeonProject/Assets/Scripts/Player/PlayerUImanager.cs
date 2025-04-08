using UnityEngine;

public class PlayerUImanager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;



    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ReGame();
            }
            else
            {
                Pause();
            }
        }

    }

    void Pause()
    {
        SoundManager.instance.PlaySFX(SFXType.PauseSound);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ReGame()
    {
        SoundManager.instance.PlaySFX(SFXType.PauseSound);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

}
