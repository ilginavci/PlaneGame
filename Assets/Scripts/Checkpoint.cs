using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Dictionary<Player, bool> did_Player_triggered = new Dictionary<Player, bool>();
    private void Start()
    {
        did_Player_triggered[PlayerManager.player_Manager.GetPlayer(true)] = false;
        did_Player_triggered[PlayerManager.player_Manager.GetPlayer(false)] = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            if (this == GameManager.gameManager.GetPlayerCheckpoint(true))
            {
                if (!did_Player_triggered[PlayerManager.player_Manager.GetPlayer(true)])
                {
                    GameManager.gameManager.NextCheckpoint(true);
                    did_Player_triggered[PlayerManager.player_Manager.GetPlayer(true)] = true;
                }

            }
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            if (this == GameManager.gameManager.GetPlayerCheckpoint(false))
            {
                if (!did_Player_triggered[PlayerManager.player_Manager.GetPlayer(false)])
                {
                    GameManager.gameManager.NextCheckpoint(false);
                    did_Player_triggered[PlayerManager.player_Manager.GetPlayer(false)] = true;
                }
            }
        }
    }
    public void ChangeChildrenLayer(LayerMask layerMask)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.layer = layerMask;
        }
    }
}
