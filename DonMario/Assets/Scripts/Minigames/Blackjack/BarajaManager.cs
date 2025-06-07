using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Palo
{
    Corazones,
    Diamantes,
    Treboles,
    Picas
}

[System.Serializable]
public class Carta
{
    public int valor;
    public string nombre;
    public Palo palo;

    public string NombreCompleto => $"{nombre} de {palo}";
    public string ObtenerSimboloPalo()
    {
        return palo switch
        {
            Palo.Corazones => "♥",
            Palo.Diamantes => "♦",
            Palo.Treboles => "♣",
            Palo.Picas => "♠",
            _ => "?"
        };
    }
}

public class BarajaManager : MonoBehaviour
{
    public static BarajaManager Instance { get; private set; }

    public List<Carta> baraja = new List<Carta>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GenerarBaraja();
    }

    private void GenerarBaraja()
    {
        baraja.Clear();

        string[] nombres = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "As" };
        int[] valores = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

        foreach (Palo palo in System.Enum.GetValues(typeof(Palo)))
        {
            for (int i = 0; i < nombres.Length; i++)
            {
                Carta carta = new Carta
                {
                    nombre = nombres[i],
                    valor = valores[i],
                    palo = palo
                };
                baraja.Add(carta);
            }
        }

        Debug.Log("Baraja generada con éxito. Total de cartas: " + baraja.Count);
    }
    public List<Carta> RobarCartasQueSumenEntre(int minimo, int maximo)
    {
        List<Carta> copia = new List<Carta>(baraja);
        List<Carta> mejorMano = new List<Carta>();
        int mejorTotal = 0;

        for (int intento = 0; intento < 200; intento++)
        {
            List<Carta> mano = new List<Carta>();
            List<Carta> copiaTemporal = new List<Carta>(copia);
            int total = 0;

            while (total < minimo && copiaTemporal.Count > 0)
            {
                int index = Random.Range(0, copiaTemporal.Count);
                Carta carta = copiaTemporal[index];
                total += carta.valor;
                mano.Add(carta);
                copiaTemporal.RemoveAt(index);

                if (total > maximo)
                    break;
            }

            if (total >= minimo && total <= maximo)
            {
                mejorMano = mano;
                mejorTotal = total;
                break;
            }
        }

        foreach (Carta carta in mejorMano)
        {
            baraja.Remove(carta);
        }

        Debug.Log($"Mejor Mano total: {mejorTotal}");

        return mejorMano;
    }
    public Carta RobarCartaAleatoria()
    {
        if (baraja.Count == 0) return null;

        int index = Random.Range(0, baraja.Count);
        Carta carta = baraja[index];
        baraja.RemoveAt(index);
        return carta;
    }
}
