using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PowerController player1,player2;
    private Dictionary<PowerController,Custom_Powers> player_Power = new Dictionary<PowerController, Custom_Powers>();
    private static PlayerManager playerManager_Instance = null;
    public GameObject destroyEffect;
    [SerializeField]
    private GameObject Rocket_Prefab,Shield_Prefab;
    public static PlayerManager player_Manager{
        get{
            if (playerManager_Instance == null)
            {
                playerManager_Instance = new GameObject("Player Manager").AddComponent<PlayerManager>();
            }
            return playerManager_Instance;
        }
    }
    private void OnEnable() {
        playerManager_Instance = this;
    }
    private void Start() {
        player_Power[player1] = Custom_Powers.NONE;
        player_Power[player2] = Custom_Powers.NONE;
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
    public PowerController GetPlayer(bool is_Player1){
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
        var target = GetPlayer(is_Player1);
        target.player_cam.transform.SetParent(null);
        target.gameObject.SetActive(false);
        destroyEffect.SetActive(false);
        destroyEffect.transform.position = target.transform.position;
        destroyEffect.SetActive(true);
        StartCoroutine(RespawnDelay(is_Player1));
    }
    IEnumerator RespawnDelay(bool is_Player1){
        yield return new WaitForSeconds(1.5f);
        var target = GetPlayer(is_Player1);
        target.gameObject.SetActive(true);
        target.player_cam.transform.SetParent(target.transform);
        RespawnPlayer(is_Player1);
    }
    public enum Custom_Powers
    {
        ROCKET,
        SPEED_BOOSTER,
        SHIELD,
        NONE
    }
    public void SetPlayerPower(PowerController player,Custom_Powers power)
    {
        player_Power[player] = power;
        switch (player_Power[player])
        {
            case Custom_Powers.ROCKET:
                if (player.GetRocket() == null)
                {
                    TakeRocket(player.GetRocketPart());
                }
                break;
        }
    }
    public Custom_Powers GetPlayerPower(PowerController player){
        return player_Power[player];
    }
    private void TakeRocket(Transform rocket_part){
        Debug.Log("Rocket part: " + rocket_part.position + " Rocket_Prefab: " + Rocket_Prefab.name);
        GameObject rocket_go = GameObject.Instantiate(Rocket_Prefab,rocket_part.position,Quaternion.EulerAngles(new Vector3(0,-90,0)));
        rocket_go.transform.parent = rocket_part;
        rocket_go.transform.localPosition = new Vector3(0,0,2.21f);
        rocket_go.transform.rotation = new Quaternion(0,0,0,rocket_go.transform.rotation.z);
        if (player_Power[GetPlayer(true)] == Custom_Powers.ROCKET)
        {
            GetPlayer(true).SetRocket(rocket_go.GetComponent<Rocket>());
        }
        if (player_Power[GetPlayer(false)] == Custom_Powers.ROCKET)
        {
            GetPlayer(false).SetRocket(rocket_go.GetComponent<Rocket>());
        }
    }
    public void TakeShield(Transform player_transform)
    {
        Debug.Log("Shield part: " + player_transform.position + "Shield_Prefab: " + Shield_Prefab.name);
        GameObject shield_go = GameObject.Instantiate(Shield_Prefab,player_transform.position,Quaternion.EulerAngles(Vector3.zero));
        shield_go.transform.parent = player_transform;
        shield_go.transform.localPosition = Vector3.zero;
        if (player_Power[GetPlayer(true)] == Custom_Powers.SHIELD)
        {
            GetPlayer(true).SetShield(shield_go.GetComponent<Shield>());
        }
        if (player_Power[GetPlayer(false)] == Custom_Powers.SHIELD)
        {
            GetPlayer(false).SetShield(shield_go.GetComponent<Shield>());
        }
    }
    public void SetRandomPower(PowerController player){
        int random_n = Random.Range(0,3);//Random.Range(0,3);
        if (random_n == 1)
        {
            SetPlayerPower(player, Custom_Powers.ROCKET);
        }
        else if(random_n == 0){
            SetPlayerPower(player,Custom_Powers.SPEED_BOOSTER);
        }
        else if (random_n == 2)
        {
            SetPlayerPower(player,Custom_Powers.SHIELD);
        }
    }
    
}
