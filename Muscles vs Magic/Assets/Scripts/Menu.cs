using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartLevel(string Level){
     SceneManager.LoadScene(Level);
    }

    public void MenuLevel(string Level){
     SceneManager.LoadScene(Level);
    }

   public void Closed(){
    Application.Quit();
   }
}
