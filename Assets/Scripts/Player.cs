using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using RDG;

public class Player : MonoBehaviour
{
    public int bulletCount;
    public float speed = 0.1f;
    public GameObject bullet;

    public Manager Manager;

    public TextMeshProUGUI uiBallCount; 

    private float rotation = -19;
    private bool rotateToRight = true;
    private float maxRotation = 50;

    // particle effect
    public GameObject shootParticle;


    private int beforeLevel = 2;
    private CamEffect camEffect;
    private Transform cannon;

    // Start is called before the first frame update
    void Awake(){
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
        setUI();
        cannon = transform.Find("cannon");
        camEffect = GameObject.Find("Main Camera").GetComponent<CamEffect>();
        shootParticle.SetActive(false);
    }

    public void addBullet(int i = 1){
        bulletCount+=i;
        setUI();
    }


    public void setUI(){
        uiBallCount.text = bulletCount.ToString();
    }

    void shoot(){
        if (bulletCount > 0 && Time.timeScale != 0){

                Vector3 bulletPosition = transform.position;
                // make the bullet spawn a bit in front of the player
                // calculate the direction of the player and add it to the position
                bulletPosition -= transform.up * 0.75f ;
                // instantiate the bullet


                // GameObject projectile = Instantiate(bullet, bulletPosition, transform.rotation);
                GameObject projectile = Instantiate(bullet, bulletPosition, transform.rotation);
                Destroy(projectile, 30f);
                
                // set the tag of the bullet to "bullet"
                projectile.tag = "bullet";
                addBullet(-1);
                // add a force to the bullet towards the direction of the player
                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.AddForce(-transform.up * 12.5f, ForceMode2D.Impulse);
                // make the cannon move backwars and forwards
                StartCoroutine("cannon_shoot_effect");
            }
    }


    IEnumerator cannon_shoot_effect(){

        if (PlayerPrefs.GetInt("vibration") == 1){
            Vibration.Vibrate(100, 50);
        }

        camEffect.Shake();
        shootParticle.SetActive(true);
        shootParticle.GetComponent<ParticleSystem>().Play();

        // loop 4 times 
        for (int i = 0; i < 4; i++)
        {
            cannon.localPosition += new Vector3(0, 0.075f, 0);
            yield return new WaitForSeconds(0.0025f);
        }
        // loop 4 times 
        for (int i = 0; i < 4; i++)
        {
            cannon.localPosition -= new Vector3(0, 0.075f, 0);
            yield return new WaitForSeconds(0.01f);
        }


    }


    // Update is called once per frame
    void Update()
    {

        // rotate the player continuously around the z axis
        if (rotateToRight)
        {
            rotation += speed * Time.deltaTime;
            if (rotation >= maxRotation)
            {
                rotateToRight = false;
                beforeLevel --;
            }
        }
        else
        {
            rotation -= speed * Time.deltaTime;
            if (rotation <= -maxRotation)
            {
                rotateToRight = true;
            }
        }
        transform.rotation = Quaternion.Euler(0, 0, rotation);


        // pour le passage de niveau
        if (beforeLevel == 0)
        {
            Manager.upLevel();
            beforeLevel = 2;
            addBullet(Manager.level/10);
        }


        // if right click
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }


        // shoot a bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }


        // on press enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Manager.upLevel();
        }
        
        // on press return andoid
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("home");
        }
        
    }
}
