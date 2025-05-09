using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    private DrawCards player1 = null;
    private DrawCards player2 = null;
    private TextMeshProUGUI player1GameOverText, player1Text;
    private TextMeshProUGUI player2GameOverText, player2Text;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private GameObject restartButton;

    private DrawCards currentPlayer;
    private bool gameOver = false, gameStarted = false;
    private DrawCards lostPlayer;
    private DrawCards wonPlayer;
    private int animationNumber;

    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private AudioSource drawCardSource, shuffleCardsSource;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple GameManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        playerManager.playerInputManager.onPlayerJoined += OnPlayerJoined;
    }

    private void OnDisable()
    {
        playerManager.playerInputManager.onPlayerJoined -= OnPlayerJoined;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        if (player1 == null)
        {
            player1 = input.gameObject.GetComponent<DrawCards>();
            player1GameOverText = player1.GetComponent<DrawCards>().gameOverText;
            player1Text = player1.GetComponent<DrawCards>().playerText;
            player1Text.text = "Player 1";
        }
        else if (player2 == null)
        {
            player2 = input.gameObject.GetComponent<DrawCards>();
            player2GameOverText = player2.GetComponent<DrawCards>().gameOverText;
            player2Text = player2.GetComponent<DrawCards>().playerText;
            player2Text.text = "Player 2";
        }
        else
        {
            Debug.LogError("Two players already joined.");
            return;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        NextRound();
    }

    private bool CheckPlayer(GameObject player)
    {
        return currentPlayer.gameObject == player;
    }

    public void CheckDraw(GameObject player)
    {
        if (gameOver) return;

        Debug.Log(player);
        if (CheckPlayer(player)) return;
        if (currentPlayer.stand || currentPlayer.aceValueChoice.activeSelf) return;

        Debug.Log(currentPlayer.name);
        currentPlayer.DrawCard();
        drawCardSource.Play();
        if (currentPlayer.aceValueChoice.activeSelf) return;

        if (currentPlayer.count > 21)
        {
            currentPlayer.stand = true;
            EndGame();
            return;
        }

        SwitchTurn();
    }

    public void Stand(GameObject player)
    {
        if (gameOver) return;
        if (CheckPlayer(player)) return;

        currentPlayer.stand = true;
        SwitchTurn();
    }

    private void SwitchTurn()
    {
        for (int i = 0; i < 2; i++)
        {
            currentPlayer.playerText.color = Color.red;
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
            SwitchPlayerState();
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
            animationNumber = 2;
        }
        else if (p2 > p1)
        {
            player2GameOverText.text = "You Won!";
            player1GameOverText.text = "You Lost!";
            lostPlayer = player1;
            wonPlayer = player2;
            animationNumber = 1;
        }
        else
        {
            player1GameOverText.text = "It's a Tie!";
            player2GameOverText.text = "It's a Tie!";
            lostPlayer = null;
            wonPlayer = null;
            animationNumber = 0;
        }
        StartCoroutine(CheckShotGun());
    }

    public void AceValue(GameObject player, int v)
    {
        if (currentPlayer.gameObject == player || !currentPlayer.aceValueChoice.activeSelf) return;
        currentPlayer.count += v;
        currentPlayer.aceValueChoice.SetActive(false);
        if (currentPlayer.count > 21)
        {
            currentPlayer.stand = true;
            EndGame();
            return;
        }
        SwitchTurn();
    }

    private IEnumerator CheckShotGun()
    {
        if (lostPlayer == null)
        {
            yield return new WaitForSeconds(1);
            NextRound();
            yield break;
        }

        shotgun.shotgunAnimator.SetInteger("Who", animationNumber);
        yield return new WaitForSeconds(0.2f);
        AnimatorStateInfo stateInfo = shotgun.shotgunAnimator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        yield return new WaitForSeconds(animationLength);

        animationNumber = 0;
        shotgun.shotgunAnimator.SetInteger("Who", animationNumber);
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
            player1Text.color = Color.green;
            player2Text.color = Color.red;
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

    public bool GameStarted()
    {
        return gameStarted;
    }

    public void RestartGame()
    {
        player1GameOverText.text = "";
        player2GameOverText.text = "";
        player1Text.color = Color.white;
        player2Text.color = Color.white;
        gameOver = false;
        gameStarted = false;
        currentPlayer = null;
        wonPlayer = null;
        lostPlayer = null;
    }

    private void SwitchPlayerState()
    {
        if (currentPlayer == player1)
        {
            player1.GetComponent<EyeFollow>().LookDown();
            player2.GetComponent<EyeFollow>().LookAtOtherPlayer();
        }
        else
        {
            player2.transform.GetComponent<EyeFollow>().LookDown();
            player1.GetComponent<EyeFollow>().LookAtOtherPlayer();
        }
    }
}