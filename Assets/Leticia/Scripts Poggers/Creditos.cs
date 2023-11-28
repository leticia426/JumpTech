using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    [SerializeField] private string TelaMenu;
    [SerializeField] private float velocidade = 5.0f; // Velocidade de movimento para cima
    [SerializeField] private float alturaAlvo = 10.0f; // Altura que o objeto deve alcançar

    void Update()
    {
        // Verifica se o objeto ainda não atingiu a altura desejada
        if (transform.position.y < alturaAlvo)
        {
            // Move o objeto para cima ao longo do eixo Y
            transform.Translate(Vector3.up * velocidade * Time.deltaTime);
        }
        else
        {
            SceneManager.LoadScene(TelaMenu);
        }
    }
}