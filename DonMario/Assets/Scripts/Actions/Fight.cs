
using UnityEngine;

public class Fight 
{

    public int minigame = 2;

    public void Damage(EnemyAttackType aType)
    {

        string message = "Casi Te matan, pero lograste escapar con la cola entre las patas, perdiste un poco de vida";
        PlayerManager.Instance.TakeDamage(aType);
        TypewriterTextUI.Instance.ShowMessage(message);
    }
    string MiniGameName(int numero) => numero switch
    {
        0 => "Blackjack",
        1 => "Cesta",
        2 => "Lluvia",
        3 => "Pescar",
        4 => "PiedraPapelTijeras",
        5 => "TiroAlBlanco",
        _ => "ScenaNoEncontrada" // "_" es el default
    };

    public void ChooseMinigame()
    {
        minigame = Random.Range(0, 6);
        SceneLoader.Instance.LoadSceneAdditive(MiniGameName(minigame));
    }
    public void CloseMinigame()
    {
        SceneLoader.Instance.UnloadScene(MiniGameName(minigame));

        var messagesPool = PlayerManager.Instance.stats.messages;

        string message = messagesPool[Random.Range(0, messagesPool.Length)];
       
        TypewriterTextUI.Instance.ShowMessage(message);
    }

}
