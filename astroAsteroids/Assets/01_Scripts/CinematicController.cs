using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour {

    public string sceneToLoad = "";
    public List<GameObject> cinematicPiece;
    int index = -1;
    Animator currentCinematicAnim;
    public float timer = 0.0f;
    public float waitTime = 10f;
    public bool loaded = false;
    public bool cinematicEnded = false;
    public bool isIntroCinematic = false;

    void Start() {
        //AudioManager.StopMusic2();
        activator();
    }

    void Update() {
        if(currentCinematicAnim != null){
            if(timer < currentCinematicAnim.GetCurrentAnimatorStateInfo(0).length + waitTime && !cinematicEnded){
                timer += Time.deltaTime;
                if(timer >= currentCinematicAnim.GetCurrentAnimatorStateInfo(0).length){
                    activator();
                }
            }
            else if(!cinematicEnded){
                if(index >= cinematicPiece.Count){
                    cinematicEnded = true;
                    NextMiniGame();
                }
                else{
                    activator();
                }
            }
        }
    }

    void activator(){
        timer = 0;
        index++;
        if(index < cinematicPiece.Count){
            currentCinematicAnim = cinematicPiece[index].GetComponent<Animator>();
            for (int i = 0; i < cinematicPiece.Count; i++) {
                if(i == index)
                    cinematicPiece[i].SetActive(true);
                else
                    cinematicPiece[i].SetActive(false);
            }
        }else if (!loaded && !cinematicEnded) {
            cinematicEnded = true;
            NextMiniGame();
        }
    }

    public void NextMiniGame() {      //se reinicia el juego con la transición personalizada
        StopAllCoroutines();
        if(GameManager.instance.gameMode == 0 || isIntroCinematic)
            TransitionManager.instance.LoadSceneWithTransition(sceneToLoad);
        else
            TransitionManager.instance.LoadSceneWithTransition("MainMenu");
    }
}