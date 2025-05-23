using BASE;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // Singleton

    [Range(1, 4)]                           // A�ade un slider para facilitar la asignacion de datos desde el editmode
    public int numberOfPlayers = 1;
    public PlayerScore[] playerScore;       // No nos ha dado tiempo

    public bool inputsAreEnabled = true;

    private void Awake()
    {
        // Singelton Pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        this.numberOfPlayers = numberOfPlayers;
        playerScore = new PlayerScore[numberOfPlayers];
    }

    public void AddScoreToPlayerId(int id) 
    {
        playerScore[id].score += 1;
    }
}
