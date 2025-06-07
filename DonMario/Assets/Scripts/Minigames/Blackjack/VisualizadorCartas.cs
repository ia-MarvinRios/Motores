using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class VisualizadorCartas : MonoBehaviour
{
    [SerializeField] private GameObject cartaUIPrefab;
    [SerializeField] private RectTransform contenedorCartas; // Cambié a RectTransform

    [SerializeField] private Color colorRojo;
    [SerializeField] private Color colorNegro;

    public void MostrarCartas(List<Carta> cartas)
    {
        // Limpiar cartas anteriores
        foreach (Transform hijo in contenedorCartas)
        {
            Destroy(hijo.gameObject);
        }

        Rect rect = contenedorCartas.rect; // Área del panel

        foreach (Carta carta in cartas)
        {
            GameObject cartaGO = Instantiate(cartaUIPrefab, contenedorCartas);

            // Posicionar carta aleatoriamente dentro del rect del panel
            Vector2 pos = new Vector2(
                Random.Range(rect.xMin, rect.xMax),
                Random.Range(rect.yMin, rect.yMax)
            );

            RectTransform rt = cartaGO.GetComponent<RectTransform>();
            rt.anchoredPosition = pos;

            TextMeshProUGUI[] textos = cartaGO.GetComponentsInChildren<TextMeshProUGUI>();
            if (textos.Length < 2) continue;

            var textoValor = textos[0];
            var textoPalo = textos[1];

            textoValor.text = carta.nombre;
            textoPalo.text = carta.ObtenerSimboloPalo();

            bool esRojo = carta.palo == Palo.Corazones || carta.palo == Palo.Diamantes;
            Color color = esRojo ? colorRojo : colorNegro;

            textoValor.color = color;
            textoPalo.color = color;
        }
    }
}
