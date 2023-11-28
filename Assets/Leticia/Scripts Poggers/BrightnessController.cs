using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Image brightnessOverlay; // Referência à imagem de sobreposição
    public Slider brightnessSlider; // Referência ao Slider de brilho

    public static BrightnessController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        brightnessSlider.onValueChanged.AddListener(AdjustBrightness);

        SetGlobalBrightness(ProgressManager.Instance.brightness);
    }

    public void AdjustBrightness(float brightness)
    {
        ProgressManager progressManager = ProgressManager.Instance;
        // O parâmetro 'brightness' pode variar de 0 (escuro) a 1 (normal) a 2 (claro)

        // Limite o valor de brilho para evitar valores fora do intervalo
        brightness = Mathf.Clamp(brightness, 0f, 2f);

        // Ajuste a opacidade da imagem de sobreposição para controlar o brilho
        brightnessOverlay.color = new Color(1f, 1f, 1f, 1f - brightness);

        // Armazene as configurações de brilho no ProgressManager
        progressManager.SetBrightness(brightness);
    }

    public void SetGlobalBrightness(float brightness)
    {
        // Esta função pode ser usada para ajustar o brilho global em tempo real
        brightnessSlider.value = brightness;
        AdjustBrightness(brightness);
    }
}