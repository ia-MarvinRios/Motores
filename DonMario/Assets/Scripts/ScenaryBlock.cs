using UnityEngine;
using System.Collections;

public class ScenaryBlock : MonoBehaviour
{
    public bool isPlayed = false;
    public bool isBlackBlock = false;

    public Transform scenaryParent;
    public Transform[] spawnPoints; // Puntos de spawn
    public float minSpawnDelay = 0.1f; // Tiempo mínimo de espera
    public float maxSpawnDelay = 0.3f; // Tiempo máximo de espera

    public GameObject[] roomsPrefabsT; // Habitaciones hacia arriba
    public GameObject[] roomsPrefabsD; // Habitaciones hacia abajo
    public GameObject[] roomsPrefabsL; // Habitaciones hacia la izquierda
    public GameObject[] roomsPrefabsR; // Habitaciones hacia la derecha

    public GameObject blockDoorPrefab;

    [Space(20)]
    public Transform upRoom;
    public Transform downRoom;
    public Transform leftRoom;
    public Transform rightRoom;

    private Coroutine myCoroutine;
   
    private void Start()
    {
       // isBlackBlock = IsBlackBlock();

        if(!isBlackBlock) myCoroutine = StartCoroutine(SpawnRoomsCoroutine());

    }
    private void OnDestroy()
    {
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
       
    }
    private IEnumerator SpawnRoomsCoroutine()
    {
        scenaryParent = GameObject.Find("ScenaryParent").transform;

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Retraso aleatorio entre spawns
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            // Determina qué arreglo usar en función del nombre del punto de spawn
            GameObject[] selectedRooms = GetRoomPrefabsForSpawnPoint(spawnPoint.name);

            if (selectedRooms != null && selectedRooms.Length > 0 && Manager.Instance.ActualRoomLessThanMax())
            {
                // Verifica si la posición está ocupada antes de instanciar
                if (!IsPositionOccupied(spawnPoint.position))
                {
                    // Instancia un prefab aleatorio
                    GameObject newRoom = Instantiate(
                        selectedRooms[Random.Range(0, selectedRooms.Length)],
                        spawnPoint.position,
                        Quaternion.identity,
                        scenaryParent
                    );

                    AsignateRoomTransform(spawnPoint.name, newRoom);

                    Manager.Instance.AddRoomCount();
                }
            }
        }
        SetBlockDoors();
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
    private void AsignateRoomTransform(string spawnPointName, GameObject g)
    {
        ScenaryBlock s = g.GetComponent<ScenaryBlock>();
        switch (spawnPointName.ToLower())
        {
            case "top":
                if(s.isBlackBlock) break;
                s.downRoom = this.transform;
                upRoom = g.transform;
                break;
                
            case "down":
                if (s.isBlackBlock) break;
                s.upRoom = this.transform;
                downRoom = g.transform;
                break;

            case "left":
                if (s.isBlackBlock) break;
                s.rightRoom = this.transform;
                leftRoom = g.transform;
                break;

            case "right":
                if (s.isBlackBlock) break;
                s.leftRoom = this.transform;
                rightRoom = g.transform;
                break;

            default:
                break;
        }
    }

 
    private bool IsPositionOccupied(Vector2 position)
    {
        // Verifica si hay algún collider en la posición dada
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f); // Ajusta el radio según el tamaño de tus habitaciones

        foreach (Collider2D collider in colliders)
        {
            // Ignora el collider del propio objeto
            if (collider != null && collider.gameObject != gameObject)
            {
                return true; // Hay otro objeto en la posición
            }
        }

        return false; // No hay otro objeto en la posición
    }

    private void SetBlockDoors()
    {

        if (upRoom == null) InstanciateDoor(CalculateDoorPosition());
        if (downRoom == null) InstanciateDoor(CalculateDoorPosition(down: true));
        if (leftRoom == null) InstanciateDoor(CalculateDoorPosition(left: true, horizontal: true));
        if (rightRoom == null) InstanciateDoor(CalculateDoorPosition(horizontal: true));
    }
    Vector3 CalculateDoorPosition(bool left = false, bool down = false,
                                  bool horizontal = false)
    {

        int leftDir = left ? -1 : 1;
        int downDir = down ? -1 : 1;

        // tamaño = tamañoSprite / pixelPerUnits
        //1920/100 19.2
        float xPos = (19.2f / 2) * leftDir;
        float yPos = (10.8f / 2) * downDir;

        if (horizontal) 
        {
            return new Vector3(xPos, 0, 0);
        }
       else
        { 
             return new Vector3(0, yPos, 0);
        }

    }
    void InstanciateDoor(Vector3 pos)
    {
        GameObject door = Instantiate(blockDoorPrefab);
        door.transform.parent = this.transform;
        door.transform.localPosition = pos;
    }
    private bool IsBlackBlock()
    {
        if (roomsPrefabsT == null || roomsPrefabsT.Length == 0 ) return false;
        if (roomsPrefabsD == null || roomsPrefabsD.Length == 0 ) return false;
        if (roomsPrefabsL == null || roomsPrefabsL.Length == 0 ) return false;
        if (roomsPrefabsR == null || roomsPrefabsR.Length == 0 ) return false;

        return true;
    }

 
}