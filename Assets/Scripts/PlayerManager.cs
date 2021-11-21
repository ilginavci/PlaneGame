using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player player1,player2;
    private Dictionary<Player,Custom_Powers> player_Power = new Dictionary<Player, Custom_Powers>();
    private static PlayerManager playerManager_Instance = null;
    [SerializeField]
    private GameObject Rocket_Prefab;
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
        GetPlayer(is_Player1).player_cam.transform.SetParent(null);
        GetPlayer(is_Player1).gameObject.SetActive(false);
        //StartCoroutine(RespawnDelay(is_Player1));
    }
    IEnumerator RespawnDelay(bool is_Player1){
        yield return new WaitForSeconds(0.6f);
        RespawnPlayer(is_Player1);
    }
    public enum Custom_Powers
    {
        ROCKET,
        SPEED_BOOSTER,
        GUN,
        NONE
    }
    public void SetPlayerPower(Player player,Custom_Powers power)
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
    public Custom_Powers GetPlayerPower(Player player){
        return player_Power[player];
    }
    private void TakeRocket(Transform rocket_part){
        Debug.Log("Rocket part: " + rocket_part.position + " Rocket_Prefab: " + Rocket_Prefab.name);
        GameObject rocket_go = GameObject.Instantiate(Rocket_Prefab,rocket_part.position,Quaternion.EulerAngles(Vector3.zero));
        rocket_go.transform.parent = rocket_part;
        rocket_go.transform.localPosition = Vector3.zero;
        if (player_Power[GetPlayer(true)] == Custom_Powers.ROCKET)
        {
            GetPlayer(true).SetRocket(rocket_go.GetComponent<Rocket>());
        }
        if (player_Power[GetPlayer(false)] == Custom_Powers.ROCKET)
        {
            GetPlayer(false).SetRocket(rocket_go.GetComponent<Rocket>());
        }
    }
    public void SetRandomPower(Player player){
        int random_n = Random.Range(0,2);
        if (random_n == 1)
        {
            SetPlayerPower(player, Custom_Powers.ROCKET);
        }
        else if(random_n == 0){
            SetPlayerPower(player,Custom_Powers.SPEED_BOOSTER);
        }
    }
    
}
