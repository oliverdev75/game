using System;
using System.Collections;
using BASE;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject[] players;
    public GameObject[] spawnObjects;
    public InputKeycodes_SO[] playerInputs;
    public PlayerSkinData[] playerSkins;

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
            
            // Player skin
            players[i].GetComponentInChildren<SpriteRenderer>().sprite = playerSkins[i].sprite;
            players[i].GetComponentInChildren<CharacterLegsAnimation>().SetLegColor(playerSkins[i].color);
            players[i].GetComponentInChildren<CharacterHealth>().SetDeathSplashColor(playerSkins[i].color);
            
            spawnIndex++;
            if (spawnIndex >= spawnObjects.Length)
            {
                spawnIndex = 0;
            }
        }
    }

    [Serializable]
    public struct PlayerSkinData
    {
        public Sprite sprite;
        public Color color;
    }
    
}
