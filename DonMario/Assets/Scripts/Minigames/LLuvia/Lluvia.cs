using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lluvia : MonoBehaviour
{
    [SerializeField] private float espera = 0.1f;
    public GameObject Gotas;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Caida", espera, espera);
    }

    void Caida()
    {
        Instantiate(Gotas, new Vector3(Random.Range(-10, 10), 10, 0), Quaternion.identity);
    }

}
