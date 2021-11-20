using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int p1_Current_Checkpoint, p2_Current_Checkpoint = 0;
    private static GameManager gameManager_Instance = null;
    [SerializeField]
    private List<Checkpoint> players_Checkpoints = new List<Checkpoint>();
    private bool is_p1_Last, is_p2_Last = false;
    public static GameManager gameManager
    {
        get
        {
            if (gameManager_Instance == null)
            {
                gameManager_Instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return gameManager_Instance;
        }
    }
    private void OnEnable()
    {
        gameManager_Instance = this;
    }
    private void Start()
    {
        SetCheckpoints();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextCheckpoint(true);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            NextCheckpoint(false);
        }
    }
    public void NextCheckpoint(bool is_Player1)
    {
        if (is_Player1)
        {
            if (p1_Current_Checkpoint < players_Checkpoints.Count)
            {
                p1_Current_Checkpoint++;
                if (p1_Current_Checkpoint >= players_Checkpoints.Count)
                {
                    is_p1_Last = true;
                    p1_Current_Checkpoint = players_Checkpoints.Count - 1;
                }
                SetCheckpoints();
            }

        }
        else
        {
            if (p2_Current_Checkpoint < players_Checkpoints.Count)
            {
                p2_Current_Checkpoint++;
                if (p2_Current_Checkpoint >= players_Checkpoints.Count)
                {
                    is_p2_Last = true;
                    p2_Current_Checkpoint = players_Checkpoints.Count - 1;
                }
                SetCheckpoints();
            }

        }

    }
    private void SetCheckpoints()
    {
        foreach (Checkpoint checkpoint in players_Checkpoints)
        {
            if (checkpoint == players_Checkpoints[p1_Current_Checkpoint])
            {
                if (p1_Current_Checkpoint == p2_Current_Checkpoint)
                {
                    checkpoint.gameObject.layer = LayerMask.NameToLayer("BothP1_P2");
                    checkpoint.ChangeChildrenLayer(LayerMask.NameToLayer("BothP1_P2"));

                }
                else
                {
                    checkpoint.gameObject.layer = LayerMask.NameToLayer("OnlyP1");
                    checkpoint.ChangeChildrenLayer(LayerMask.NameToLayer("OnlyP1"));
                }
            }
            else
            {
                if (checkpoint == players_Checkpoints[p2_Current_Checkpoint])
                {
                    if (p1_Current_Checkpoint == p2_Current_Checkpoint)
                    {
                        checkpoint.gameObject.layer = LayerMask.NameToLayer("BothP1_P2");
                        checkpoint.ChangeChildrenLayer(LayerMask.NameToLayer("BothP1_P2"));
                    }
                    else
                    {
                        checkpoint.gameObject.layer = LayerMask.NameToLayer("OnlyP2");
                        checkpoint.ChangeChildrenLayer(LayerMask.NameToLayer("OnlyP2"));
                    }
                }
                else
                {
                    checkpoint.gameObject.layer = LayerMask.NameToLayer("NoneP1_P2");
                    checkpoint.ChangeChildrenLayer(LayerMask.NameToLayer("NoneP1_P2"));
                }
            }
            if (is_p1_Last)
            {
                if (is_p2_Last)
                {
                    players_Checkpoints[p1_Current_Checkpoint].gameObject.layer = LayerMask.NameToLayer("NoneP1_P2");
                    players_Checkpoints[p1_Current_Checkpoint].ChangeChildrenLayer(LayerMask.NameToLayer("NoneP1_P2"));
                }
                else
                {
                    if (p2_Current_Checkpoint == players_Checkpoints.Count - 1 && !is_p2_Last)
                    {
                        players_Checkpoints[p2_Current_Checkpoint].gameObject.layer = LayerMask.NameToLayer("OnlyP2");
                        players_Checkpoints[p2_Current_Checkpoint].ChangeChildrenLayer(LayerMask.NameToLayer("OnlyP2"));
                    }
                    else
                    {
                        players_Checkpoints[p1_Current_Checkpoint].gameObject.layer = LayerMask.NameToLayer("NoneP1_P2");
                        players_Checkpoints[p1_Current_Checkpoint].ChangeChildrenLayer(LayerMask.NameToLayer("NoneP1_P2"));
                       
                    }
                     
                }
            }
            if (is_p2_Last)
            {
                if (is_p1_Last)
                {
                    players_Checkpoints[p2_Current_Checkpoint].gameObject.layer = LayerMask.NameToLayer("NoneP1_P2");
                    players_Checkpoints[p2_Current_Checkpoint].ChangeChildrenLayer(LayerMask.NameToLayer("NoneP1_P2"));
                }
                else
                {
                    if (p1_Current_Checkpoint == players_Checkpoints.Count - 1 && !is_p1_Last)
                    {
                        players_Checkpoints[p1_Current_Checkpoint].gameObject.layer = LayerMask.NameToLayer("OnlyP1");
                        players_Checkpoints[p1_Current_Checkpoint].ChangeChildrenLayer(LayerMask.NameToLayer("OnlyP1"));
                    }
                    else
                    {
                        players_Checkpoints[p2_Current_Checkpoint].gameObject.layer = LayerMask.NameToLayer("NoneP1_P2");
                        players_Checkpoints[p2_Current_Checkpoint].ChangeChildrenLayer(LayerMask.NameToLayer("NoneP1_P2"));
                       
                    }
                }
            }
        }
        Debug.Log("P1: " + p1_Current_Checkpoint);
        Debug.Log("P2: " + p2_Current_Checkpoint);

    }
}
