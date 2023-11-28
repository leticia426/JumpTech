using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private ButtonInGaming buttonInGaming;

    [SerializeField] private GameObject pauseMenuUI; // Referência ao GameObject da tela de pausa

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                buttonInGaming.AttButton();
            }
        }
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        // Ative o GameObject da tela de pausa
        pauseMenuUI.SetActive(true);
        isPaused = true;

    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        // Desative o GameObject da tela de pausa
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}