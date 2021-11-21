using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player player1,player2;
    private static PlayerManager playerManager_Instance = null;
    public static PlayerManager player_Manager{
        get{
            if (playerManager_Instance == null)
            {
                playerManager_Instance = new GameObject("Player Manager").AddComponent<PlayerManager>();
            }
            return playerManager_Instance;
        }
    }
    public void RespawnPlayer(bool is_player1)
    {
        if (is_player1)
        {
            player1.gameObject.transform.position = GameManager.gameManager.GetPlayerCheckpoint(true).gameObject.transform.position;
        }
        else
        {
            player2.gameObject.transform.position = GameManager.gameManager.GetPlayerCheckpoint(false).gameObject.transform.position;
        }
    }
    public Player GetPlayer(bool is_Player1){
        if (is_Player1)
        {
            return player1;
        }
        else
        {
            return player2;
        }
    }
    public void ToBeShot(GameObject go,bool is_Player1){
        go.SetActive(false);
        StartCoroutine(RespawnDelay(is_Player1));
    }
    IEnumerator RespawnDelay(bool is_Player1){
        yield return new WaitForSeconds(0.6f);
        RespawnPlayer(is_Player1);
    }
}
