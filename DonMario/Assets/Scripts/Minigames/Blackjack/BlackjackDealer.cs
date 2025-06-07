using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackjackDealer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoTotalDealer;

    private List<Carta> manoDealer = new List<Carta>();
    private int total = 0;
    public int Total => total; 

    private void Start()
    {
        RobarCartasHastaRango(18, 22);
        MostrarCartas();
    }

    private void RobarCartasHastaRango(int min, int max)
    {
        manoDealer = BarajaManager.Instance.RobarCartasQueSumenEntre(min, max);
        total = 0;

        foreach (var carta in manoDealer)
        {
            total += carta.valor;
        }
    }

    public void MostrarCartas()
    {
        textoTotalDealer.text = $"Total: {total}";
    }
}

