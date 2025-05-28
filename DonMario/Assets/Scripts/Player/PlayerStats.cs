using UnityEngine;

[System.Serializable]
public class PlayerStats
{

    public int maxHealth;
    public float defense; // Multiplicador de daño (0.5f = reduce daño un 50%).
    public string className;
    public string characterDescripcion;
    public Sprite classIcon;
    public Color classColor;


    public PlayerStats(PlayerClassSO playerClass)
    {
        maxHealth = playerClass.maxHealth;
        defense = playerClass.defense;
        className = playerClass.playerName;
        characterDescripcion = playerClass.characterDescripcion;
        classIcon = playerClass.classIcon;
        classColor = playerClass.classColor;
    }


    public void TakeDamage(ref int currentHealth, EnemyAttackType attackType)
    {
        int damage = 0;

     
        switch (attackType)
        {
            case EnemyAttackType.Light:
                damage = Random.Range(5, 10);
                break;
            case EnemyAttackType.Medium:
                damage = Random.Range(10, 15);
                break;
            case EnemyAttackType.Heavy:
                damage = Random.Range(15, 25);
                break;
        }

        
        damage = Mathf.RoundToInt(damage * defense);

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // Evita vida negativa.

        //Debug.Log($"{className} pierde el minijuego. Recibe {damage} de daño ({attackType}). Vida: {currentHealth}/{maxHealth}");
    }
    public void TakeDamage(ref int currentHealth, int damage)
    {
     

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); 

        //Debug.Log($"{className} pierde el minijuego. Recibe {damage} de daño ({attackType}). Vida: {currentHealth}/{maxHealth}");
    }


    public void Heal(ref int currentHealth, int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // No supera el máximo.
        //Debug.Log($"{className} se cura {healAmount} de vida. Vida actual: {currentHealth}/{maxHealth}");
    }
}

