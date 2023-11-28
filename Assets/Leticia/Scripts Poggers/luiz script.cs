using System.Collections;
using TMPro;
using UnityEngine;

public class PersonagemController : MonoBehaviour
{
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Dialogue dialogue; // Adicione uma referência ao script de diálogo
    private DialogueControl dc;
    private AudioController audioController;
    [SerializeField] private ButtonInGaming buttonInGaming;

    [Header("Configurações de Movimento")]
    [SerializeField] private float velocidadeMovimento = 5f;
    [SerializeField] private float forcaPulo = 10f;
    [SerializeField] private float atritoNoChao = 2f;
    [SerializeField] private float coyoteTime;

    [Header("SkinWindth e vida")]
    public float skinWidth = 0.02f;
    public int vidaMaxima = 100;
    public int vidaAtual;

    private bool estaNoChao = false;
    private bool podePular = true;
    private bool podeDuploPulo = true;
    private bool estaGameOver = false;
    bool canMove = true;
    bool AtivarMetodo = true;
    private float tempoUltimoChao;
    bool gameOverEstaAtivo;



    [Header("Moedas")]
    public TextMeshProUGUI moedasText;
    public int moedas;
    [SerializeField] private AudioClip somMoeda; // Adicione uma referência ao arquivo de áudio da moeda
    private AudioSource audioSource; // Crie uma referência para o componente de áudio

    [Header("GameOver")]
    [SerializeField] private GameObject GameOverLayer;


    private enum EstadoPersonagem
    {
        Parado,
        Andando,
        Pulando,
        Caindo,
        Pulo2
    }

    private EstadoPersonagem estado = EstadoPersonagem.Parado;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        vidaAtual = vidaMaxima;
        dialogue = FindObjectOfType<Dialogue>(); // Encontre o script de diálogo
        dc = FindObjectOfType<DialogueControl>();
        audioController = FindObjectOfType<AudioController>();

        // Obtenha o componente de áudio do GameObject
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        // Verifique se o jogo está no estado de "Game Over" antes de permitir movimento ou ações do jogador
        if (!estaGameOver)
        {
            UpdateAnimacoes();

            if (AtivarMetodo == true)
            {
                HandleMovimento();
                HandlePulo();
            }
        }

        if (estaGameOver && gameOverEstaAtivo)
        {
            buttonInGaming.AttButton();
            gameOverEstaAtivo = false;
        }

        if (dc.CloseDialogue == true)
        {
            canMove = true;
        }

        if (dialogue.dialogueActive == true)
        {
            canMove = false;
        }


        // Outro código de atualização aqui...
    }

    void HandleMovimento()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        Vector2 direcaoMovimento = new Vector2(movimentoHorizontal, 0);

        rb.velocity = new Vector2(movimentoHorizontal * velocidadeMovimento, rb.velocity.y);

        if (movimentoHorizontal < 0)
        {
            spriteRenderer.flipX = true;
            estado = EstadoPersonagem.Andando;
        }
        else if (movimentoHorizontal > 0)
        {
            spriteRenderer.flipX = false;
            estado = EstadoPersonagem.Andando;
        }
        else
        {
            estado = EstadoPersonagem.Parado;
        }

        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            estaNoChao = true;
        }
        else
        {
            estaNoChao = false;
        }
    }

    void HandlePulo()
    {
        if (estaNoChao || Time.time - tempoUltimoChao <= coyoteTime)
        {
            podePular = true;
            podeDuploPulo = true;
        }
        else
        {
            podePular = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (podePular)
            {
                rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
                estado = EstadoPersonagem.Pulando;
                podePular = false; // Desativa o pulo após usá-lo
            }
            else if (podeDuploPulo)
            {
                rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
                podeDuploPulo = false;
                estado = EstadoPersonagem.Pulo2;
            }
           
        }
    }

    void UpdateAnimacoes()
    {
        animator.SetFloat("Velocidade", Mathf.Abs(rb.velocity.x));
        animator.SetBool("NoChao", estaNoChao);
        
        switch (estado)
        {
            case EstadoPersonagem.Parado:
                animator.SetBool("Parado", true);
                animator.SetBool("Andando", false);
                animator.SetBool("Pulando", false);
                animator.SetBool("Caindo", false);
                animator.SetBool("Pulo2", false);
                break;
            case EstadoPersonagem.Andando:
                animator.SetBool("Parado", false);
                animator.SetBool("Andando", true);
                animator.SetBool("Pulando", false);
                animator.SetBool("Caindo", false);
                animator.SetBool("Pulo2", false);
                break;
            case EstadoPersonagem.Pulando:
                animator.SetBool("Parado", false);
                animator.SetBool("Andando", false);
                animator.SetBool("Pulando", true);
                animator.SetBool("Caindo", false);
                animator.SetBool("Pulo2", false);
                break;
            case EstadoPersonagem.Caindo:
                animator.SetBool("Parado", false);
                animator.SetBool("Andando", false);
                animator.SetBool("Pulando", false);
                animator.SetBool("Caindo", true);
                animator.SetBool("Pulo2",false);
                break;
            case EstadoPersonagem.Pulo2:
                animator.SetBool("Parado", false);
                animator.SetBool("Andando", false);
                animator.SetBool("Pulando", false);
                animator.SetBool("Caindo",false);
                animator.SetBool("Pulo2", true);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estado = EstadoPersonagem.Parado;
            estaNoChao = true;
            tempoUltimoChao = Time.time; // Atualiza o tempo quando o personagem toca no chão
        }

    }


    public void PerderVida(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Morrer();
            
        }
    }

    void Morrer()
    {
        animator.SetTrigger("Morrer");
        // Desativa o componente Rigidbody2D para parar o movimento
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;

        // Define estaGameOver como true para indicar o estado de "Game Over"
        estaGameOver = true;
        gameOverEstaAtivo = true;

        // Ativa o objeto "GameOverLayer"
        if (GameOverLayer != null)
        {
            GameOverLayer.SetActive(true);
            // Chame a função para tocar o som de Game Over
            audioController.PlaySomGameOver();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("moeda"))
        {
            moedas++; // Incrementa o contador de moedas
            Destroy(other.gameObject); // Destroi a moeda coletada
            AtualizarTextoMoedas(); // Atualiza o texto na tela
            TocarSomMoeda(); // Toca o som da moeda
        }

    }

    void TocarSomMoeda()
    {
        if (somMoeda != null && audioSource != null)
        {
            audioSource.PlayOneShot(somMoeda);
        }
    }

    void AtualizarTextoMoedas()
    {
        moedasText.text = " " + moedas.ToString(); // Atualiza o texto na tela com o número de moedas coletadas
    }

    private void FixedUpdate()
    {
        if (canMove == false)
        {
            AtivarMetodo = false;
            rb.velocity = Vector2.zero;

        }

        else
        {
            AtivarMetodo = true;
        }
    }


}