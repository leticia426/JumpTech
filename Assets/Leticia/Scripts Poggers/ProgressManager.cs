using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    // Outras variáveis de controle de progresso
    public float audioVolume = 1.0f; // Volume de áudio global
    public float brightness = 1.0f;  // Valor de brilho global
    [SerializeField] private AudioSource somToque;
    public string currentScene;

    public bool[] completedFases; // Um array para acompanhar o status de cada fase

    // Use um Singleton para acessar o ProgressManager de qualquer lugar do jogo
    public static ProgressManager Instance { get; private set; }

    public bool Teleported;

    private void Start()
    {
        completedFases[0] = true;

        // Verifique o progresso do jogo, desbloqueie fases, etc.
        // Implemente a lógica de progresso do jogo aqui
        LoadSettings();
        LoadProgress();

        // Altere o volume de áudio global na inicialização
        AudioManager.instance.SetGlobalAudioVolume(audioVolume);

        // Altere o brilho global na inicialização
        BrightnessController.instance.SetGlobalBrightness(brightness);
    }

    private void Awake()
    {
        GetCurrentSceneName();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Isso evita que o objeto seja destruído entre as cenas.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (currentScene != GetCurrentSceneName())
        {
            currentScene = "0";
        }
    }

    private string GetCurrentSceneName()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void InitializeProgress()
    {
        completedFases = new bool[8]; // Supondo que você tenha 8 fases, isso cria um array para acompanhar o progresso de cada fase.
        completedFases[0] = true;
    }

    public void TocarSomToque()
    {
        if (somToque != null)
        {
            somToque.Play();
        }
    }

    public void CompleteFase(int faseIndex)
    {
        if (faseIndex >= 0 && faseIndex < completedFases.Length)
        {
            completedFases[faseIndex] = true;
            SaveProgress();
        }
    }

    public bool IsFaseCompleted(int faseIndex)
    {
        if (faseIndex >= 0 && faseIndex < completedFases.Length)
        {
            return completedFases[faseIndex];
        }
        return false;
    }

   
    public void LoadSettings()
    {
        // Carregue as configurações de áudio e brilho aqui
        if (PlayerPrefs.HasKey("AudioVolume"))
        {
            audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        }

        if (PlayerPrefs.HasKey("Brightness"))
        {
            brightness = PlayerPrefs.GetFloat("Brightness");
        }
    }

    public void SaveSettings()
    {
        // Implemente a lógica para salvar as configurações de áudio e brilho aqui
        PlayerPrefs.SetFloat("AudioVolume", audioVolume);
        PlayerPrefs.SetFloat("Brightness", brightness);
        PlayerPrefs.Save();

        // Atualize o volume de áudio global após salvar as configurações
        AudioManager.instance.SetGlobalAudioVolume(audioVolume);

        // Atualize o brilho global após salvar as configurações
        BrightnessController.instance.SetGlobalBrightness(brightness);
    }

    public float GetBrightness()
    {
        return brightness;
    }

    public void SetBrightness(float value)
    {
        brightness = value;
    }

    public void UpdateAudioVolume(float newVolume)
    {
        audioVolume = newVolume;
    }
    public void PopulateSaveData(SaveData a_SaveData)
    {
        a_SaveData.completedFases = completedFases;
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        if (a_SaveData.completedFases != null && a_SaveData.completedFases.Length == completedFases.Length)
        {
            completedFases = a_SaveData.completedFases;
        }
    }
    public void SaveProgress()
    {
        Debug.Log("Salvando game..");
        SaveDataManager.SaveJsonData(new List<ISaveable> { new SaveableProgress(this) });
        Debug.Log("Game Salvo");
    }

    public void LoadProgress()
    {
        Debug.Log("Carregando Progresso");
        SaveDataManager.LoadJsonData(new List<ISaveable> { new SaveableProgress(this) });
        Debug.Log("Progresso Carregado");
    }


}
