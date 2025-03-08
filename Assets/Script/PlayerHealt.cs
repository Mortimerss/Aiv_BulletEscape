using UnityEngine;
using TMPro; // Importa il namespace TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private TextMeshProUGUI healthText; // Riferimento a TextMeshProUGUI

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Qui puoi aggiungere la logica per il game over
            Debug.Log("Game Over!");
            FindObjectOfType<GameManager>().GameOver();
        }
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }
    public void DisableHealthText()
    {
        if (healthText != null)
        {
            healthText.gameObject.SetActive(false);
        }
    }
}