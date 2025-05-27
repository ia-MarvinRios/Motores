using System.Linq;
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
    Explore exploreLogic;
    public int CurrentHealth { get; private set; }

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

    public void InitializePlayer(PlayerClassSO playerClass)
    {
        currentClass = playerClass;
        stats = new PlayerStats(playerClass);
        CurrentHealth = stats.maxHealth;
        SetHealthTxt();

        Debug.Log($"Jugador inicializado como {currentClass.playerName}");
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
        healthTxt.text = $"Vida: {CurrentHealth}";
        SetHealthTxt();
    }

    public void ModifyHealth(int points)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + points, 0, stats.maxHealth);
    }

  
    public int GetHelth() => CurrentHealth;


}

[System.Serializable]
public class PlayerStats
{
    public int maxHealth;
    public float movementSpeed;
    public int baseDamage;
    public float attackSpeed;
    public float defense;
    public string className;
    public string classDescription;
    public Sprite classIcon;
    public Color classColor;

    public PlayerStats(PlayerClassSO playerClass)
    {
        maxHealth = playerClass.maxHealth;
        baseDamage = playerClass.baseDamage;
        defense = playerClass.defense;
        className = playerClass.playerName;
        classDescription = playerClass.classDescription;
        classIcon = playerClass.classIcon;
        classColor = playerClass.classColor;
    }
}

public enum PlayerClasses
{
    GuerreroPesado,
    GuerreroLigero,
    Picaro,
    Sanadora
}