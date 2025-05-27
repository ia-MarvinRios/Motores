using System.Collections.Generic;
using UnityEngine;

public class Explore
{
    [Header("Probabilidades (0-100)")]
    [Range(0, 100)] public int healProbability = 25;       // Probabilidad de curación
    [Range(0, 100)] public int damageProbability = 25;    // Probabilidad de daño
    [Range(0, 100)] public int specialEventProbability = 5; // Probabilidad de evento especial
    [Range(0, 100)] public int nothingProbability = 45;   // Probabilidad de que no ocurra nada

    public Explore()
    {
        NormalizeProbabilities();
    }

    public void TriggerRandomEvent()
    {
        int randomValue = Random.Range(0, 100);
        int cumulativeProbability = 0;

        cumulativeProbability += healProbability;
        if (randomValue < cumulativeProbability)
        {
            Heal();
            return;
        }

        cumulativeProbability += damageProbability;
        if (randomValue < cumulativeProbability)
        {
            Damage();
            return;
        }

        cumulativeProbability += specialEventProbability;
        if (randomValue < cumulativeProbability)
        {
            SpecialEvent();
            return;
        }

        // Si llega aquí, ejecuta Nothing
        Nothing();
    }

    public void SetProbabilities(int heal, int damage, int special, int nothing)
    {
        healProbability = heal;
        damageProbability = damage;
        specialEventProbability = special;
        nothingProbability = nothing;
        NormalizeProbabilities();
    }

    private void NormalizeProbabilities()
    {
        int total = healProbability + damageProbability + specialEventProbability + nothingProbability;

        if (total != 100)
        {
            float multiplier = 100f / total;
            healProbability = Mathf.RoundToInt(healProbability * multiplier);
            damageProbability = Mathf.RoundToInt(damageProbability * multiplier);
            specialEventProbability = Mathf.RoundToInt(specialEventProbability * multiplier);
            nothingProbability = 100 - healProbability - damageProbability - specialEventProbability;
        }
    }

    // Listas de eventos (las mismas que tenías)
    private List<string> healingEvents = new List<string>()
    {
        "Encuentras una poción curativa y recuperas {0} de vida.",
        "Un misterioso sanador te bendice, restaurando {0} de salud.",
        "Descansas un momento y te sientes mejor (+{0} HP)."
    };

    private List<string> damageEvents = new List<string>()
    {
        "¡Te torciste el tobillo! Pierdes {0} de vida.",
        "Un enemigo te ataca por sorpresa y luego escapa (-{0} HP).",
        "Caes en una trampa y sufres {0} de daño."
    };

    public void Heal()
    {
        int max = Mathf.FloorToInt(PlayerManager.Instance.stats.maxHealth / 2);
        int healthChange = Random.Range(1, max + 1);
        string message = string.Format(healingEvents[Random.Range(0, healingEvents.Count)], healthChange);
        PlayerManager.Instance.ModifyHealth(healthChange);
        TypewriterTextUI.Instance.ShowMessage(message);
    }

    public void Damage()
    {
        int max = Mathf.FloorToInt(PlayerManager.Instance.stats.maxHealth / 2);
        int healthChange = Random.Range(1, max + 1);
        string message = string.Format(damageEvents[Random.Range(0, damageEvents.Count)], healthChange);
        PlayerManager.Instance.ModifyHealth(-healthChange);
        TypewriterTextUI.Instance.ShowMessage(message);
    }

    public void SpecialEvent()
    {
        if (PlayerManager.Instance.CurrentHealth < 2)
        {
            int healthChange = 10;
            string message = "¡Estás al borde de la muerte, pero un dios te concede una segunda oportunidad (+10 HP)!";
            PlayerManager.Instance.ModifyHealth(healthChange);
            TypewriterTextUI.Instance.ShowMessage(message);
        }
        else
        {
            Nothing(); // Reutilizamos Nothing() para mantener consistencia
        }
    }

    public void Nothing()
    {
        string message = "Exploras el área, pero no encuentras nada interesante...";
        TypewriterTextUI.Instance.ShowMessage(message);
    }
}