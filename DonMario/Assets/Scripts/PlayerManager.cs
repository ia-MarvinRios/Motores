using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [Header("Classes")]
    [SerializeField] private PlayerClassSO[] availableClasses;
    [SerializeField] private PlayerClassSO currentClass;

    [Space(20)]
    [Header("UI")]
    public TextMeshProUGUI healthTxt;

    [SerializeField]public PlayerStats stats { get; private set; }
    //Explore exploreLogic;
    private int currentHealth;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = this;
        }
        if (currentClass != null)
        {
            InitializePlayer(currentClass);
        }


    }
   
    private void OnDestroy()
    {
        MiniGamesManager.Instance.OnLoseMiniGame -= TakeDamage;
    }

    public void InitializePlayer(PlayerClassSO playerClass)
    {
        currentClass = playerClass;
        stats = new PlayerStats(playerClass);
        currentHealth = stats.maxHealth;
        SetHealthTxt();

        //Debug.Log($"Jugador inicializado como {currentClass.playerName}");
    }

    // Método para cambiar de clase
    public void ChangeClass(PlayerClasses newClassType)
    {
        foreach (var playerClass in availableClasses)
        {
            if (playerClass.classType == newClassType)
            {
                InitializePlayer(playerClass);
                return;
            }
        }

        Debug.LogWarning($"Clase {newClassType} no encontrada en availableClasses");
    }

    public void SetHealthTxt()
    {
        healthTxt.text = $"Vida: {currentHealth}";
       // SetHealthTxt();
    }

    //enemigos
    public void TakeDamage(EnemyAttackType aType = EnemyAttackType.Light)
    {
        stats.TakeDamage(ref currentHealth, aType);
        SetHealthTxt();
    }
    //eventos especiales como en "explorar" 
    public void TakeDamage(int points)
    {
        stats.TakeDamage(ref currentHealth, points);
        SetHealthTxt();
    }

    public void Heal(int points)
    {
        stats.Heal(ref currentHealth, points);
        SetHealthTxt();
    }

  
    public int GetHelth() => currentHealth;


}
