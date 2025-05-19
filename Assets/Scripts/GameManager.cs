using BASE;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int numberOfPlayers = 1;
    public PlayerScore[] playerScore;

    private void Awake()
    {
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
    }

    public void AddScoreToPlayerId(int id) 
    {
        playerScore[id].score += 1;
    }
}
