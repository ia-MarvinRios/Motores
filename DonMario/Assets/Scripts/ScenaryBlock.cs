using UnityEngine;
using System.Collections;

public class ScenaryBlock : MonoBehaviour
{
    public Transform scenaryParent;
    public Transform[] spawnPoints; // Puntos de spawn
    public float minSpawnDelay = 0.1f; // Tiempo m�nimo de espera
    public float maxSpawnDelay = 0.3f; // Tiempo m�ximo de espera

    public GameObject[] roomsPrefabsT; // Habitaciones hacia arriba
    public GameObject[] roomsPrefabsD; // Habitaciones hacia abajo
    public GameObject[] roomsPrefabsL; // Habitaciones hacia la izquierda
    public GameObject[] roomsPrefabsR; // Habitaciones hacia la derecha

    private void Start()
    {
        StartCoroutine(SpawnRoomsCoroutine());
    }

    private IEnumerator SpawnRoomsCoroutine()
    {
        scenaryParent = GameObject.Find("ScenaryParent").transform;

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Retraso aleatorio entre spawns
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            // Determina qu� arreglo usar en funci�n del nombre del punto de spawn
            GameObject[] selectedRooms = GetRoomPrefabsForSpawnPoint(spawnPoint.name);

            if (selectedRooms != null && selectedRooms.Length > 0 && Manager.Instance.ActualRoomLessThanMax())
            {
                // Verifica si la posici�n est� ocupada antes de instanciar
                if (!IsPositionOccupied(spawnPoint.position))
                {
                    // Instancia un prefab aleatorio
                    GameObject newRoom = Instantiate(
                        selectedRooms[Random.Range(0, selectedRooms.Length)],
                        spawnPoint.position,
                        Quaternion.identity,
                        scenaryParent
                    );

                    // Marca la posici�n como ocupada
                    MarkPositionAsOccupied(spawnPoint.position);

                    Manager.Instance.AddRoomCount();
                }
            }
        }
    }

    private GameObject[] GetRoomPrefabsForSpawnPoint(string spawnPointName)
    {
        switch (spawnPointName.ToLower())
        {
            case "top":
                return roomsPrefabsT;
            case "down":
                return roomsPrefabsD;
            case "left":
                return roomsPrefabsL;
            case "right":
                return roomsPrefabsR;
            default:
                return null;
        }
    }

    private bool IsPositionOccupied(Vector2 position)
    {
        // Verifica si hay alg�n collider en la posici�n dada
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f); // Ajusta el radio seg�n el tama�o de tus habitaciones

        foreach (Collider2D collider in colliders)
        {
            // Ignora el collider del propio objeto
            if (collider != null && collider.gameObject != gameObject)
            {
                return true; // Hay otro objeto en la posici�n
            }
        }

        return false; // No hay otro objeto en la posici�n
    }

    private void MarkPositionAsOccupied(Vector2 position)
    {
        // Aqu� puedes agregar l�gica adicional si necesitas marcar la posici�n como ocupada
       // Debug.Log($"Posici�n {position} marcada como ocupada.");
    }
}