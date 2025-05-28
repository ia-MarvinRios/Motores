using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerClass", menuName = "Player/Class")]
public class PlayerClassSO : ScriptableObject
{
    public PlayerClasses classType;
    public string playerName;
    public string characterDescripcion;

    [Header("Stats")]
    public int maxHealth;
    public int baseDamage;
    public float defense;

    [Header("Visuals")]
    public Sprite classIcon;
    public Color classColor = Color.white;


    [Header("Frases")]
    public string[] frases;
}

