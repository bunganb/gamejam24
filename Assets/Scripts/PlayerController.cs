using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float throwPower; // Daya lempar
    public GameObject projectilePrefab; // Prefab untuk objek yang dilempar
    public Transform throwPoint; // Titik di mana objek dilempar
    private bool isMyTurn = false; // Menandakan giliran pemain ini
    public int health = 100; // Kesehatan pemain

    void Update()
    {
        if (isMyTurn && Input.GetKeyDown(KeyCode.Space)) // Ketika pemain menekan space
        {
            float holdTime = 0f;
            while (Input.GetKey(KeyCode.Space)) // Simpan waktu menekan space
            {
                holdTime += Time.deltaTime; // Menambah waktu
            }

            ThrowProjectile(holdTime); // Lempar objek sesuai waktu yang ditekan
            isMyTurn = false; // Ganti giliran
            GameManager.Instance.EndTurn(); // Panggil fungsi untuk mengakhiri giliran
        }
    }

    void ThrowProjectile(float holdTime)
    {
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(holdTime * throwPower, 0); // Atur kecepatan lemparan
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Kurangi kesehatan
    }

    public void SetTurn(bool turn)
    {
        isMyTurn = turn; // Set giliran pemain
    }
}
