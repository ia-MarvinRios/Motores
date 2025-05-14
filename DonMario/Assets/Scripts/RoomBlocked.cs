using System;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBlocked : MonoBehaviour
{
    private void Start()
    {
        // Verifica si hay otro objeto en la misma posici�n al inicio
        CheckForOverlap();
    }

    private void CheckForOverlap()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // Ajusta el radio seg�n el tama�o de tus habitaciones

        foreach (Collider2D collider in colliders)
        {
            // Ignora el collider del propio objeto
            if (collider != null && collider.gameObject != gameObject)
            {
                // Si hay otro objeto en la posici�n, destruye este objeto
                Destroy(gameObject);
                return; // Salir del m�todo para evitar m�s comprobaciones
            }
        }
    }
}
