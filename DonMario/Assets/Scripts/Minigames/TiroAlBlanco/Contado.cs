using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Contado : MonoBehaviour
{
    public EnemyAttackType EnemyAttackType = EnemyAttackType.Light;
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoTiempo;

    private int puntos = 0;
    private float tiempo = 30f;
    private bool juegoFinalizado = false;

    void OnEnable()
    {
        DianaInteractiva.OnDianaTocada += SumarPunto;
    }

    void OnDisable()
    {
        DianaInteractiva.OnDianaTocada -= SumarPunto;
    }

    void Update()
    {
        if (juegoFinalizado) return;

        tiempo -= Time.deltaTime;
        textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempo);

        if (tiempo <= 0)
        {
            MiniGamesManager.Instance.Invoke_LoseMiniGame(EnemyAttackType);
            juegoFinalizado = true;
            Debug.Log("¡Perdiste!");
            
        }
    }

    public void SumarPunto()
    {
        if (juegoFinalizado) return;

        puntos++;
        textoPuntos.text = "Puntos: " + puntos;

        if (puntos >= 10)
        {
            juegoFinalizado = true;
            Debug.Log("¡Ganaste!");
            MiniGamesManager.Instance.Invoke_WinMiniGame();
        }
    }
}
