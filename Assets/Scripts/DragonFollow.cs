using UnityEngine;
using System.Collections;

public class DragonFollow : MonoBehaviour
{
    public GameObject eye;
    public Vector3 correction;
    public float heightDiff;

    void Start()
    {
    }

    void Update()
    {
        transform.localPosition = eye.transform.localPosition;
        transform.localPosition += Vector3.down * eye.transform.localPosition.y * heightDiff;
        transform.Translate(correction);
    }
}