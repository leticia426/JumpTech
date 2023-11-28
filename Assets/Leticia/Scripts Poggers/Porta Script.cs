using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    [SerializeField] private string cenaDestino; // Nome da cena para a qual a porta levar�
    [SerializeField] private int moedasNecessarias;
    [SerializeField] private GameObject teste;
    [SerializeField] private int NumerodaFase;
    private Animator portaAnimator;// Quantidade necess�ria de moedas para ativar a porta

    [SerializeField] private PersonagemController personagemController; // Refer�ncia ao script PersonagemController
    private ProgressManager progressManager;


    private void Awake()
    {
        portaAnimator = GetComponent<Animator>();
        progressManager = ProgressManager.Instance;
    }

    private void FixedUpdate()
    {
        // Verifique se o jogador coletou todas as moedas
        if (personagemController != null && personagemController.moedas >= moedasNecessarias)
        {
            portaAnimator.SetTrigger("AbrirPorta");
            teste.SetActive(true);
        }

        
    }

    // M�todo para acionar a anima��o de abertura da porta
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Verifique se o jogador tem moedas suficientes
            if (personagemController != null && personagemController.moedas >= moedasNecessarias)
            {
                // Se o jogador tem moedas suficientes, carregue a cena de destino
                SceneManager.LoadScene(cenaDestino);
                progressManager.completedFases[NumerodaFase] = true;
            }
            else
            {
                // Caso contr�rio, voc� pode exibir uma mensagem ao jogador ou fazer algo mais, como piscar a porta, indicando que n�o pode ser aberta.
            }
        }
    }
}