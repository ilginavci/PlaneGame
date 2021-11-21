using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool is_Player1;
    [SerializeField]
    private Transform rocket_part;
    private Rocket rocket = null;
    private Shield shield = null;
    private bool is_Faster = false;
    private Player1Controller player_controller;
    public Camera player_cam;
    public bool has_Shield = false;
    private void Awake() {
        player_controller = gameObject.GetComponent<Player1Controller>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("CustomPower"))
        {
            other.gameObject.SetActive(false);
            PlayerManager.player_Manager.SetRandomPower(this);
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UsePower(PlayerManager.player_Manager.GetPlayerPower(this));
        }
    }
    public Transform GetRocketPart(){
        return rocket_part;
    }
    public void SetRocket(Rocket new_rocket){
        if (rocket == null)
        {
            rocket = new_rocket;
        }
    }
    public Rocket GetRocket(){
        return rocket;
    }
    public void SetFaster(){
        is_Faster = true;
        player_controller.speed += 15;
        Debug.Log(gameObject.name +" " + "Faster");
        StartCoroutine(FasterCouldown());
    }
    public void SetShield(Shield new_Shield){
        if (!has_Shield)
        {
            shield = new_Shield;
            has_Shield = true;
        }
    }
    public bool IsFaster(){
        return is_Faster;
    }
    private void UsePower(PlayerManager.Custom_Powers power){
        switch (power)
        {
            case PlayerManager.Custom_Powers.ROCKET:
                if (is_Player1)
                {
                    rocket.LaunchRocket(PlayerManager.player_Manager.GetPlayer(false));
                }
                else
                {
                    rocket.LaunchRocket(PlayerManager.player_Manager.GetPlayer(true));
                }
                break;
            case PlayerManager.Custom_Powers.SPEED_BOOSTER:
                if (!is_Faster)
                {
                    SetFaster();
                }
                break;
            case PlayerManager.Custom_Powers.SHIELD:
                Debug.Log("Testt");
                if (!has_Shield)
                {
                    PlayerManager.player_Manager.TakeShield(transform);
                    StartCoroutine(ShieldCouldown());
                }
                break;
        }
        rocket = null;
        PlayerManager.player_Manager.SetPlayerPower(this,PlayerManager.Custom_Powers.NONE);
    }
    IEnumerator FasterCouldown(){
        yield return new WaitForSeconds(2.5f);
        is_Faster = false;
        player_controller.speed -= 15;
        PlayerManager.player_Manager.SetPlayerPower(this,PlayerManager.Custom_Powers.NONE);
    }
    IEnumerator ShieldCouldown(){
        yield return new WaitForSeconds(3f);
        Destroy(shield.gameObject);
        has_Shield = false;
        PlayerManager.player_Manager.SetPlayerPower(this,PlayerManager.Custom_Powers.NONE);
    }
}
