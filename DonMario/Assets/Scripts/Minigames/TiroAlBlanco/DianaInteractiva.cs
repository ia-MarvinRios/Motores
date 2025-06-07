using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DianaInteractiva : MonoBehaviour
{
    // Evento estático para notificar que se sumó un punto
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
