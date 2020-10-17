using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidsManager : GeneralGameManager{
    
    public List<GameObject> spawnPoints;
    public List<GameObject> endPoints;
    public List<GameObject> blueAsteroids;
    public List<GameObject> greenAsteroids;
    public List<GameObject> yellowAsteroids;
    public List<GameObject> violetAsteroids;
    public GameObject laserPrefab;
    Asteroid currentAsteroid;
    bool asteroidOnScreen = false;
    public bool laserOnScreen = false;
    int asteroidsNumber = 0;
    public int currentAsteroidsNumber = 0;
    float asteroidsSpeed = 0;
    public static AsteroidsManager instance;
    public Text asteroidsCount;
    public Image lifeSlider;
    public Animator dangerScreen;
    public float lifes = 3;
    int pointPos = 0;
    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        levelName = "Asteroides";
    }

    void Start() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Gameplay/LevelMusic/play_level_music_full");
        switch(GameManager.instance.episode){
            case 0:
                asteroidsNumber = 5;
                asteroidsSpeed = 30;
                lifeSlider.transform.parent.gameObject.SetActive(false);
            break;
            case 1:
                asteroidsNumber = 8;
                asteroidsSpeed = 25;
            break;
            case 2:
                asteroidsNumber = 10;
                asteroidsSpeed = 20f;
            break;
        }
        asteroidsCount.text = "x " + (asteroidsNumber - currentAsteroidsNumber);
        lifeSlider.fillAmount = 1;
    }

    public void UpdateCount(){
        currentAsteroidsNumber++;
        asteroidsCount.text = "x " + (asteroidsNumber - currentAsteroidsNumber);
        asteroidOnScreen = false;
        if(currentAsteroidsNumber >= asteroidsNumber){
            StartCoroutine(EndTimer()); 
        }
    }

    public void UpdateLifes(){
        lifes --;
        lifeSlider.fillAmount -= 0.333f;
            FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/gp_nivel1/hitByAsteroid");
        if (lifes == 0){
            FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/gp_nivel1/hitByAsteroid_gameOver");
            TransitionManager.instance.LoadSceneWithTransition("MiniGame_0");
        }
    }
    
    public override void Update() {
        base.Update();
        if(!asteroidOnScreen && currentAsteroidsNumber < asteroidsNumber && lifes > 0){
            pointPos = Random.Range(0, spawnPoints.Count);
            switch(pointPos){
                case 0:
                    currentAsteroid = Instantiate(blueAsteroids[Random.Range(0, GameManager.instance.episode + 1)], spawnPoints[pointPos].transform.position, Quaternion.identity).GetComponent<Asteroid>();
                break;
                case 1:
                    currentAsteroid = Instantiate(greenAsteroids[Random.Range(0, GameManager.instance.episode + 1)], spawnPoints[pointPos].transform.position, Quaternion.identity).GetComponent<Asteroid>();
                break;
                case 2:
                    currentAsteroid = Instantiate(yellowAsteroids[Random.Range(0, GameManager.instance.episode + 1)], spawnPoints[pointPos].transform.position, Quaternion.identity).GetComponent<Asteroid>();
                break;
                case 3:
                    currentAsteroid = Instantiate(violetAsteroids[Random.Range(0, GameManager.instance.episode + 1)], spawnPoints[pointPos].transform.position, Quaternion.identity).GetComponent<Asteroid>();
                break;
            }
            currentAsteroid.Ininitialize(endPoints[pointPos].transform.position, asteroidsSpeed);
            asteroidOnScreen = true;
            canGetInfo = true;
        }else{
            if (Input.anyKey && currentAsteroidsNumber >= asteroidsNumber) {
                //NextMiniGame();
            }
        }
    }

    public void ShootLaser(int pos){
        switch(pos){
            case 0:
                buttonPressed = "blue";
            break;
            case 1:
                buttonPressed = "green";
            break;
            case 2:
                buttonPressed = "yellow";
            break;
            case 3:
                buttonPressed = "violet";
            break;
        }
        if(!laserOnScreen  && lifes > 0 && currentAsteroidsNumber < asteroidsNumber && pos == pointPos){
            SendInfoPiece(true);
            Laser currentLaser = Instantiate(laserPrefab, endPoints[pos].transform.position, Quaternion.identity).GetComponent<Laser>();
            currentLaser.Ininitialize(spawnPoints[pos].transform.position, pos);
            laserOnScreen = true;
        }else
            SendInfoPiece(false);
    }

    public void CheckCongrats(){

        if(currentAsteroidsNumber > 1 && currentAsteroidsNumber < asteroidsNumber){
            int prob = Random.Range(0,101);
            if(prob <= 40 || !playedFirstCongratulationsAudio)
                SendCongratulations();
        }
    }

    public override void SendCongratulations()
    {
        base.SendCongratulations();
        FMODUnity.RuntimeManager.PlayOneShot("event:/miniGames/Nivel1_Asteroids/steve_vox_motiv_mg_1");
        Debug.Log("FMOD: STEVE_CONGRATS_0");
    }

    public void NextMiniGame() {      //se reinicia el juego con la transición personalizada
        StopAllCoroutines();
        TransitionManager.instance.LoadSceneWithTransition("EndCinematic_MiniGame_0");
    }

    IEnumerator EndTimer(){      //coroutine de fin de juego
        // endedGameObj.SetActive(true);
        PlayWinnerSound();
        yield return new WaitForSeconds(5f);
        TransitionManager.instance.LoadSceneWithTransition("EndCinematic_MiniGame_0");
    }

    void PlayWinnerSound() {
        switch (GameManager.instance.episode) {
            case 0:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gameplay/LevelMusic/stop_level_music_1");
                break;
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gameplay/LevelMusic/stop_level_music_2");
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gameplay/LevelMusic/stop_level_music_3");
                break;
        }
    }
}