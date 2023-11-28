using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverButtonManager : MonoBehaviour
{
    [SerializeField] private Button ButtonPlay;
    [SerializeField] private Button ButtonSettings;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button ExitButton2;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button ResetProgressButton;


    [SerializeField] private Button Fase1Button;
    [SerializeField] private Button Fase2Button;
    [SerializeField] private Button Fase3Button;
    [SerializeField] private Button Fase4Button;
    [SerializeField] private Button Fase5Button;
    [SerializeField] private Button Fase6Button;
    [SerializeField] private Button Fase7Button;
    [SerializeField] private Button Fase8Button;

    [SerializeField] private string Fase1;
    [SerializeField] private string Fase2;
    [SerializeField] private string Fase3;
    [SerializeField] private string Fase4;
    [SerializeField] private string Fase5;
    [SerializeField] private string Fase6;
    [SerializeField] private string Fase7;
    [SerializeField] private string Fase8;

    [SerializeField] private GameObject ObjectToDisable;
    [SerializeField] private GameObject ObjectToEnable;

    [SerializeField] private GameObject PanelPlay;
    [SerializeField] private GameObject DisableMenu;

    [SerializeField] GameObject[] cadeados;

    [SerializeField] private AudioSource somNaoPode;


    private void Awake()
    {
        ButtonPlay.onClick.AddListener(OnButtonRestartClick);
        ButtonSettings.onClick.AddListener(OnButtonSettingsClick);
        ExitButton.onClick.AddListener(OnButtonExitClick);
        QuitButton.onClick.AddListener(OnButtonQuitClick);
        ExitButton2.onClick.AddListener(OnButtonExitButton2Click);
        ResetProgressButton.onClick.AddListener(OnButtonResetProgressClick);

        Fase1Button.onClick.AddListener(OnButtonFase1Click);
        Fase2Button.onClick.AddListener(OnButtonFase2Click);
        Fase3Button.onClick.AddListener(OnButtonFase3Click);
        Fase4Button.onClick.AddListener(OnButtonFase4Click);
        Fase5Button.onClick.AddListener(OnButtonFase5Click);
        Fase6Button.onClick.AddListener(OnButtonFase6Click);
        Fase7Button.onClick.AddListener(OnButtonFase7Click);
        Fase8Button.onClick.AddListener(OnButtonFase8Click);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(ButtonPlay.gameObject);
    }

    private void Update()
    {
        ProgressManager progressManager = ProgressManager.Instance;

        if (progressManager.completedFases[1] == true)
        {
            cadeados[0].SetActive(false);
        }
        else
        {
            // A fase não está completa, ative o cadeado correspondente
            cadeados[0].SetActive(true);
        }
        if (progressManager.completedFases[2] == true)
        {
            cadeados[1].SetActive(false);
        }
        else
        {
            // A fase não está completa, ative o cadeado correspondente
            cadeados[1].SetActive(true);
        }

        if (progressManager.completedFases[3] == true)
        {
            cadeados[2].SetActive(false);
        }
        else
        {
            // A fase não está completa, ative o cadeado correspondente
            cadeados[2].SetActive(true);
        }

        if (progressManager.completedFases[4] == true)
        {
            cadeados[3].SetActive(false);
        }
        else
        {
            // A fase não está completa, ative o cadeado correspondente
            cadeados[3].SetActive(true);
        }
        if (progressManager.completedFases[5] == true)
        {
            cadeados[4].SetActive(false);
        }
        else
        {
            // A fase não está completa, ative o cadeado correspondente
            cadeados[4].SetActive(true);
        }
        if (progressManager.completedFases[6] == true)
        {
            cadeados[5].SetActive(false);
        }
        else
        {
            // A fase não está completa, ative o cadeado correspondente
            cadeados[5].SetActive(true);
        }
        if (progressManager.completedFases[7] == true)
        {
            cadeados[6].SetActive(false);
        }
        else
        {
            // A fase não está completa, ative o cadeado correspondente
            cadeados[6].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.O) && Input.GetKeyDown(KeyCode.P))
        {
            ProgressManager.Instance.completedFases[1] = true;
            ProgressManager.Instance.completedFases[2] = true;
            ProgressManager.Instance.completedFases[3] = true;
            ProgressManager.Instance.completedFases[4] = true;
            ProgressManager.Instance.completedFases[5] = true;
            ProgressManager.Instance.completedFases[6] = true;
            ProgressManager.Instance.completedFases[7] = true;
            ProgressManager.Instance.SaveProgress();
        }
    }


    void TocarSomNaoToque()
    {
        if (somNaoPode != null)
        {
            somNaoPode.Play();
        }
    }
    private void OnButtonFase1Click()
    {
        CheckAndPlayFase(0);

    }

    private void OnButtonFase2Click()
    {
        CheckAndPlayFase(1);
    }

    private void OnButtonFase3Click()
    {
        CheckAndPlayFase(2);
    }

    private void OnButtonFase4Click()
    {
        CheckAndPlayFase(3);
    }

    private void OnButtonFase5Click()
    {
        CheckAndPlayFase(4);
    }

    private void OnButtonFase6Click()
    {
        CheckAndPlayFase(5);
    }

    private void OnButtonFase7Click()
    {
        CheckAndPlayFase(6);
    }

    private void OnButtonFase8Click()
    {
        CheckAndPlayFase(7);
    }

    private void CheckAndPlayFase(int faseIndex)
    {
        ProgressManager progressManager = ProgressManager.Instance;

        if (progressManager != null && progressManager.IsFaseCompleted(faseIndex))
        {
            ProgressManager.Instance.TocarSomToque();
            FaseXPlay(faseIndex);
            ProgressManager.Instance.SaveProgress();
        }
        else
        {
            TocarSomNaoToque();
            ProgressManager.Instance.SaveProgress();
        }
    }

    private void FaseXPlay(int faseIndex)
    {
        switch (faseIndex)
        {
            case 0:
                SceneManager.LoadScene(Fase1);
                break;
            case 1:
                SceneManager.LoadScene(Fase2);
                break;
            case 2:
                SceneManager.LoadScene(Fase3);
                break;
            case 3:
                SceneManager.LoadScene(Fase4);
                break;
            case 4:
                SceneManager.LoadScene(Fase5);
                break;
            case 5:
                SceneManager.LoadScene(Fase6);
                break;
            case 6:
                SceneManager.LoadScene(Fase7);
                break;
            case 7:
                SceneManager.LoadScene(Fase8);
                break;
        }
    }




    private void OnButtonRestartClick()
    {
        // Depois configurar a opção de áudio
        Play();
        EventSystem.current.SetSelectedGameObject(Fase1Button.gameObject);
    }


    private void Play()
    {
        PanelPlay.SetActive(true);
        DisableMenu.SetActive(false);
    }

    private void OnButtonSettingsClick()
    {
        // Depois configurar a opção de áudio
        Settings();
        EventSystem.current.SetSelectedGameObject(ExitButton.gameObject);
    }

    private void Settings()
    {
        ObjectToDisable.SetActive(false);
        ObjectToEnable.SetActive(true);
    }
    private void OnButtonExitClick()
    {
        // Depois configurar a opção de áudio
        Exit();
        EventSystem.current.SetSelectedGameObject(ButtonPlay.gameObject);
    }

    private void Exit()
    {
        ObjectToDisable.SetActive(true);
        ObjectToEnable.SetActive(false);
    }

    private void OnButtonExitButton2Click()
    {
        Exit2();
        EventSystem.current.SetSelectedGameObject(ButtonSettings.gameObject);
    }

    private void Exit2()
    {
        PanelPlay.SetActive(false);
        DisableMenu.SetActive(true);
    }

    private void OnButtonQuitClick()
    {
        Quit();
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void OnButtonResetProgressClick()
    {
        ResetProgress();
    }

    private void ResetProgress()
    {
        ProgressManager progressManager = ProgressManager.Instance;

        ProgressManager.Instance.InitializeProgress();

        ProgressManager.Instance.SaveProgress();
    }



}
