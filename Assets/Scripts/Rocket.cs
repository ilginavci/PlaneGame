using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform target_Transform;
    [SerializeField]
    private float rocket_Speed;
    private bool can_go = false;
    [SerializeField]
    private ParticleSystem rocket_gas;
    private void FixedUpdate() {
        if (can_go)
        {
            gameObject.transform.LookAt(target_Transform.position);
            gameObject.transform.Translate(Vector3.forward * rocket_Speed * Time.fixedDeltaTime);
        }
    }
    public void LaunchRocket(Player target_player){
        target_Transform = target_player.transform;
        gameObject.transform.SetParent(null);
        can_go = true;
        rocket_gas.Play();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform == target_Transform)
        {
             if (other.gameObject.CompareTag("Player1"))
             {
                 if (!PlayerManager.player_Manager.GetPlayer(true).has_Shield)
                 {
                    PlayerManager.player_Manager.ToBeShot(gameObject,true);
                 }
                 
             }
             else if (other.gameObject.CompareTag("Player2"))
             {
                 if (!PlayerManager.player_Manager.GetPlayer(false).has_Shield)
                 {
                    PlayerManager.player_Manager.ToBeShot(gameObject,false);
                 }
             }
             Destroy(gameObject);
        }
    }

}
