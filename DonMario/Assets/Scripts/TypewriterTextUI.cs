using UnityEngine;

using UnityEngine.Events; // Necesario para UnityEvent
using System.Collections.Generic;

using TMPro;
using System.Collections;
using System;

public class TypewriterTextUI : MonoBehaviour
{
    public static TypewriterTextUI Instance;
    public GameObject TextPanel;
    public TextMeshProUGUI textDisplay;          // Objeto UI Text donde se mostrará el mensaje
    public float charactersPerSecond = 20f; // Velocidad de escritura (caracteres por segundo)
    public event Action OnTextFinished; // Evento que se dispara al terminar de escribir
   

    private string currentMessage;
    private float timer;
    private int currentCharIndex;
    private bool isTyping;

    private Queue<string> messageQueue = new Queue<string>(); // Cola de mensajes pendientes
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        TextPanel.SetActive(false);
    }

    void Update()
    {
        if (isTyping)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += 1f / charactersPerSecond;
                currentCharIndex++;
                textDisplay.text = currentMessage.Substring(0, currentCharIndex);

                if (currentCharIndex >= currentMessage.Length)
                {
                    isTyping = false;
                   

                    // Pasa al siguiente mensaje en cola (si existe)
                    if (messageQueue.Count > 0)
                    {
                        StartTyping(messageQueue.Dequeue());
                        return;
                    }
                    OnTextFinished?.Invoke(); // ¡Dispara el evento!
                    StartCoroutine(ClosePanel());
                }
            }
        }
    }

    public void ShowMessage(string message)
    {
        if (isTyping)
        {
            messageQueue.Enqueue(message);
        }
        else
        {
            StartTyping(message);
        }
    }

    private void StartTyping(string message)
    {
        TextPanel.SetActive(true);
        currentMessage = message;
        currentCharIndex = 0;
        timer = 1f / charactersPerSecond;
        isTyping = true;
        textDisplay.text = "";
    }

   
    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(2);
        textDisplay.text = "";
        TextPanel.SetActive(false);
    }

    public void SkipTyping()
    {
        if (isTyping)
        {
            textDisplay.text = currentMessage;
            isTyping = false;
            currentCharIndex = currentMessage.Length;
            OnTextFinished?.Invoke(); // También dispara el evento si se salta
        }
    }
}