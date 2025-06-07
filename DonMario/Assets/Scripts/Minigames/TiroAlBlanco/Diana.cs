using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diana : MonoBehaviour
{
    [SerializeField] private GameObject dianaPrefab;
    [SerializeField] private float tiempoEntreDianas = 1.5f;
    [SerializeField] private float tiempoVisible = 1.0f;

    void Start()
    {
        StartCoroutine(GenerarDianas());
    }

    IEnumerator GenerarDianas()
    {
        while (true)
        {
            Vector3 posicion = ObtenerPosicionAleatoria();
            GameObject nuevaDiana = Instantiate(dianaPrefab, posicion, Quaternion.identity);
            nuevaDiana.transform.localScale = Vector3.one;

            yield return new WaitForSeconds(tiempoVisible);

            if (nuevaDiana != null)
                Destroy(nuevaDiana);

            yield return new WaitForSeconds(tiempoEntreDianas);
        }
    }

    Vector3 ObtenerPosicionAleatoria()
    {
        float x = Random.Range(-9f, 9f);
        float y = Random.Range(-5f, 5f);
        return new Vector3(x, y, 0f);
    }
}
