using System;
using UnityEngine;
using System.Collections;

public class DamageOnCollision : MonoBehaviour
{
    private bool canTakeDamage = true;
    public float damageCooldown = 1.0f; // Tempo di attesa tra un danno e l'altro

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger attivato con: " + other.gameObject.name);

        if (!other.gameObject.GetComponent<MoveBullet>()) return;

        if (canTakeDamage)
        {
            Debug.Log("Proiettile rilevato! Applicando danno.");
            FindObjectOfType<PlayerHealth>().TakeDamage(10);
            StartCoroutine(DamageCooldown());
        }
        else
        {
            Debug.Log("Danno evitato, cooldown attivo.");
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
