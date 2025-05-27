using UnityEngine;

public class PPT_MoveTowards : MonoBehaviour
{
     private Vector3 origen;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;

    private bool isMoving = false;
    private bool movingToTarget = false;
    private float progress = 0f;

    private void Start()
    {
        
        origen = transform.position;
        
        //SetTargetAndMove();
    }

    private void Update()
    {
        if(!isMoving) return;
         // Calcular el progreso del movimiento

        progress += Time.deltaTime * speed;
        progress = Mathf.Clamp01(progress);

        // Mover el objeto según la dirección
        if (movingToTarget)
        {
            transform.position = Vector3.Lerp(origen, target.position, progress);
        }
        else
        {
            transform.position = Vector3.Lerp(target.position, origen, progress);
        }

        // Detener el movimiento cuando se llega al destino
        if (progress >= 1f)
        {
            isMoving = false;
        }
        
    }

    // Evento para mover de origen a target
    public void MoveToTarget()
    {
        if (target == null) return;

        isMoving = true;
        movingToTarget = true;
        progress = 0f;
    }

    // Evento para mover de target a origen
    public void MoveToOrigin()
    {
        if (origen == null) return;

        movingToTarget = false;
        isMoving = true;
        
        progress = 0f;
    }
}