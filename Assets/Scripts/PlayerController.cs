using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject projectilePrefab; // Prefab objek yang akan dilempar
    public float minThrowForce = 10f; // Kekuatan lemparan minimum
    public float maxThrowForce = 50f; // Kekuatan lemparan maksimum
    public float chargeTime = 2f; // Waktu maksimum untuk charge kekuatan lemparan
    public float angle = 45f; // Sudut lemparan dalam derajat

    private float currentThrowForce; // Kekuatan lemparan yang dihitung
    private float chargeStartTime; // Waktu saat tombol ditekan
    private bool isThrowing = false;
    private bool canThrow = false; // Variabel untuk mengontrol apakah pemain boleh melempar

    void Update()
    {
        if (canThrow) // Cek apakah pemain aktif boleh melempar
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isThrowing)
            {
                chargeStartTime = Time.time;
            }

            if (Input.GetKey(KeyCode.Space) && !isThrowing)
            {
                float chargeDuration = Time.time - chargeStartTime;
                currentThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargeDuration / chargeTime);
                angle = Mathf.Lerp(30f, 60f, chargeDuration / chargeTime);

                if (chargeDuration >= chargeTime)
                {
                    ThrowObject();
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && !isThrowing)
            {
                ThrowObject();
            }
        }
    }

    public void EnableThrow()
    {
        canThrow = true; // Izinkan pemain melempar
    }

    public void DisableThrow()
    {
        canThrow = false; // Nonaktifkan pemain melempar
    }

    void ThrowObject()
    {
        isThrowing = true;

        // Spawn objek yang dilempar
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        // Mendapatkan komponen Rigidbody2D dari projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Menghitung sudut lemparan dalam radian
        float angleInRadians = angle * Mathf.Deg2Rad;
        Vector2 throwDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // Terapkan gaya ke objek dengan kekuatan lemparan
        rb.AddForce(throwDirection * currentThrowForce, ForceMode2D.Impulse);

        // Reset status lemparan
        isThrowing = false;
        canThrow = false; // Nonaktifkan kemampuan melempar setelah lemparan
    }
}
