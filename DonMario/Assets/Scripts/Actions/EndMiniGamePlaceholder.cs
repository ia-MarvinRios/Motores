using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMiniGamePlaceholder : MonoBehaviour
{
   public void Regresar()
    {
        FindObjectOfType<MiniGamesManager>().EndMinigame();
    }
}
