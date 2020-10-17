using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.InputSystem;
// using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour {

    public ScreenSwipe mainSS, episodeSS, miniGameSS;
    public GameObject episodes, miniGame;
    public Text infoItemTxt;
    //--------------extra ui---------------//
    public InfoDataUserUI infoDataUserUIPrefab;
    public GameObject allData;
    public GameObject userPanel;
    public Transform dataParent;
    public InputField userInputField;
    public Text userNameTxt;
    public static MainMenuManager instance;

    //FMOD

    //public FMOD.Studio.EventInstance mainMusic;

    void Awake() {
        // var inputEvent = InputSystem.CreateEvent < PointerMoveEvent > ( ) ;
        // inputEvent . deviceType = typeof ( Mouse ) ;
        // inputEvent . deviceIndex = 0 ;
        // inputEvent . delta = myMouseDelta ;
        // inputEvent . position = myMousePosition ;
        // InputSystem . QueueEvent ( inputEvent ) ;
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start(){
        userPanel.SetActive(false);
        allData.SetActive(false);

        StartCoroutine(PlayMusic());
        //mainMusic = FMODUnity.RuntimeManager.CreateInstance("event:/MainMenu/mainMenu_music/play_mm_music");


        episodes.SetActive(true);
        miniGame.SetActive(false);
    }

    public void Exit(){
        Application.Quit();
    }

    public void HistoryMode(){
        TransitionManager.instance.LoadSceneWithTransition("Cinematic_1");
    }

    public void SelectMode(int n){
        GameManager.instance.gameMode = n;
        FMODUnity.RuntimeManager.PlayOneShot("event:/MainMenu/UI/buttonAccept");
        mainSS.GoToScreen(n);
        miniGameSS.UpdateColoration();
        episodeSS.UpdateColoration();
    }

    public void NextEpisode(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/MainMenu/UI/buttonRArrow");
        if (episodes.activeSelf){
            if(episodeSS.CurrentScreen + 1 > episodeSS.ScreenCount - 1)
                episodeSS.GoToScreen(0);
            else
                episodeSS.GoToNextScreen();
            episodeSS.UpdateColoration();
        }else{
            if(miniGame.activeSelf){
                if(miniGameSS.CurrentScreen + 1 > miniGameSS.ScreenCount - 1)
                    miniGameSS.GoToScreen(0);
                else
                    miniGameSS.GoToNextScreen();
            }
            miniGameSS.UpdateColoration();
        }
    }

    public void PreviousEpisode(){

        FMODUnity.RuntimeManager.PlayOneShot("event:/MainMenu/UI/buttonLArrow");
       
        if (episodes.activeSelf){
            if(episodeSS.CurrentScreen - 1 < 0)
            episodeSS.GoToScreen(episodeSS.ScreenCount - 1);
            else
                episodeSS.GoToPreviousScreen();
            episodeSS.UpdateColoration();
        }else{
            if(miniGame.activeSelf){
                if(miniGameSS.CurrentScreen - 1 < 0)
                    miniGameSS.GoToScreen(miniGameSS.ScreenCount - 1);
                else
                    miniGameSS.GoToPreviousScreen();
            }
            miniGameSS.UpdateColoration();
        }
    }

    public void Next(){

        if(miniGame.activeSelf){
            //Stopping mainMenu music

            FMODUnity.RuntimeManager.PlayOneShot("event:/Gameplay/startGame");

            //mainMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //mainMusic.release();


            Debug.Log("FM Music stopped");

            GameManager.instance.episode = episodeSS.CurrentScreen;
            // GameManager.instance.miniGame = miniGameSS.CurrentScreen;
            // Debug.Log(GameManager.instance.episode);
            TransitionManager.instance.LoadSceneWithTransition("Cinematic_MiniGame_0");
        }
        else{
            FMODUnity.RuntimeManager.PlayOneShot("event:/MainMenu/UI/buttonAccept");
            episodes.SetActive(false);
            miniGame.SetActive(true);
            miniGameSS.UpdateColoration();
        }
    }

    public void Previous(){
         FMODUnity.RuntimeManager.PlayOneShot("event:/MainMenu/UI/buttonReturn");
            if (episodes.activeSelf){
            mainSS.GoToScreen(0);
        }
        else if(miniGame.activeSelf){
            episodes.SetActive(true);
            miniGame.SetActive(false);
            episodeSS.UpdateColoration();
        }
    }

    public void UserData(){
        userPanel.SetActive(true);
        allData.SetActive(false);
    }

    public void AllData(){
        userPanel.SetActive(false);
        allData.SetActive(true);
        // if(dataParent.childCount == 0){
            foreach (Transform child in dataParent) {
                Destroy(child.gameObject);
            }
            Instantiate(infoDataUserUIPrefab, Vector3.zero, Quaternion.identity, dataParent);
            foreach (var item in UserDataManager.instance.playerData.userDataPieces) {
                InfoDataUserUI aux = Instantiate(infoDataUserUIPrefab, Vector3.zero, Quaternion.identity, dataParent);
                aux.SetInfo(item.userName, item.inputTime.ToString(), item.level, item.buttonPressed, item.episode, item.date,item.correct.ToString(),item.n.ToString());
            }
        // }
    }

    public void ExitData(){
        userPanel.SetActive(false);
        allData.SetActive(false);
    }

    public void ChangeUserName(){
        if(userInputField.text != null){
            GameManager.instance.userName = userInputField.text;
            userNameTxt.text = GameManager.instance.userName;
        }
    }

    public void ResetAllData(){
        foreach (Transform child in dataParent) {
            Destroy(child.gameObject);
        }
        UserDataManager.instance.ResetAllData();
        // Instantiate(infoDataUserUIPrefab, Vector3.zero, Quaternion.identity, dataParent);
    }

    private IEnumerator PlayMusic(){
        yield return new WaitForSeconds(0.7f);
        FMODUnity.RuntimeManager.PlayOneShot("event:/MainMenu/mainMenu_music/play_mm_music");
        Debug.Log("FM: Music MainMenu started");
        //mainMusic.start();
    }

}
