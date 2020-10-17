using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

    private void Update() {
        if(Input.anyKey)
            LoadMainMenu();
    }

    public void LoadMainMenu(){
        TransitionManager.instance.LoadSceneWithTransition("MainMenu");
    }
}