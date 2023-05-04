using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public int level = 0;
    public GameObject block; // prefab du cube
    public GameObject[] powerups; // liste des powerups

    // UI
    public TextMeshProUGUI txtLevel; // text du level

    private GameObject cubeList; // parent de tout les cubes
    private List<string> block_colors = new List<string>();


    public int getHigh(){
        return PlayerPrefs.GetInt("highscore");
    }

    public void setHigh(int levelmax){
        if (levelmax > getHigh()){
            PlayerPrefs.SetInt("highscore", level);
        }
    }

    public void setLevelText(){
        txtLevel.text = "Lvl " + level.ToString();
    }


    public void startGame(){
        // destroy all the cubes
        foreach (Transform child in cubeList.transform)
        {
            Destroy(child.gameObject);
        }
        // loop 3 times to create 3 rows
        for (level = 1; level <= 3; level++){
            // create a list of cubes
            createRow();
        }
        level--;
        
        setLevelText();
    }


    // Check si la partie est finie et restart
    public void isOver(){
        setHigh(level);
        bool isOver = false;
        foreach (Transform child in cubeList.transform)
        {
            if (child.position.y > 2.725f){
                // if the child is above the screen, destroy it
                if (child.gameObject.tag == "block"){
                    isOver = true;
                } else {
                    Destroy(child.gameObject);
                }
            }
        }
        if (isOver){
            startGame();
        }
    }


    // Génère une ligne de cubes avec des powerups aléatoires
    // La valeur des cubes est définie par la variable level
    public void createRow(){

        // Fait monter tout les cubes d'une ligne
        foreach (Transform child in cubeList.transform){
            child.position += new Vector3(0, 0.5f, 0);
        }

        List<GameObject> cubes = new List<GameObject>();
        GameObject cube;
        string rdm_color = block_colors[Random.Range(0, block_colors.Count)];
        float y = -2.775f;
        // loop 10 times to create 10 blocks
        for (float i = -2; i <= 2; i+=0.5f){
            // if 10% of chance
            if (Random.Range(0, 100) < 5){
                // create a powerup
                GameObject powerup = Instantiate(powerups[Random.Range(0, powerups.Length)], new Vector3(i, y, 0), Quaternion.identity);
                powerup.transform.parent = cubeList.transform;
                powerup.GetComponent<Pow>().pv = level;
                powerup.GetComponent<Pow>().setText();
                powerup.GetComponent<Pow>().setColor(rdm_color);
            } else {
                cube = Instantiate(block, new Vector3(i, y, 0), Quaternion.identity);
                cube.transform.parent = cubeList.transform;
                cube.GetComponent<Block>().pv = level*2;
                cube.GetComponent<Block>().setText();
                cube.GetComponent<Block>().setColor(rdm_color);
                // add cube to the list cubes
                cubes.Add(cube);
            }
        }
        // destroy on random cube from the list
        int rdm = Random.Range(0, 2);
        for (int i = 0; i < rdm; i++){
            Destroy(cubes[Random.Range(0, cubes.Count-1)]);
        }
    }


    public void upLevel(){
        level++;
        createRow();
        setLevelText();
        isOver();
    }


    void Awake()
    {
        block_colors.Add("#fffff");
        block_colors.Add("#f9584b");
        block_colors.Add("#ffd12b");
        block_colors.Add("#b7dffd");
        block_colors.Add("#80e8e0");
        block_colors.Add("#3eb489");
        // create a parent for all the cubes
        cubeList = new GameObject("CubeList");
        txtLevel = GameObject.Find("LevelTXT").GetComponent<TextMeshProUGUI>();
        startGame();
    }




}
