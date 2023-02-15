using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectWinner : MonoBehaviour
{

int winner;
[SerializeField] TextMeshProUGUI winnerText;
    public void Start () {
        //gets winner and shows the winner on screen
        winner = PlayerPrefs.GetInt("winner");
        switch (winner) {
            case 0:
                winnerText.text = "Draw!";
                break;
            case 1:
                winnerText.text = "Player 1 wins!";
                break;
            case 2:
                winnerText.text = "Player 2 wins!";
                break;
            default:
                break;
        }
    }
}
