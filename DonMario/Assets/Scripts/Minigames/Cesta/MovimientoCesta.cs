using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCesta : MonoBehaviour
{
    float horizontalInput;
    float Velocidad = 15f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * Velocidad, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Acido"))
        {
            Destroy(this.gameObject);
        }
    }
    public ContadorManzanas contador;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("manzana"))
        {
            contador.SumarPunto();
        }
    }
}
