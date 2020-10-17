using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDataUserUI : MonoBehaviour {

    public Text user;
    public Text correctBtn;
    public Text inputTime;
    public Text level;
    public Text interactionCount;
    public Text buttonpressed;
    public Text episode;
    public Text date;

    public void SetInfo(string user, string inputTime, string level, string buttonpressed, string episode, string date, string correct, string n){
        this.user.text = user;
        this.inputTime.text = inputTime;
        this.level.text = level;
        this.buttonpressed.text = buttonpressed;
        this.episode.text = episode;
        this.date.text = date;
        this.correctBtn.text = correct;
        this.interactionCount.text = n;
    }
}