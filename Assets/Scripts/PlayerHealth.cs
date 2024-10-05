using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;   // HP maksimum
    private float currentHealth;     // HP saat ini

    void Start()
    {
        currentHealth = maxHealth;   // Set HP awal
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");
        Destroy(gameObject);  // Untuk sementara, kita hancurkan objeknya
    }

    // Tambahkan public getter method untuk mengakses currentHealth
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
