using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DragonBreath : MonoBehaviour {

    float timeToBreathe;
    AudioSource breathSound;

    private void Start()
    {
        timeToBreathe = 5f;
        breathSound = GetComponent<AudioSource>();
    }
    
    private void Update () {
		if ((timeToBreathe -= Time.deltaTime) < 0f)
        {
            breathSound.Play();
            timeToBreathe = Random.Range(2f, 7f);
        }
	}
}
