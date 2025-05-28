using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Contador : MonoBehaviour
{
    public EnemyAttackType EnemyAttackType = EnemyAttackType.Light;
    [SerializeField] private GameObject objetoRequerido;
    [SerializeField] private Font fuente;  // Para asignar Liberation Sans desde el inspector
    private Text contadorText;
    private float tiempo;
    private bool juegoTerminado;

    private void Start()
    {
        tiempo = 0f;
        juegoTerminado = false;

        // Crear el Canvas si no existe
        GameObject canvasObjeto = new GameObject("Canvas");
        Canvas canvas = canvasObjeto.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObjeto.AddComponent<CanvasScaler>();
        canvasObjeto.AddComponent<GraphicRaycaster>();

        // Crear el texto del contador
        GameObject textoObjeto = new GameObject("ContadorTexto");
        textoObjeto.transform.SetParent(canvasObjeto.transform);

        // Configurar el componente Text
        contadorText = textoObjeto.AddComponent<Text>();
        contadorText.font = fuente != null ? fuente : Resources.GetBuiltinResource<Font>("Arial.ttf");
        contadorText.fontSize = 30;
        contadorText.color = Color.white;
        contadorText.alignment = TextAnchor.UpperLeft;
        contadorText.rectTransform.sizeDelta = new Vector2(300, 100);
        contadorText.rectTransform.anchoredPosition = new Vector2(-8f, 4f);
    }

    private void Update()
    {
        if (juegoTerminado) return;

        // Verificar si el objeto ha sido destruido
        if (objetoRequerido == null)
        {
            MiniGamesManager.Instance.Invoke_LoseMiniGame(EnemyAttackType);
            Debug.Log("Juego perdido: el objeto fue destruido.");
            juegoTerminado = true;
            return;
        }

        // Actualizar el contador si el objeto aún existe
        tiempo += Time.deltaTime;
        contadorText.text = $"Tiempo: {tiempo:F1}";

        // Verificar si el contador llega a 20 segundos
        if (tiempo >= 20f)
        {
            MiniGamesManager.Instance.Invoke_WinMiniGame();
            juegoTerminado = true;
        }
    }
}
