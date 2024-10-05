using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
     public float damage = 20f;   // Damage yang diberikan saat mengenai lawan

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika objek mengenai lawan (tag harus disesuaikan)
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // Hancurkan objek setelah mengenai sesuatu
        Destroy(gameObject);
    }
}

