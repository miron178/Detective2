using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;

    private bool paused = false;

    private GameManager()
    {

    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }

    public void Pause(bool pause)
    {
        paused = pause;
        Cursor.visible = pause;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = pause ? 0 : 1;
    }

    public bool IsPaused()
    {
        return paused;
    }
}
