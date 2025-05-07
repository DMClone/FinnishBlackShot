using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DrawCards player1;
    [SerializeField] private DrawCards player2;

    [SerializeField] private DrawCards currentPlayer;
    private bool gameOver = false;

    private void Start()
    {
        player1.NewStart();
        player2.NewStart();
        currentPlayer = player1;
        player1.playerText.color = Color.green;
        player2.playerText.color = Color.red;
    }

    private void Update()
    {
        if (gameOver) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentPlayer.stand)
            {
                Debug.Log($"{currentPlayer.name} has already stood.");
                return;
            }

            currentPlayer.DrawCard();
            Debug.Log($"{currentPlayer.name} drew a card. Count: {currentPlayer.count}");

            if (currentPlayer.count > 21)
            {
                Debug.Log($"{currentPlayer.name} busted!");
                currentPlayer.stand = true;
                EndGame();
                return;
            }

            SwitchTurn();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            currentPlayer.stand = true;
            Debug.Log($"{currentPlayer.name} stands.");
            SwitchTurn();
        }
    }

    private void SwitchTurn()
    {
        for (int i = 0; i < 2; i++)
        {
            currentPlayer.playerText.color = Color.red;
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
            currentPlayer.playerText.color = Color.green;

            if (!currentPlayer.stand)
            {
                Debug.Log($"Now it's {currentPlayer.name}'s turn.");
                return;
            }
        }

        Debug.Log("Both players have stood. Ending game...");
        EndGame();
    }

    private void EndGame()
    {
        gameOver = true;

        int p1 = player1.count > 21 ? 0 : player1.count;
        int p2 = player2.count > 21 ? 0 : player2.count;

        if (p1 > p2)
        {
            Debug.Log("Player 1 wins!");
        }
        else if (p2 > p1)
        {
            Debug.Log("Player 2 wins!");
        }
        else
        {
            Debug.Log("It's a tie!");
        }
    }
}
