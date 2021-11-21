using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform target_Transform;
    [SerializeField]
    private float rocket_Speed;
    public void LaunchRocket(Vector3 target_position,bool is_Player1){
        if (is_Player1)
        {
            target_Transform = PlayerManager.player_Manager.GetPlayer(false).gameObject.transform;
        }
        else
        {
            target_Transform = PlayerManager.player_Manager.GetPlayer(true).gameObject.transform;
        }
        gameObject.transform.LookAt(target_position);
        gameObject.transform.Translate(Vector3.forward * rocket_Speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player1"))
        {
            PlayerManager.player_Manager.ToBeShot(gameObject,true);
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            PlayerManager.player_Manager.ToBeShot(gameObject,false);
        }
    }
}
