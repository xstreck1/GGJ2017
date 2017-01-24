using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DragonFollow : MonoBehaviour
{
    [NotNull]
    public Text gui;

    AudioSource pickupSound;

    public GameObject eye;
    public Vector3 correction;
    public float heightDiff;
    
    public bool Collided { get; private set; }

    const int pointInc = 50;

   
    public float Timer { get; private set; }
    const float totalTime = 260f;

    int points = 0;

    void Start()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    void Update()
    {

        transform.localPosition = eye.transform.localPosition;
        transform.localPosition += Vector3.down * eye.transform.localPosition.y * heightDiff;
        transform.Translate(correction);

        Timer += Time.deltaTime;
        if (Collided)
        {
            gui.text = "wave your hands";

            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            gui.text = points.ToString();
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        Collided = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        Collided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "loop")
        {
            points += pointInc;
            pickupSound.Play();
        }
        else
        {
            Debug.Log("Enter Trigger");
            Collided = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit Trigger");
        Collided = false;
    }
}