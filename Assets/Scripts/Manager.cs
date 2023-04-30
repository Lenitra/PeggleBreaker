using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int level = 0;
    public GameObject block; // prefab du cube
    public GameObject[] powerups; // liste des powerups


    private GameObject cubeList; // parent de tout les cubes
    private List<string> block_colors = new List<string>();



    public void upLevel(){
        // loop into childrens of the parent cubeList
        foreach (Transform child in cubeList.transform)
        {
            // move to up child
            child.position += new Vector3(0, 0.5f, 0);
            if (child.position.y > 2.725f){
                // if the child is above the screen, destroy it
                Destroy(child.gameObject);
            }
        }

        level++;
        // create a list of cubes
        List<GameObject> cubes = new List<GameObject>();
        GameObject cube;
        string rdm_color = block_colors[Random.Range(0, block_colors.Count)];
        float y = -2.775f;
        // x 2.25
        // loop 10 times to create 10 rows
        for (float i = -2; i <= 2; i+=0.5f){
            // if 10% of chance
            if (Random.Range(0, 100) < 10){
                // create a powerup
                GameObject powerup = Instantiate(powerups[Random.Range(0, powerups.Length)], new Vector3(i, y, 0), Quaternion.identity);
                powerup.transform.parent = cubeList.transform;
                powerup.GetComponent<Pow>().pv = level;
                powerup.GetComponent<Pow>().setText();
                powerup.GetComponent<Pow>().setColor(rdm_color);
            } else {
                cube = Instantiate(block, new Vector3(i, y, 0), Quaternion.identity);
                cube.transform.parent = cubeList.transform;
                cube.GetComponent<Block>().pv = level;
                cube.GetComponent<Block>().setText();
                cube.GetComponent<Block>().setColor(rdm_color);
                // add cube to the list cubes
                cubes.Add(cube);
            }
        }
        // destroy on random cube from the list
        // get the length of the list
        // get a random number between 1 and 4 included
        int rdm = Random.Range(1, 2);
        for (int i = 0; i < rdm; i++){
            Destroy(cubes[Random.Range(0, cubes.Count-1)]);
        } 
        
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
        upLevel();
        upLevel();
        upLevel();
    }




}
