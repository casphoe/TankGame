using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : CCompoent
{
    public void QuitButton()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
