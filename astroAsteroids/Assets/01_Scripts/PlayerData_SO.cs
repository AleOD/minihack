using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData_SO : ScriptableObject {
    
    public List<UserDataPiece> userDataPieces;
    public void AddDataPiece(float time, string currentLevel, string buttonPressed, bool correct, int inputsCount) {
        UserDataPiece dataPiece = new UserDataPiece();
        dataPiece.inputTime = time;
        dataPiece.level = currentLevel;
        dataPiece.buttonPressed = buttonPressed;
        dataPiece.date = System.DateTime.Now.ToString();
        dataPiece.correct = (correct ? 1 : 0);
        dataPiece.n = inputsCount;
        dataPiece.userName = GameManager.instance.userName;
        dataPiece.SetEpisode(GameManager.instance.episode);
        userDataPieces.Add(dataPiece);
        GameSaveManager.instance.SaveGame();
        CSVManager.AppendToReport(ConvertDataPieceToStringArray(dataPiece));
    }

    public string[] ConvertDataPieceToStringArray(UserDataPiece dataPiece){
        string[] auxArray = new string[8];
        auxArray[0] = dataPiece.userName;
        auxArray[1] = dataPiece.correct.ToString();
        auxArray[2] = dataPiece.inputTime.ToString();
        auxArray[3] = dataPiece.level;
        auxArray[4] = dataPiece.n.ToString();
        auxArray[5] = dataPiece.buttonPressed;
        auxArray[6] = dataPiece.episode;
        auxArray[7] = dataPiece.date;
        return auxArray;
    }
}

[System.Serializable]
public struct UserDataPiece{

    public string date;
    public string userName;//usuario
    public float inputTime;//tiempo de interacción
    public string level;//mini juego
    public string buttonPressed;//color presionado
    public string episode;//dificultad
    public int correct; //informacion sobre si la pulsacion fue o no correcta
    public int n; //numero de interaccion

    public void SetEpisode(int value){
        switch(value){
            case 0:
                episode = "facil";
            break;
            case 1:
                episode = "medio";
            break;
            case 2:
                episode = "dificil";
            break;
        }
    }
}