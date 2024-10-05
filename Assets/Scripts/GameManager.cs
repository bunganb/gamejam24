using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController player2;
    private PlayerController currentHealth,currentPlayer;

    void Start()
    {
        currentPlayer = player1; // Set player 1 sebagai pemain pertama
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Tombol untuk switch giliran
        {
            SwitchTurn();
        }
    }

    void SwitchTurn()
    {
        currentPlayer = (currentPlayer == player1) ? player2 : player1;
        Debug.Log("Turn switched to " + currentPlayer.gameObject.name);
    }

    public void CheckGameOver()
    {
         // Gunakan getter method untuk mengakses nilai currentHealth
        if (player1.GetComponent<PlayerHealth>().GetCurrentHealth() <= 0)
        {
            Debug.Log("Player 2 Wins!");
        }
        else if (player2.GetComponent<PlayerHealth>().GetCurrentHealth() <= 0)
        {
            Debug.Log("Player 1 Wins!");
        }
    }
}
