using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    public bool CloseDialogue = false;
    [SerializeField] private bool ActivateCoins = false; // Nova variável para controlar a ativação das moedas
    


    [Header("Components")] 
    public GameObject dialogueObj;
    public GameObject GameObjectDinheiro;
    public GameObject ContadorMonetario;
    public TextMeshProUGUI actorNameText;
    public TextMeshProUGUI speechText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;

    private void Awake()
    {
        GetCurrentSceneName();

        if (ProgressManager.Instance.currentScene == GetCurrentSceneName()) 
        {
            GameObjectDinheiro.SetActive(true);
            ContadorMonetario.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            NextSentence();
        }
    }

    public void Speech(string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
        CloseDialogue = false;
    }

    IEnumerator TypeSentence ()
    {
        foreach (char letter in sentences[index].ToCharArray()) 
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        
        }
    }

    public void NextSentence ()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
               
                
                
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive (false);
                CloseDialogue = true;
                ActivateCoins = true; // Define a variável quando o diálogo é fechado
            }
        }
    }

    private void FixedUpdate()
    {
        if (CloseDialogue && ActivateCoins && ProgressManager.Instance.currentScene != GetCurrentSceneName())
        {
            GameObjectDinheiro.SetActive(true);
            ContadorMonetario.SetActive(true);
            ProgressManager.Instance.currentScene = GetCurrentSceneName(); // Atualiza a cena atual
        }
    }

    // Função para obter o nome da cena atual
    private string GetCurrentSceneName()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}
