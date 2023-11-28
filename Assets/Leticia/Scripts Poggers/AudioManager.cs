using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    public static AudioManager instance;

    private void Start()
    {
        soundSlider.onValueChanged.AddListener(AdjustSound);

        // Defina o volume global com base no valor do ProgressManager
        SetGlobalAudioVolume(ProgressManager.Instance.audioVolume);
    }

    public void AdjustSound(float volume)
    {
        // Limite o valor do volume entre 0 e 1
        volume = Mathf.Clamp01(volume);

        // Atualize o volume global e informe o ProgressManager
        SetGlobalAudioVolume(volume);
        ProgressManager.Instance.audioVolume = volume;
        ProgressManager.Instance.SaveSettings();
    }

    public void SetGlobalAudioVolume(float volume)
    {
        // Encontra todas as fontes de áudio na cena
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Ajusta o volume de todas as fontes de áudio
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
        // Esta função pode ser usada para ajustar o brilho global em tempo real
        soundSlider.value = volume;
    }
}