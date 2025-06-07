using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlackjackJugador : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoCartas;     
    [SerializeField] private TextMeshProUGUI textoTotal;      
    [SerializeField] private Button botonPedirCarta;
    [SerializeField] private Button botonPlantarse;
    [SerializeField] private Comparador comparador;
    [SerializeField] private VisualizadorCartas visualizador;

    private List<Carta> mano = new List<Carta>();
    private int total = 0;
    public int Total => total;
    private bool sePlanto = false;

    private void Start()
    {
        // Robar dos cartas iniciales
        RobarCarta();
        RobarCarta();
        ActualizarUI();

        // Asignar eventos a botones
        botonPedirCarta.onClick.AddListener(() =>
        {
            if (sePlanto) return;
            RobarCarta();
            ActualizarUI();
            VerificarSiSePasa();
        });

        botonPlantarse.onClick.AddListener(() =>
        {
            Plantarse();
        });
    }

    private void RobarCarta()
    {
        Carta carta = BarajaManager.Instance.RobarCartaAleatoria();
        if (carta != null)
        {
            mano.Add(carta);
            total += carta.valor;
        }
    }

    private void ActualizarUI()
    {
        string texto = "Tus cartas:\n";
        foreach (Carta carta in mano)
        {
            texto += $"- {carta.NombreCompleto} ({carta.valor})\n";
        }

        textoCartas.text = texto;
        textoTotal.text = $"Total: {total}";

    }

    private void Plantarse()
    {
        sePlanto = true;
        botonPedirCarta.interactable = false;
        botonPlantarse.interactable = false;

        Debug.Log("Te has plantado con un total de: " + total);

        comparador.CompararResultados();
    }

    private void VerificarSiSePasa()
    {
        if (total > 21)
        {
            botonPedirCarta.interactable = false;
            botonPlantarse.interactable = false;
        }
    }
}
