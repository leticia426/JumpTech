using UnityEngine;

public class Espinho : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour jogadorScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu com o espinho é o jogador
        if (collision.CompareTag("Player"))
        {
            // Causa 1 ponto de dano ao jogador usando o script do jogador referenciado
            if (jogadorScript != null)
            {
                ((PersonagemController)jogadorScript).PerderVida(1); // Certifique-se de que o script do jogador tenha a função CausarDano
            }
        }
    }
}