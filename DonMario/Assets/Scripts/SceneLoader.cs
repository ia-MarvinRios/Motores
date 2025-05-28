using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneLoader Instance;
    public int minigame = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        MiniGamesManager.Instance.OnStartMiniGame += ChooseMinigame;

        /*MiniGamesManager.Instance.OnLoseMiniGame += CloseMinigame;
        MiniGamesManager.Instance.OnWinMiniGame += CloseMinigame;*/
    }

    private void OnDisable()
    {
        MiniGamesManager.Instance.OnStartMiniGame -= ChooseMinigame;

       /* MiniGamesManager.Instance.OnLoseMiniGame -= CloseMinigame;
        MiniGamesManager.Instance.OnWinMiniGame -= CloseMinigame;*/
    }
    public void ChooseMinigame()
    {
        minigame = Random.Range(0, 6);
        LoadSceneAdditive(Resultado(minigame));
    }
    public void CloseMinigame()
    {
        UnloadScene(Resultado(minigame));
    }




    string Resultado (int numero) => numero switch
    {
        0 => "Blackjack",
        1 => "Cesta",
        2 => "Lluvia",
        3 => "Pescar",
        4 => "PiedraPapelTijeras",
        5 => "TiroAlBlanco",
        _ => "ScenaNoEncontrada" // "_" es el default
    };



    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }




    // Carga una nueva escena sobre la actual
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    // Descarga una escena específica
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}