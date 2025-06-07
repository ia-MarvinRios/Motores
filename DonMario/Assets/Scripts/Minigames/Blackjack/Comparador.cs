using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparador : MonoBehaviour
{
    public EnemyAttackType EnemyAttackType = EnemyAttackType.Heavy;
    [SerializeField] private BlackjackJugador jugador;
    [SerializeField] private BlackjackDealer dealer;

    public void CompararResultados()
    {
        int totalJugador = jugador.Total;
        int totalDealer = dealer.Total;


        if (totalJugador > 21)
        {
            Debug.Log("Perdiste: tu total excede 21.");
            MiniGamesManager.Instance.Invoke_LoseMiniGame(EnemyAttackType);
        }
        else if (totalDealer > 21)
        {
            Debug.Log("¡Ganaste! El dealer se pasó de 21.");
            MiniGamesManager.Instance.Invoke_WinMiniGame();
        }
        else if (totalJugador > totalDealer)
        {
            Debug.Log("¡Ganaste! Tu total es mayor.");
            MiniGamesManager.Instance.Invoke_WinMiniGame();
        }
        else if (totalJugador < totalDealer)
        {
            Debug.Log("Perdiste: el dealer tiene un total mayor.");
            MiniGamesManager.Instance.Invoke_LoseMiniGame(EnemyAttackType);
        }
        else
        {
            Debug.Log("Empate.");
            MiniGamesManager.Instance.Invoke_WinMiniGame();
        }
    }
}
