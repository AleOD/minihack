using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour{
    
    float lerpTime = 5;
    float currentLerpTime = 0;
    Vector3 startPos, startScale;
    Vector3 endPos, endScale;
    bool arrived = false;
    bool initilized = false;
    public GameObject explosionEffect;
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

    public void Ininitialize(Vector3 endPos, float lerpTime){
        startScale = new Vector3(0.01f, 0.01f, 0.01f);
        transform.localScale = startScale;
        endScale = new Vector3(0.7f, 0.7f, 0.7f);
        startPos = new Vector3(transform.position.x, transform.position.y, 0);
        this.endPos = endPos;
        this.lerpTime = lerpTime;
        initilized = true;
    }

    void Arrive(){
        AsteroidsManager.instance.dangerScreen.SetTrigger("On");
        switch(GameManager.instance.episode){
            case 0:
                Destroyer(false);
            break;
            case 1:
                Destroyer(true);
                AsteroidsManager.instance.UpdateLifes();
            break;
            case 2:
                Destroyer(true);
                AsteroidsManager.instance.UpdateLifes();
            break;
        }
    }

    public void Destroyer(bool state){
        Instantiate(explosionEffect,transform.position,Quaternion.identity);
        if(state){
            AsteroidsManager.instance.UpdateCount();
            Destroy(this.gameObject);
            AsteroidsManager.instance.AddPoints();
            AsteroidsManager.instance.CheckCongrats();
            switch (Laser.laserCounter) { 
                    case 0:
                    FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/AsteroidExpl/AsteroidExpl_L");
                    break;
                    case 1:
                    FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/AsteroidExpl/AsteroidExpl_C");
                    break; 
                    case 2:
                    FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/AsteroidExpl/AsteroidExpl_C");
                    break;
                    case 3:
                    FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/lasersAndAsteroids/AsteroidExpl/AsteroidExpl_R");
                    break;
            }
        }
        arrived = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("entro al trigger");
        if(other.gameObject.tag == "Laser"){
            Destroyer(true);
        }
    }
}