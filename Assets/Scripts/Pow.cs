using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pow : MonoBehaviour
{
    public int pv;
    public TextMeshProUGUI text; 
    public GameObject effectObject;


    // ID of the pow
    // 1 = double les balles qui rentrent en collision
    // 2 = laser horizontal qui fait 1 dégat a tout les blocks sur la même ligne 
    // 3 = laser vertical qui fait 1 dégat a tout les blocks sur la même colonne
    public int id;






    public void setText(){
        text.text = pv.ToString();
    }

    public void TakeDamage(int damage)
    {
        pv -= damage;
        // set the text to the current health
        setText();
        if (pv <= 0)
        {
            Destroy(gameObject, 0.1f);
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        setText();
        if (other.gameObject.tag == "bullet"){
            if (id == 1){
                TakeDamage(1);
                if (other.GetComponent<Bullet>().cloned == false){
                    // check if the ball go up
                    Vector3 spawnPosition;
                    bool goUp = other.GetComponent<Rigidbody2D>().velocity.y > 0;
                    spawnPosition = transform.position;
                    // if (goUp){
                    //     // add 1 ball to the player
                    //     spawnPosition = new Vector3(transform.position.x, transform.position.y+0.75f, transform.position.z);
                    // } else {
                    //     spawnPosition = new Vector3(transform.position.x, transform.position.y-0.75f, transform.position.z);
                    // }

                    GameObject newObject = Instantiate(other.gameObject, spawnPosition , other.transform.rotation);
                    newObject.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity;
                    newObject.GetComponent<Bullet>().cloned = true;
                }
            }



            else if (id == 2){
                TakeDamage(1);
                // get the block manager
                GameObject cubeList = GameObject.Find("CubeList");
                // meke the effect
                StartCoroutine(Effect2());

                // loop objects into the cube list
                foreach (Transform child in cubeList.transform)
                {
                    // if the child is on the same line && child has a tag block
                    if (child.position.y == transform.position.y && child.gameObject.tag == "block"){
                        // destroy the child
                        child.GetComponent<Block>().TakeDamage(1);
                    }
                }
            }

            else if (id == 3){
                TakeDamage(1);
                // get the block manager
                GameObject cubeList = GameObject.Find("CubeList");
                // meke the effect
                StartCoroutine(Effect2());

                // loop objects into the cube list
                foreach (Transform child in cubeList.transform)
                {
                    // if the child is on the same line && child has a tag block
                    if (child.position.x == transform.position.x && child.gameObject.tag == "block"){
                        // destroy the child
                        child.GetComponent<Block>().TakeDamage(1);
                    }
                }
            }


        }
    }


    IEnumerator Effect2 (){
        // create the effect
        effectObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        effectObject.SetActive(false);

    }

}