using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using State;

namespace View
{
    public enum MenuView
    {
        None = 0,
        MainMenu = 1,
        SettingsMenu = 2,
        AboutMenu = 3,
    }

    public enum PopupView
    {
        None = 0,
        LoadingBar = 1,
        Confirmation = 2,
    }
}