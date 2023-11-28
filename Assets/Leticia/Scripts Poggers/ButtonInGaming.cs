using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonInGaming : MonoBehaviour
{
    [SerializeField] private Button ButtonRestart;
    [SerializeField] private Button ButtonMenu;
    [SerializeField] private string RecomecarFase;
    [SerializeField] private string VoltarAoMenu;

    private void Awake()
    {
        ButtonRestart.onClick.AddListener(OnButtonRestartClick);
        ButtonMenu.onClick.AddListener(OnButtonMenuClick);
    }

    public void AttButton()
    {
        EventSystem.current.SetSelectedGameObject(ButtonRestart.gameObject);
    }

    private void OnButtonRestartClick()
    {
        // Depois configurar a opção de áudio
        Restart();
    }

    private void OnButtonMenuClick()
    {
        // Depois configurar a opção de áudio
        Menu();
    }

    private void Restart()
    {
        SceneManager.LoadScene(RecomecarFase);
    }

    private void Menu()
    {
        SceneManager.LoadScene(VoltarAoMenu);
    }
}
