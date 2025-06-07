using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cursor : MonoBehaviour
{
    void Start()
    {
        // Oculta el cursor del sistema
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        // Convierte la posición del mouse en pantalla a posición del mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Mantiene el objeto en el plano 2D
        transform.position = mousePosition;
    }
}
