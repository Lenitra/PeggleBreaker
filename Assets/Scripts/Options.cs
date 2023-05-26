using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RDG;

public class Options : MonoBehaviour
{

    public GameObject optionsPanel;
    public GameObject vibrationToggle;


    public void back(){
        SceneManager.LoadScene("Home");
        Time.timeScale = 1;
    }

    public void restart(){
        SceneManager.LoadScene("infinite");
        Time.timeScale = 1;
    }

    public void toggleOptions(){
        
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        if (optionsPanel.activeSelf){
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void toggleVibration(){

        if (PlayerPrefs.GetInt("vibration") == 0){
            vibrationToggle.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("vibration", 1);
            Vibration.Vibrate(100, 50);
        } 
        else {
            PlayerPrefs.SetInt("vibration", 0);
            vibrationToggle.GetComponent<Toggle>().isOn = true;
        }
        
        PlayerPrefs.Save();
    }

    void Start(){
        if (!PlayerPrefs.HasKey("vibration")){
            PlayerPrefs.SetInt("vibration", 1);
        }
        if (PlayerPrefs.GetInt("vibration") == 1){
            vibrationToggle.GetComponent<Toggle>().isOn = false;
        } else {
            vibrationToggle.GetComponent<Toggle>().isOn = true;
        }
    }



}
