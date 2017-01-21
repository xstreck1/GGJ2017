﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class ViveController : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    static private bool menuPressed = false;
    static public bool MenuPressed { private set { menuPressed = value; } get { return menuPressed; } }

    public bool left;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
        {
            MenuPressed = true;
        }

        if (Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
        {
            MenuPressed = false;
        }
    }
}