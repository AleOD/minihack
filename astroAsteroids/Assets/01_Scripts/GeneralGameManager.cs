using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralGameManager : MonoBehaviour {

    //data info
    [HideInInspector] public float inputInfoTimer = 0;
    [HideInInspector] public bool canGetInfo = false;
    [HideInInspector] public int inputsCount = 0;
    [HideInInspector] public string levelName;
    [HideInInspector] public string buttonPressed;
    public Text scoreTxt;
    public Transform scoreParent;
    // public GameObjectTimeDestroyer scorePrefab;
    int score = 0;
    [HideInInspector] public bool playedHelpAudio = false;
    [HideInInspector] public bool playedFirstCongratulationsAudio = false;
    // public GameObject endedGameObj;

    public virtual void Update() { 
        if(canGetInfo){
            inputInfoTimer += Time.deltaTime;
            if(inputInfoTimer > 10 && !playedHelpAudio)
                SendHelp();
            if(inputInfoTimer < 10 && playedHelpAudio)
                playedHelpAudio = false;
        }
    }

    public void AddPoints(){
        int aux;
        if(inputInfoTimer <= 5)
            aux = 100;
        else if(inputInfoTimer <= 10)
            aux = 70;
        else if(inputInfoTimer <= 15)
            aux = 40;
        else
            aux = 20;
        // GameObjectTimeDestroyer scoreAux = Instantiate(scorePrefab, scoreParent.transform.position, Quaternion.identity, scoreParent);
        // scoreAux.Initialize(aux);
        score += aux;
        scoreTxt.text = "Puntaje: " + score;
    }

    public void SendInfoPiece(bool correct){
        inputsCount ++;
        if(scoreTxt != null && correct){
            // int aux;
            // if(inputInfoTimer <= 5)
            //     aux = 100;
            // else if(inputInfoTimer <= 10)
            //     aux = 70;
            // else if(inputInfoTimer <= 15)
            //     aux = 40;
            // else
            //     aux = 20;
            // GameObjectTimeDestroyer scoreAux = Instantiate(scorePrefab, scoreParent.transform.position, Quaternion.identity, scoreParent);
            // scoreAux.Initialize(aux);
            // score += aux;
            // scoreTxt.text = "Puntaje: " + score;
        }
        canGetInfo = false;
        UserDataManager.instance.AddDataPiece(inputInfoTimer, levelName, buttonPressed, correct, inputsCount);
        if(correct)
            inputInfoTimer = 0;
        canGetInfo = true;
    }

    public void SendHelp(){
        playedHelpAudio = true;
        Debug.Log("FMOD: [missing] STEVE_HELP");
        //reproducir aqui uno de los audios de ayuda como el de Astro pulsa el un boton para continuar o algo asi
    }

    public virtual void SendCongratulations(){
        playedFirstCongratulationsAudio = true;  

    }

    
}
