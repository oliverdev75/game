using BASE;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int numberOfPlayers = 1;

    [Space]

    public InputKeycodes_SO playerInputs;

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
}
