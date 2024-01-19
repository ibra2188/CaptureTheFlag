using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_switcher : MonoBehaviour
{
  public void play()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
  }
  
  public void back()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
  }
}
