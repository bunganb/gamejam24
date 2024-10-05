using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public Transform throwPoint;   // Titik di mana objek dilempar
    public GameObject projectilePrefab; // Prefab objek yang akan dilempar
    public float minThrowForce = 5f;  // Kekuatan lemparan minimum
    public float maxThrowForce = 20f; // Kekuatan lemparan maksimum
    public float chargeTime = 2f;     // Waktu maksimum untuk charge kekuatan lemparan
    public float angle = 45f;         // Sudut lemparan dalam derajat

    private float currentThrowForce;  // Kekuatan lemparan yang dihitung
    private float chargeStartTime;    // Waktu saat tombol ditekan
    private bool isThrowing = false;

    void Update()
    {
        // Jika tombol Space ditekan, mulai menghitung charge
        if (Input.GetKeyDown(KeyCode.Space) && !isThrowing)
        {
            chargeStartTime = Time.time;
        }

        // Jika tombol Space masih ditekan, hitung kekuatan berdasarkan durasi penekanan
        if (Input.GetKey(KeyCode.Space) && !isThrowing)
        {
            float chargeDuration = Time.time - chargeStartTime;
            currentThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargeDuration / chargeTime);

            // Jika sudah mencapai kekuatan maksimum, lempar otomatis
            if (chargeDuration >= chargeTime)
            {
                ThrowObject();
            }
        }

        // Jika tombol Space dilepas sebelum mencapai batas maksimal, lempar dengan kekuatan saat ini
        if (Input.GetKeyUp(KeyCode.Space) && !isThrowing)
        {
            ThrowObject();
        }

        // Adjust angle (optional, if you want to control it via keys)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            angle += 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            angle -= 1f;
        }
    }

    void ThrowObject()
    {
        isThrowing = true;

        // Spawn objek yang dilempar
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        // Menghitung sudut dan kekuatan lemparan
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Mengatur arah lemparan dengan sedikit offset ke atas
        Vector2 throwDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians + 0.2f)); // Menambahkan offset pada sudut Y

        // Terapkan gaya ke objek berdasarkan sudut dan kekuatan yang dihitung
        rb.AddForce(throwDirection * currentThrowForce, ForceMode2D.Impulse);

        // Reset status lemparan
        isThrowing = false;
        currentThrowForce = minThrowForce;  // Reset kekuatan lemparan untuk lemparan berikutnya
    }
}
