
using System.Collections;
using TMPro;
using UnityEngine;


public class FishManager : MonoBehaviour
{
    
    public float timer = 20;
    public bool canLose = true;
    public bool canWin = true;

    public TextMeshProUGUI timerTxt;
    public GameObject winPanel;
    public GameObject losePanel;


    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false); 
    }
    private void Update()
    {

        if(!canLose || !canWin) return;

        timer -= Time.deltaTime;
        timerTxt.text = $"Derrota en {timer.ToString("F2")}";
        if (timer < 0 && canLose)
        {
            LoseMethod();
            timer = 0;
        }
    }

    private void LoseMethod()
    {
        Debug.Log("Perdi");
        canWin = false;
        canLose = false;
        StartCoroutine(Lose());
    }
    public void WinMethod()
    {
        Debug.Log("gane");
        if (!canWin) return;
        StartCoroutine(Win());
        canWin = false;
        canLose = false;


    }
    IEnumerator Win()
    {
        timerTxt.transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        winPanel.SetActive(true);

        yield return new WaitForSeconds(3);
        FindObjectOfType<MiniGamesManager>().Invoke_WinMiniGame();
        
    }

    IEnumerator Lose()
    {
        timerTxt.transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        losePanel.SetActive(true);

        yield return new WaitForSeconds(3);
        FindObjectOfType<MiniGamesManager>().Invoke_LoseMiniGame();
        
    }



}
