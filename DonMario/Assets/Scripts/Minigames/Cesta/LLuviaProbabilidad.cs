using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLuviaProbabilidad : MonoBehaviour
{
    [Header("Configuración de caída")]
    [SerializeField] private float espera = 0.1f;
    [SerializeField] private GameObject manzana;
    [SerializeField] private GameObject piedra;

    [Header("Probabilidades (0 a 1)")]
    [Range(0f, 1f)]
    [SerializeField] private float probabilidadManzana = 0.8f; // 80%
    // La probabilidad de piedra será automáticamente (1 - probabilidadManzana)

    void Start()
    {
        InvokeRepeating("Caida", espera, espera);
    }

    void Caida()
    {
        GameObject objetoACaer = (Random.value <= probabilidadManzana) ? manzana : piedra;
        Instantiate(objetoACaer, new Vector3(Random.Range(-10f, 10f), 10f, 0f), Quaternion.identity);
    }
}
