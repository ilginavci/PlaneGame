using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager ui_Manager_Instance = null;
    [SerializeField]
    private GameObject custom_Power_UI;
    private Animator ui_Anim;
    private void Awake() {
        ui_Anim = gameObject.GetComponent<Animator>();
    }
    public static UI_Manager ui_Manager{
        get{
            if (ui_Manager_Instance == null)
            {
                ui_Manager_Instance = new GameObject("UI Manager").AddComponent<UI_Manager>();
            }
            return ui_Manager_Instance;
        }
    }
    private void OnEnable() {
        ui_Manager_Instance = this;
    }
    public void OpenCustomPowerUI(){
        custom_Power_UI.SetActive(true);
        ui_Anim.Play("CustomPowerAnim");
    }
}
