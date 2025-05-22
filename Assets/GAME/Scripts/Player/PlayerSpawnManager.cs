using System.Collections;
using BASE;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject[] players;
    public GameObject[] spawnObjects;
    public InputKeycodes_SO[] playerInputs;
    public Color[] playerColors;

    void Start()
    {
        int numberOfPlayers = GameManager.instance.numberOfPlayers;
        players = new GameObject[numberOfPlayers];
        
        int spawnIndex = Random.Range(0, spawnObjects.Length);
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = Instantiate(playerPrefab);
            players[i].transform.position = spawnObjects[spawnIndex].transform.position;
            players[i].GetComponent<PlayerController>().inputKeycodes = playerInputs[i];
            
            // Player color
            SpriteRenderer[] spriteRenderer = players[i].GetComponentsInChildren<SpriteRenderer>();
            for (int j = 0; j < spriteRenderer.Length; j++)
                spriteRenderer[j].color = playerColors[i];

            spawnIndex++;
            if (spawnIndex >= spawnObjects.Length)
            {
                spawnIndex = 0;
            }
        }
    }

}
