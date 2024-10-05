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

    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Space) && !isThrowing)
    {
    chargeStartTime = Time.time;
    }
    if (Input.GetKey(KeyCode.Space) && !isThrowing)
    {
    float chargeDuration = Time.time - chargeStartTime;
    currentThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargeDuration / chargeTime);
    if (chargeDuration >= chargeTime)
    {
    ThrowObject();
    }
    }

    // Jika tombol Space ditekan, mulai menghitung charge
    if (Input.GetKeyDown(KeyCode.Space) && !isThrowing)
    {
    chargeStartTime = Time.time;
    }

    // Jika tombol Space masih ditekan, hitung kekuatan dan sudut berdasarkan durasi penekanan
    if (Input.GetKey(KeyCode.Space) && !isThrowing)
    {
    float chargeDuration = Time.time - chargeStartTime;

    // Kekuatan lemparan
    currentThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargeDuration / chargeTime);

    // Sudut lemparan (misalnya antara 30 hingga 60 derajat)
    angle = Mathf.Lerp(30f, 60f, chargeDuration / chargeTime);

    // Jika sudah mencapai kekuatan maksimum, lempar otomatis
    if (chargeDuration >= chargeTime)
        {
        ThrowObject();
        }
    }

    // Jika tombol Space dilepas sebelum mencapai batas maksimal, lempar dengan kekuatan dan sudut saat ini
    if (Input.GetKeyUp(KeyCode.Space) && !isThrowing)
        {
        ThrowObject();
        }
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

            // Menghitung arah lemparan dengan menggunakan sudut dan gravitasi
            Vector2 throwDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            // Terapkan gaya ke objek dengan kekuatan lemparan
            rb.AddForce(throwDirection * currentThrowForce, ForceMode2D.Impulse);

            // Reset status lemparan
            isThrowing = false;
            currentThrowForce = minThrowForce; // Reset kekuatan lemparan untuk lemparan berikutnya
        }
    }
