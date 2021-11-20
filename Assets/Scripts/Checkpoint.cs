using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  
   
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player1"))
        {
            GameManager.gameManager.NextCheckpoint(true);
        }
        else
        {   
            GameManager.gameManager.NextCheckpoint(false);
        }
    }
    public void ChangeChildrenLayer(LayerMask layerMask){
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.layer = layerMask;
        }
    }
}
