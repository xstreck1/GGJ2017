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

    public bool CanMove { get { return !(timer < 0 || timer > totalTime || collided); } }

    const int pointInc = 50;


    bool collided;
    float timer;
    const float totalTime = 260f;

    int points = 0;

    void Start()
    {
        collided = false;
        timer = -5f;
        pickupSound = GetComponent<AudioSource>();
    }

    void Update()
    {


        transform.localPosition = eye.transform.localPosition;
        transform.localPosition += Vector3.down * eye.transform.localPosition.y * heightDiff;
        transform.Translate(correction);

        timer += Time.deltaTime;
        if (!CanMove)
        {
            if (timer < 0)
            {
                gui.text = "wave your hands in " + Convert.ToInt32(0f - timer);
            }
            else if (timer > totalTime)
            {
                gui.text = "final score " + points;
            }
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }
        else
        {
            gui.text = points.ToString();
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        collided = true;
        gui.text = "CRASHED";
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

            Debug.Log("Collided");
            collided = true;
            gui.text = "CRASHED";
        }
    }
}