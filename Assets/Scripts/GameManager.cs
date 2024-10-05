using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController player2;
    private PlayerController currentPlayer;

    void Start()
    {
        currentPlayer = player1; // Set player 1 sebagai pemain pertama
        player1.EnableThrow(); // Izinkan player 1 melempar pertama kali
        player2.DisableThrow(); // Nonaktifkan lemparan untuk player 2 saat belum gilirannya
    }

    void Update()
    {
        // Cek apakah giliran sudah selesai (misalnya saat lemparan selesai)
        if (Input.GetKeyDown(KeyCode.Return)) // Tombol untuk switch giliran
        {
            SwitchTurn();
        }
    }

    void SwitchTurn()
    {
        // Tukar giliran antara player 1 dan player 2
        if (currentPlayer == player1)
        {
            currentPlayer = player2;
            player1.DisableThrow();
            player2.EnableThrow();
        }
        else
        {
            currentPlayer = player1;
            player2.DisableThrow();
            player1.EnableThrow();
        }

        Debug.Log("Turn switched to " + currentPlayer.gameObject.name);
    }
}
