using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float restitution = 0.5f;
    private Player player;
    private float staticTime = 1;
    
    public bool cloned = false;
    
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update(){
        // if the ball is under the screen, destroy it
        if (transform.position.y < -50){
            Destroy(gameObject);
        }
        // if the ball is static for 3 seconds, destroy it
        if (rb.velocity.magnitude < 0.01f){
            if (staticTime > 0){
                staticTime -= Time.deltaTime;
            } else {
                Destroy(gameObject);
            }
        }
    }


    // bounce back when hitting something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si l'objet avec lequel on entre en collision a pour tag "map"
        if (rb != null){
            // if the object has the tad "Chaudron"
            if (collision.gameObject.tag == "Chaudron"){
                player.addBullet(2);
                Destroy(gameObject);
                return;
            }

            if (collision.gameObject.tag == "PowDouble"){
                return;
            }

                Vector2 relativeVelocity = collision.relativeVelocity;
                // Calculer la force de rebondissement en fonction de la vitesse relative et du coefficient de restitution
                float bounceForce = relativeVelocity.magnitude * restitution;
                // Calculer la direction de rebond basée sur la normale de la collision
                Vector2 direction = collision.contacts[0].normal;
                // Appliquer la force de rebondissement à l'objet dynamique
                rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        }
    }
}


