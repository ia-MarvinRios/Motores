using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPlayer : MonoBehaviour
{
    public Camera meinCamera;
    public FishManager FishManager;
    public float movementSpeed;
    public Transform minPos;
    public Transform maxPos;
    public float enemiHealth = 10;
    private bool canWin = true;
    private bool canDamage;


    private void Start()
    {
        FishManager = FindObjectOfType<FishManager>();
    }
    void Update()
    {        
        transform.localPosition += new Vector3(movementSpeed * CalculateHorizontalPosition() * Time.deltaTime, 0, 0);

        if(canDamage && canWin)
        {
            enemiHealth -= Time.deltaTime;

            if (enemiHealth < 0)
            {
                FishManager.WinMethod();
                canWin = false;
            }
        }
    }

 

    public float CalculateHorizontalPosition()
    {
        // 1. Obtener la posición del GameObject
        Vector3 objectWorldPosition = transform.position;

        // Convertir la posición del objeto a coordenadas de pantalla
     
        Vector3 objectScreenPosition = meinCamera.WorldToScreenPoint(objectWorldPosition);

        // 2. Obtener la posición del mouse
        Vector3 mouseScreenPosition = Input.mousePosition;

        // 3. Calcular la relación horizontal
        float screenWidth = Screen.width;
        float objectScreenX = objectScreenPosition.x;
        float mouseScreenX = mouseScreenPosition.x;

        // 4. Calcular la posición relativa
        float relativePosition;

        if (mouseScreenX >= objectScreenX)
        {
            if (transform.position.x > maxPos.position.x) return 0;
            // Derecha del objeto
            float rightEdgeDistance = screenWidth - objectScreenX;
            relativePosition = (mouseScreenX - objectScreenX) / rightEdgeDistance;
        }
        else
        {
            if (transform.position.x < minPos.position.x) return 0;
            // Izquierda del objeto
            float leftEdgeDistance = objectScreenX;
            relativePosition = (mouseScreenX - objectScreenX) / leftEdgeDistance;
        }

        // 5. Asegurar que el valor está entre -1 y 1
        relativePosition = Mathf.Clamp(relativePosition, -1f, 1f);

        return relativePosition;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("enemie"))
        {
            canDamage = false;
           
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("enemie"))
        {
            canDamage = true;
        }
    }




}
