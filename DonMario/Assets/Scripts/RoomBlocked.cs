using System;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBlocked : MonoBehaviour
{
    private void Start()
    {
        // Verifica si hay otro objeto en la misma posición al inicio
        CheckForOverlap();
    }

    private void CheckForOverlap()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // Ajusta el radio según el tamaño de tus habitaciones

        foreach (Collider2D collider in colliders)
        {
            // Ignora el collider del propio objeto
            if (collider != null && collider.gameObject != gameObject)
            {
                // Si hay otro objeto en la posición, destruye este objeto
                Destroy(gameObject);
                return; // Salir del método para evitar más comprobaciones
            }
        }
    }
}
