using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DrawCards player1;
    [SerializeField] private DrawCards player2;
    [SerializeField] private TextMeshProUGUI player1GameOverText;
    [SerializeField] private TextMeshProUGUI player2GameOverText;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private GameObject restartButton;

    private DrawCards currentPlayer;
    private bool gameOver = false;
    private DrawCards lostPlayer;
    private DrawCards wonPlayer;

    private void Start()
    {
        NextRound();
    }

    private void Update()
    {
        if (gameOver) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentPlayer.stand || currentPlayer.AceValueChoice.activeSelf) return;

            currentPlayer.DrawCard();
            if (currentPlayer.AceValueChoice.activeSelf) return;

            if (currentPlayer.count > 21)
            {
                currentPlayer.stand = true;
                EndGame();
                return;
            }

            SwitchTurn();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            currentPlayer.stand = true;
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
                return;
            }
        }

        EndGame();
    }

    private void EndGame()
    {
        gameOver = true;

        int p1 = player1.count > 21 ? 0 : player1.count;
        int p2 = player2.count > 21 ? 0 : player2.count;

        player1GameOverText.enabled = true;
        player2GameOverText.enabled = true;

        if (p1 > p2)
        {
            player1GameOverText.text = "You Won!";
            player2GameOverText.text = "You Lost!";
            lostPlayer = player2;
            wonPlayer = player1;
        }
        else if (p2 > p1)
        {
            player2GameOverText.text = "You Won!";
            player1GameOverText.text = "You Lost!";
            lostPlayer = player1;
            wonPlayer = player2;
        }
        else
        {
            player1GameOverText.text = "It's a Tie!";
            player2GameOverText.text = "It's a Tie!";
            lostPlayer = null;
            wonPlayer = null;
        }
        CheckShotGun();
    }

    public void AceValue(int v)
    {
        currentPlayer.count += v;
        currentPlayer.AceValueChoice.SetActive(false);
        if (currentPlayer.count > 21)
        {
            currentPlayer.stand = true;
            EndGame();
            return;
        }
        SwitchTurn();
    }

    private void CheckShotGun()
    {
        if (lostPlayer == null) return;
        if (shotgun.shots[0] == true)
        {
            restartButton.SetActive(true);
        }
        else
        {
            shotgun.shots.RemoveAt(0);
            NextRound();
        }
    }

    private void NextRound()
    {
        if (lostPlayer != null)
        {
            currentPlayer = wonPlayer;
            wonPlayer.playerText.color = Color.green;
            lostPlayer.playerText.color = Color.red;
        }
        else
        {
            currentPlayer = player1;
            player1.playerText.color = Color.green;
            player2.playerText.color = Color.red;
        }
        player1.NewStart();
        player2.NewStart();
        player1GameOverText.enabled = false;
        player2GameOverText.enabled = false;
        gameOver = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
