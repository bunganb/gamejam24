using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab objek yang akan dilempar
    public Transform throwPoint; // Titik dari mana objek akan dilempar
    public float maxPower = 100f; // Maksimal kekuatan lempar
    private float power;
    private bool isCharging;

    // Identifikasi pemain
    public bool isPlayer1; // Tandai apakah ini Player1 atau Player2

    void Update()
    {
        // Cek apakah ini giliran pemain saat ini
        if ((isPlayer1 && TurnManager.isPlayer1Turn) || (!isPlayer1 && !TurnManager.isPlayer1Turn))
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Mulai charging
            {
                isCharging = true; // Mulai mengisi kekuatan
                power = 0f; // Reset kekuatan saat mulai charging
            }

            if (isCharging) // Selama charging
            {
                power += Time.deltaTime * maxPower; // Tambahkan kekuatan seiring waktu
                power = Mathf.Clamp(power, 0f, maxPower); // Batasi kekuatan lempar
            }

            if (isCharging && Input.GetKeyUp(KeyCode.Space)) // Saat tombol dilepaskan
            {
                isCharging = false; // Hentikan pengisian
                ThrowObject(power); // Lempar objek dengan kekuatan yang sudah diisi
                TurnManager.SwitchTurn(); // Pindah ke giliran pemain lain
            }
        }
    }

    void ThrowObject(float throwPower)
    {
        // Buat objek lemparan
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        
        // Hitung arah lemparan berdasarkan kekuatan
        rb.AddForce(throwPoint.forward * throwPower, ForceMode.Impulse);
        Debug.Log("Objek dilempar dengan kekuatan: " + throwPower); // Tampilkan di Console
    }
}
