using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healAmount = 5;
    public int healCost = 10; // Costo in punti del punteggio

    public void HealPlayer()
    {
        if (ScoreManager.Instance.GetScore() >= healCost)
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                ScoreManager.Instance.RemoveScore(healCost); // Sottrai i punti spesi
            }
        }
        else
        {
            Debug.Log("Punteggio insufficiente per curarsi!");
            // Qui puoi aggiungere un feedback visivo o sonoro per indicare che il giocatore non ha abbastanza punti
        }
    }
}