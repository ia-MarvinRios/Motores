using UnityEngine;
using System.Collections.Generic;

public class Lluvia : MonoBehaviour
{
    [Header("Configuración Básica")]
    [SerializeField] private float espera = 0.1f;
    [SerializeField] private GameObject gotaPrefab;
    [SerializeField] private float tiempoCambioPatron = 3f;

    [Header("Límites de Generación")]
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float alturaInicial = 10f;

    [Header("Configuración de Patrones")]
    [SerializeField] private int densidadPorPatron = 5;

    private List<PatronLluvia> patrones = new List<PatronLluvia>();
    private PatronLluvia patronActual;
    private float tiempoUltimoCambio;

    // Representación de los patrones con asteriscos (gotas) y guiones (espacios)
    private readonly string[] patronesConfig = {
        "****---****",      // Patrón 1
        "**--**--**--",     // Patrón 2
        "--**--**--**--",   // Patrón 3
        "***--***--***",    // Patrón 4
        "*************--",  // Patrón 5
        "--*********"       // Patrón 6
    };

    void Start()
    {
        InicializarPatrones();
        SeleccionarPatronAleatorio();
        tiempoUltimoCambio = Time.time;
    }

    void Update()
    {
        // Cambiar patrón periódicamente
        if (Time.time - tiempoUltimoCambio >= tiempoCambioPatron)
        {
            SeleccionarPatronAleatorio();
            tiempoUltimoCambio = Time.time;
        }
    }

    void FixedUpdate()
    {
        // Generar gotas con una frecuencia controlada
        if (Time.fixedTime % espera < Time.fixedDeltaTime)
        {
            GenerarGotas();
        }
    }

    void InicializarPatrones()
    {
        foreach (string config in patronesConfig)
        {
            patrones.Add(new PatronLluvia(config, minX, maxX));
        }
    }

    void SeleccionarPatronAleatorio()
    {
        patronActual = patrones[Random.Range(0, patrones.Count)];
    }

    void GenerarGotas()
    {
        if (patronActual == null) return;

        // Generar múltiples gotas según la densidad configurada
        for (int i = 0; i < densidadPorPatron; i++)
        {
            float xPos = patronActual.ObtenerPosicionAleatoria();
            Instantiate(gotaPrefab, new Vector3(xPos, alturaInicial, 0), Quaternion.identity);
        }
    }

    // Clase interna para manejar cada patrón
    private class PatronLluvia
    {
        private List<Vector2> zonasActivas = new List<Vector2>();
        private float minX;
        private float maxX;

        public PatronLluvia(string config, float minX, float maxX)
        {
            this.minX = minX;
            this.maxX = maxX;
            ProcesarConfiguracion(config);
        }

        private void ProcesarConfiguracion(string config)
        {
            // Calcular el ancho de cada segmento
            float anchoTotal = maxX - minX;
            float anchoSegmento = anchoTotal / config.Length;

            // Procesar cada caracter en la configuración
            for (int i = 0; i < config.Length; i++)
            {
                if (config[i] == '*') // Solo agregamos zonas para asteriscos (gotas)
                {
                    float inicioX = minX + i * anchoSegmento;
                    float finX = inicioX + anchoSegmento;
                    zonasActivas.Add(new Vector2(inicioX, finX));
                }
            }
        }

        public float ObtenerPosicionAleatoria()
        {
            if (zonasActivas.Count == 0) return minX;

            // Seleccionar una zona activa aleatoria
            int indiceZona = Random.Range(0, zonasActivas.Count);
            Vector2 zona = zonasActivas[indiceZona];

            // Calcular posición aleatoria dentro de la zona
            return Random.Range(zona.x, zona.y);
        }
    }
}