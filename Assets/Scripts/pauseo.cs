using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseo : MonoBehaviour
{
   public static bool isPaused = false;
   public GameObject pauseMenuUI;

   void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
   }

   void Update()
   {
     if (isPaused)
     {
        Cursor.lockState = CursorLockMode.None;
     }
     else Cursor.lockState = CursorLockMode.Locked;
     if (Input.GetKeyDown(KeyCode.Escape))
     {
        if(isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
     }
   }
   
   public void Resume()
   {
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
   }

   public void Pause()
   {
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
   }

   public void ResumeButton()
   {
      pauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      isPaused = false;
   }
}
