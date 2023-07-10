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
    // 4 = laser vertical et horizontal qui fait 1 dégat a tout les blocks sur la même colonne et ligne
    public int id;






    public void setText(){
        if (pv <= 0){
            pv = 0;
        }
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


    public void setColor(string hex_color){
        GetComponent<SpriteRenderer>().color = ColorUtility.TryParseHtmlString(hex_color, out Color color) ? color : Color.white;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        setText();
        if (other.gameObject.tag == "bullet" && pv > 0){
            if (id == 1){
                TakeDamage(1);
                if (other.GetComponent<Bullet>().cloned == false){
                    other.GetComponent<Bullet>().cloned = true;
                    GameObject newObject = Instantiate(other.gameObject, transform.position , other.transform.rotation);
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

            else if (id == 4){
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

                        child.GetComponent<Block>().TakeDamage(1);
                    }
                    if (child.position.x == transform.position.x && child.gameObject.tag == "block"){

                        child.GetComponent<Block>().TakeDamage(1);
                    }
                }
            }

            else if (id == 5){
                // bomb that destroy all blocks around
                TakeDamage(1);
                // get the block manager
                GameObject cubeList = GameObject.Find("CubeList");
                // meke the effect
                StartCoroutine(Effect2());
                // loop objects into the cube list 
                foreach (Transform child in cubeList.transform)
                {
                    // if the child et a distance de 1
                    if (Vector3.Distance(child.position, transform.position) <= 1 && child.gameObject.tag == "block"){
                        // destroy the child
                        child.GetComponent<Block>().TakeDamage(2);
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
