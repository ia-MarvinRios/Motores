using System;

using UnityEngine;

public class MiniGamesManager : MonoBehaviour//Singleton<MiniGamesManager>
{
    public static MiniGamesManager Instance;
    public event Action OnStartMiniGame;
    public event Action OnEndMiniGame;
    public event Action OnWinMiniGame;
    public event Action OnLoseMiniGame;


    public GameObject[] objectsToHideOnMinigame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        //OnStartMiniGame += StartMiniGame;
        OnWinMiniGame += EndMinigame;
        OnLoseMiniGame += EndMinigame;
    }
    private void OnDisable()
    {
        //OnStartMiniGame -= StartMiniGame;
        OnWinMiniGame -= EndMinigame;
        OnLoseMiniGame -= EndMinigame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Invoke_StartMiniGame();
        }
    }




    public void StartMiniGame()
    {
        //ShowObjects(false);
        Invoke_StartMiniGame();
    }

    public void EndMinigame()
    {
        ShowObjects(true);
        OnEndMiniGame?.Invoke();
    }
    public void ShowObjects(bool show)
    {
        if (objectsToHideOnMinigame == null || objectsToHideOnMinigame.Length < 1) return;
        foreach(GameObject obj in objectsToHideOnMinigame)
        {
            obj.SetActive(show);
        }
    }




    // Activar Delegados ______________________________________ 
    public void Invoke_StartMiniGame()
    {
        OnStartMiniGame?.Invoke();
    }
    public void Invoke_WinMiniGame()
    {
        OnWinMiniGame?.Invoke();
    }
    public void Invoke_LoseMiniGame()
    {
        OnLoseMiniGame?.Invoke();
    }
    



}
