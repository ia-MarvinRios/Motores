
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Pescado")]
    public bool goingRight;
    public float minSpeed = 5;
    public float maxSpeed = 10;
    private float speed;
    private float progress;

    public Transform minPos;
    public Transform maxPos;


    //public Transform fish;

    private void Start()
    {
        speed = minSpeed;
        Invoke("ChangeSpeed", 3);
    }
    private void Update()
    {
        MoveFish();
    }
    void MoveFish()
    {       

        // Mover el objeto según la dirección
        if (goingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, maxPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, minPos.position, speed * Time.deltaTime);
        }

        // Detener el movimiento cuando se llega al destino
        if (transform.position.x <= minPos.position.x)
        {
            goingRight = true;
        }
        if (transform.position.x >= maxPos.position.x)
        {
            goingRight = false;
        }
    }

    public void MoveRight()
    {
        if (maxPos == null) return;

        goingRight = true;
        progress = 0f;
    }

    // Evento para mover de target a origen
    public void MoveToLeft()
    {
        if (minPos == null) return;

        goingRight = false;
        progress = 0f;
    }

    public void ChangeSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed);


        int t = Random.Range(1, 3);
        Invoke("ChangeSpeed",t );
    }

}
