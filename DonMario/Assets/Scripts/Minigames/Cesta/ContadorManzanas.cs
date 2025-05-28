using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorManzanas : MonoBehaviour
{
    public EnemyAttackType EnemyAttackType = EnemyAttackType.Light;

    [Header("Objeto que detecta colisiones con manzanas")]
    [SerializeField] private GameObject detector;

    [Header("Texto que muestra los puntos")]
    [SerializeField] private TextMeshProUGUI textoPuntaje;

    [Header("Puntaje actual")]
    private int puntos = 0;

    public int Puntos { get => puntos; set => puntos = value; }

    public void SumarPunto()
    {
        puntos++;
        textoPuntaje.text = "Puntos: " + puntos;

        if (puntos >= 20)
        {
            MiniGamesManager.Instance.Invoke_WinMiniGame();
        }
    }

    private void Update()
    {
        if (detector == null)
        {
            MiniGamesManager.Instance.Invoke_LoseMiniGame(EnemyAttackType);
            enabled = false;
        }
    }
}
