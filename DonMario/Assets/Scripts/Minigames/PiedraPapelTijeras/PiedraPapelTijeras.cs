using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PiedraPapelTijeras : MonoBehaviour
{
    int round = 1;
    int wins = 0; 
    int loses = 0;

    enum HandOptions { sissord = 0, rock = 1, paper = 2 }
    enum GameResult { Win, Lose, Draw }
    private GameResult gameResult;
    HandOptions choosedHand;
    HandOptions enemyChoosedHand;
    private Coroutine duelCorrutine;

    [Header("UI Elements")]
    public Button[] handButtons;
    public TextMeshProUGUI winsText; // Texto para mostrar las victorias
    public TextMeshProUGUI losesText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText; // Texto para mostrar el Resultado del round

    [Header("Animation")]
    public PPT_MoveTowards[] handsMove;

    [Header("Sprites")]
    public Sprite hand;
    public Sprite rock;
    public Sprite sissord;
    [Space(20)]
    public SpriteRenderer enemyHandSP;
    public SpriteRenderer playerHandSP;

    private void Start()
    {
        resultPanel.SetActive(false);   
    }

    private IEnumerator Duel()
    {
        EnableButtons(false);
        EnemyLogic();
        DecideWinner();
        UpdateScore(); // Actualizamos el puntaje

        enemyHandSP.sprite = ChooseSprite(enemyChoosedHand);
        playerHandSP.sprite = ChooseSprite(choosedHand);
        // Animación de inicio
        foreach (var hand in handsMove)
        {
            hand.MoveToTarget();
        }

        yield return new WaitForSeconds(2f);
        ShowResultText(); // Mostramos Resultado

        yield return new WaitForSeconds(2f);
        ClearResultText(); // Limpiamos Resultado

        // Animación de retorno
        foreach (var hand in handsMove)
        {
            hand.MoveToOrigin();
        }

        if(loses > 1)
        {
            yield return new WaitForSeconds(1f);
            FindObjectOfType<MiniGamesManager>().Invoke_LoseMiniGame();
            yield break;
        }
        if (wins > 1)
        {
            yield return new WaitForSeconds(1f);
            FindObjectOfType<MiniGamesManager>().Invoke_WinMiniGame();
            yield break;
        }
        EnableButtons(true);
    }


    private void UpdateScore()
    {
        if (gameResult == GameResult.Win)
        {
            wins++;
            winsText.text = $"Victorias: {wins}";
        }
        if(gameResult == GameResult.Lose)
        {
            loses++;
            losesText.text = $"Derrotas: {loses}";
        }
    }

    private void ShowResultText()
    {
        resultPanel.SetActive(true);
        resultText.text = gameResult switch
        {
            GameResult.Win => "¡Ganaste!",
            GameResult.Lose => "Perdiste...",
            _ => "Empate"
        };
    }

    private void ClearResultText()
    {
        resultPanel.SetActive(false);
        resultText.text = "";
    }

    // Resto del código sin cambios...
    private void EnemyLogic()
    {
        enemyChoosedHand = (HandOptions)Random.Range(0, 3);
    }

    private void EnableButtons(bool enabled)
    {
        foreach (Button button in handButtons)
        {
            button.enabled = enabled;
        }
    }

    public void DecideWinner()
    {
        if (choosedHand == enemyChoosedHand)
        {
            gameResult = GameResult.Draw;
            return;
        }

        bool[,] winConditions = new bool[3, 3] {
            { false, false, true },   // Tijeras
            { true, false, false },  // Piedra
            { false, true, false }   // Papel
        };

        gameResult = winConditions[(int)choosedHand, (int)enemyChoosedHand]
            ? GameResult.Win
            : GameResult.Lose;
    }


    private Sprite ChooseSprite(HandOptions _hand)
    {
        if(_hand == HandOptions.paper) return hand;
        if(_hand == HandOptions.sissord) return sissord;
        return rock;
    }
    //referencias en el inspector__________________________________________
    public void Choose_Sissord()
    {
        choosedHand = HandOptions.sissord;
        duelCorrutine = StartCoroutine(Duel());

    }
    public void Choose_Rock()
    {
        choosedHand = HandOptions.rock;
        duelCorrutine = StartCoroutine(Duel());

    }
    public void Choose_Papper()
    {
        choosedHand = HandOptions.paper;
        duelCorrutine = StartCoroutine(Duel());

    }
}
