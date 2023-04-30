using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int pv;
    public TextMeshProUGUI text; 
    // list of colors hex values
    
    public Player player; // player

    void Awake(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    public void setColor(string hex_color){
        GetComponent<SpriteRenderer>().color = ColorUtility.TryParseHtmlString(hex_color, out Color color) ? color : Color.white;
    }

    public void setText(){
        text.text = pv.ToString();
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(anime());
        pv -= damage;
        // set the text to the current health
        setText();
        if (pv <= 0)
        {
            Destroy(gameObject);
            // add 1 ball to the player
            player.addBullet();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {   
            TakeDamage(1);
        }
    }


    // annimation 
    IEnumerator anime(){
        Transform square = transform.GetChild(0);
        // grandir le scale
        // loop 4 times
        for (int i = 0; i < 5; i++){
            // grandir le scale
            // square.localScale += new Vector3(0.1f, 0.1f, 0);
            square.localScale += new Vector3(0.01f, 0.01f, 0);
            // attendre 0.1s
            yield return new WaitForSeconds(0.01f);
        }
        // attendre 0.1s
        yield return new WaitForSeconds(0.01f);
        square.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }

}
