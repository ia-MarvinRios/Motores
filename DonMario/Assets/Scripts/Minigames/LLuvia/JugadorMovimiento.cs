using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour
{
    public Camera meinCamera;
    //float horizontalInput;
    float Velocidad = 10f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = meinCamera.ScreenToWorldPoint(Input.mousePosition);

        // Calcular la dirección hacia la posición X del mouse
        float direction = Mathf.Clamp(mousePosition.x - transform.position.x, -1f, 1f);

        // Mover el objeto hacia la posición del mouse
        rb.velocity = new Vector2(direction * Velocidad, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Acido"))
        {
            Destroy(this.gameObject);
        }
    }
}
