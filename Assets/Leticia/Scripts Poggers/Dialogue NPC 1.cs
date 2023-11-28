using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] speechTxt;
    public string actorName;
    private Animator animator;

    private DialogueControl dc;
    public float radious;
    public LayerMask playerLayer;
    bool onRadious;
    public bool dialogueActive = false; // Variável de controle para rastrear se o diálogo está ativo

    [Header("Texto de Instrução")]
    [SerializeField] private GameObject InstrucitonText;





    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    private void FixedUpdate()
    {
        Interact();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && onRadious && !dialogueActive)
        {
            dc.Speech(speechTxt, actorName);
            dialogueActive = true; // Marcar que o diálogo está ativo
           
        }

        if (Input.GetKeyDown(KeyCode.F) && onRadious && !dialogueActive)
        {
            dc.Speech(speechTxt, actorName);
            dialogueActive = true; // Marcar que o diálogo está ativo

        }

        if (dc.CloseDialogue == true)
        {
            dialogueActive = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Botão Fire1 pressionado e dentro da área de interação.");
        }

        if (onRadious)
        {
            InstrucitonText.SetActive(true);
        }
        else
        {
            InstrucitonText.SetActive(false);
        }
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if (hit != null)
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }

   

    // Método para fechar o diálogo e permitir que o personagem se mova novamente
    

}
