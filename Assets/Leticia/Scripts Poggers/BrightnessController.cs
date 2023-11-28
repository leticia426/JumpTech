using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Image brightnessOverlay; // Refer�ncia � imagem de sobreposi��o
    public Slider brightnessSlider; // Refer�ncia ao Slider de brilho

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
        // O par�metro 'brightness' pode variar de 0 (escuro) a 1 (normal) a 2 (claro)

        // Limite o valor de brilho para evitar valores fora do intervalo
        brightness = Mathf.Clamp(brightness, 0f, 2f);

        // Ajuste a opacidade da imagem de sobreposi��o para controlar o brilho
        brightnessOverlay.color = new Color(1f, 1f, 1f, 1f - brightness);

        // Armazene as configura��es de brilho no ProgressManager
        progressManager.SetBrightness(brightness);
    }

    public void SetGlobalBrightness(float brightness)
    {
        // Esta fun��o pode ser usada para ajustar o brilho global em tempo real
        brightnessSlider.value = brightness;
        AdjustBrightness(brightness);
    }
}