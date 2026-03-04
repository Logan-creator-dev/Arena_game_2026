using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
  private bool isPaused;
  public GameObject pausePanel;

  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (isPaused)
        ResumeGame();
      else
        PauseGame();
     
    }
  }
  public void PauseGame()
  {
    Time.timeScale = 0;
    pausePanel.SetActive(true);
    isPaused = true;
  }

  public void ResumeGame()
  {
    Time.timeScale = 1;
    pausePanel.SetActive(false);
    isPaused = false;
  }
}
