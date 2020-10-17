using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    public Animator transitionAnim;     //las animaciones para el cambio de escena, pueden ser bastante más animadas segun las animaciones que se preparen
    public static TransitionManager instance;    //la instancia para poder invocar de cualquier parte
    bool isLoading = false;

    private void Awake() {    //referencia de la instancia
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void LoadSceneWithTransition(){    //se comienza el cambio de escena con la animación
        if(!isLoading)
            StartCoroutine(LoadScene());
    }

    public void LoadSceneWithTransition(string name){    //se comienza el cambio de escena con la animación
        if(!isLoading)
            StartCoroutine(LoadScene(name));
    }

    IEnumerator LoadScene(){    //coroutine del cambio de escena
        isLoading = true;
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        isLoading = false;
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadScene(string name){    //coroutine del cambio de escena
        isLoading = true;
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        isLoading = false;
        SceneManager.LoadScene(name);
    }
}