using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class ViveController : MonoBehaviour
{
    [NotNull]
    public AudioSource shortFlap;

    [NotNull]
    public AudioSource longFlap;

    SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    public bool MenuPressed { private set; get; }

    public bool left;

    public Vector3 LastPost { get; private set; }

    public float HeightDiff { get; private set; }

    public float cummulativeHeight = 0f;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        LastPost = transform.position;
        HeightDiff = 0f;
        MenuPressed = false;
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

        HeightDiff = LastPost.y - transform.localPosition.y;
        LastPost = transform.localPosition;

        if (HeightDiff > 0f)
        {
            cummulativeHeight += HeightDiff;
        }
        else if (cummulativeHeight > 0.7f)
        {
            longFlap.Play();
            cummulativeHeight = 0f;
        }
        else if (cummulativeHeight > 0.35f)
        {
            shortFlap.Play();
            cummulativeHeight = 0f;
        }
    }
}