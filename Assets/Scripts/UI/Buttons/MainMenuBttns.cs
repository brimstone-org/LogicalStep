using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBttns : MonoBehaviour {

    public MainMenuManager mainMenu;

    public void ButtonPressed(string functionName)
    {
        mainMenu.SendMessage(functionName);
    }
}
