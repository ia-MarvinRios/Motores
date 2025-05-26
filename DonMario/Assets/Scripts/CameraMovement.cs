using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float movementDuration = 1f; // Tiempo en segundos para completar el movimiento
    public AnimationCurve speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Curva de aceleración/desaceleración

    private Coroutine movementCoroutine;
    private Vector3 originalPosition;
    private bool isMoving = false;

    // Llama a este método para iniciar el movimiento
    public void MoveToTarget(Transform t)
    {
        target = t;
        if (isMoving) return; // Evita iniciar un nuevo movimiento si ya hay uno en curso

        originalPosition = transform.position;
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);

        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }

        movementCoroutine = StartCoroutine(MoveCameraCoroutine(originalPosition, targetPosition));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 startPos, Vector3 endPos)
    {
        isMoving = true;
        float elapsedTime = 0f;

        while (elapsedTime < movementDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / movementDuration);
            float curveValue = speedCurve.Evaluate(t); // Aplica la curva de velocidad

            transform.position = Vector3.Lerp(startPos, endPos, curveValue);
            yield return null;
        }

        // Asegurarse de llegar exactamente al punto final
        transform.position = endPos;
        isMoving = false;
    }
}