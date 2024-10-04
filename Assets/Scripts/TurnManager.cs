using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TurnManager 
{
    public static bool isPlayer1Turn = true; // Mulai dari Player1

    public static void SwitchTurn()
    {
        isPlayer1Turn = !isPlayer1Turn; // Ubah giliran
    }
}
