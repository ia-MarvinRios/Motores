using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DianaInteractiva : MonoBehaviour
{
    // Evento est�tico para notificar que se sum� un punto
    public static System.Action OnDianaTocada;

    private bool fueClickeada = false;

    void OnMouseDown()
    {
        if (fueClickeada) return;

        fueClickeada = true;

        OnDianaTocada?.Invoke(); // Lanza el evento
        Destroy(gameObject);     // Destruye la diana
    }
}
