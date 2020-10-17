using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    
    public float lerpTime = 5;
    float currentLerpTime = 0;
    Vector3 startPos, startScale;
    Vector3 endPos, endScale;
    bool arrived = false;
    bool initilized = false;
    public GameObject explosionEffect;
    public static int laserCounter;
    public AudioClip soundEffect;

    private void Update() {
        if(initilized){
            if(currentLerpTime <= lerpTime && !arrived){
                currentLerpTime += Time.deltaTime; 
                float perc = currentLerpTime / lerpTime;
                transform.position = Vector3.Lerp(startPos, endPos, perc);
                transform.localScale = Vector3.Lerp(startScale, endScale, perc);
            }else if(currentLerpTime > lerpTime && !arrived){
                Arrive();
            }
        }
    }

    public void Ininitialize(Vector3 endPos, int pointPos){
        startScale = new Vector3(0.5f, 0.5f, 1f);
        transform.localScale = startScale;
        endScale = new Vector3(0.1f, 0.1f, 0.1f);
        startPos = new Vector3(transform.position.x, transform.position.y, 0);
        this.endPos = endPos;
        switch(pointPos){
            case 0:
                transform.Rotate(0,0,-50);
                FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/Lasers/LaserBlast_L");
                laserCounter = 0;
            break;
            case 1:
                transform.Rotate(0,0,-20);
                FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/Lasers/LaserBlast_C");
                laserCounter = 1;
            break;
            case 2:
                transform.Rotate(0,0,20);
                FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/Lasers/LaserBlast_C");

                laserCounter = 2;
            break;
            case 3:
                transform.Rotate(0,0,50);
                FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/Lasers/LaserBlast_R");
                laserCounter = 3;
            break;
        }
        initilized = true;
    }

    void Arrive(){
        arrived = true;
        Destroyer(true);
    }

    public void Destroyer(bool state){
        if(state){
            AsteroidsManager.instance.laserOnScreen = false;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Instantiate(explosionEffect,transform.position,Quaternion.identity);
        // Debug.Log("entro al trigger2");
        if(other.gameObject.tag == "Asteroid")
            Arrive();
    }
}