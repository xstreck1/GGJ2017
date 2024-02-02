using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ViveController : MonoBehaviour
{
    [NotNull]
    public AudioSource shortFlap;

    [NotNull]
    public AudioSource longFlap;

    public bool MenuPressed { get;  private set;}

    public bool left;

    public Vector3 LastPost { get; private set; }

    public float HeightDiff { get; private set; }

    public float cummulativeHeight = 0f;
    
    private Transform _transform;

    void Start()
    {
        _transform = transform;
        LastPost = _transform.position;
        HeightDiff = 0f;
        MenuPressed = false;
    }

    void Update()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        foreach (var device in devices)
        {
            var dirFlag = left ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right;
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller | dirFlag))
            {
                device.TryGetFeatureValue(CommonUsages.menuButton, out bool pressed);
                MenuPressed = pressed;
            }
        }

        var localPos = _transform.localPosition;
        HeightDiff = LastPost.y - localPos.y;
        LastPost = localPos;

        if (HeightDiff > 0f)
        {
            cummulativeHeight += HeightDiff;
        }
        else switch (cummulativeHeight)
        {
            case > 0.7f:
                longFlap.Play();
                cummulativeHeight = 0f;
                break;
            case > 0.35f:
                shortFlap.Play();
                cummulativeHeight = 0f;
                break;
        }
    }
}