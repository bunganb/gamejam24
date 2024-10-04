using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player1;
    public PlayerController player2;
    private PlayerController currentPlayer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentPlayer = player1; // Mulai dengan pemain 1
        currentPlayer.SetTurn(true); // Set giliran pemain 1
    }

    public void EndTurn()
    {
        // Ganti giliran
        currentPlayer.SetTurn(false);
        currentPlayer = (currentPlayer == player1) ? player2 : player1; // Ganti ke pemain lain
        currentPlayer.SetTurn(true);
    }
}
