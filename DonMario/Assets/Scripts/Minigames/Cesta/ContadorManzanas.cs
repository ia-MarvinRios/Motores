using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorManzanas : MonoBehaviour
{
    [Header("Objeto que detecta colisiones con manzanas")]
    [SerializeField] private GameObject detector;

    [Header("Texto que muestra los puntos")]
    [SerializeField] private TextMeshProUGUI textoPuntaje;

    [Header("Puntaje actual")]
    private int puntos = 0;
    public void SumarPunto()
    {
        puntos++;
        textoPuntaje.text = "Puntos: " + puntos;

        if (puntos >= 20)
        {
            Debug.Log("Ganaste");
        }
    }

    private void Update()
    {
        if (detector == null)
        {
            Debug.Log("Perdiste");
            enabled = false;
        }
    }
}
