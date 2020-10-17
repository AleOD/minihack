using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    [Header ("Nombre del usuario activo")]
    public string userName = "player";
    [Header ("Dificultad del juego")]
    public int episode = 0; //dificultad 0 = facil ; 1 = normal; 2 = dificil
    [Header ("Mini juego")]
    public int miniGame = 0; //mini juego 0 = asteroides ; 1 = estrellas; 2 = limpieza; 3 = Restaurante
    public int gameMode = 0; //modo de juego 0 = historia ; 1 = seleccion;
    public static GameManager instance;

    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
    }

}