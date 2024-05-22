using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void Quit(){
        Application.Quit();        
    }
    
    public void StartGame(){
        SceneManager.LoadScene("History");
    }

    public void Skip(){
        SceneManager.LoadScene("maket");
    }

    public void Exit(){
        SceneManager.LoadScene("menu");
    }
}
