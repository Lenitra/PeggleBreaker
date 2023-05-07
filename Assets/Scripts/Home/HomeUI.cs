using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HomeUI : MonoBehaviour
{
    // Start is called before the first frame update

    private int highscore = 0;

    public TextMeshProUGUI txtHighscore;


    public int getHigh(){
        return PlayerPrefs.GetInt("highscore");
    }

    void Awake(){
        // txtHighscore = GameObject.Find("txtHighscore").GetComponent<TextMeshProUGUI>();
        highscore = getHigh();
        txtHighscore.text = "Best Score \n" + highscore.ToString();
    }














    public void infinite(){
        // load the scene "Game"
        UnityEngine.SceneManagement.SceneManager.LoadScene("infinite");
    }



}
