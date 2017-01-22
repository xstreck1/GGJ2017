using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour {

    [NotNull]
    public ViveController left;

    [NotNull]
    public ViveController right;

    [NotNull]
    public Transform eye;

    [NotNull]
    public DragonFollow dragon;

    public float forwardSpeed;

    public float downwardSpeed;

    public float turnSpeed;

    public float strafeSpeed;

    public bool turn;

    public float RotateRat;

    public float waveRiseRatio;

	
	void Update () {
        if (left.MenuPressed && right.MenuPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (dragon.CanMove)
        {
            float diff = left.transform.position.y - right.transform.position.y;

            transform.Translate(Vector3.down * Time.deltaTime * downwardSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
            if (turn)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * diff);
                // transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + (Time.deltaTime * turnSpeed * diff),  diff * RotateRat));
                dragon.transform.localRotation = Quaternion.Euler(diff * RotateRat * Vector3.forward);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed * diff);
            }

            // Debug.Log("Rise " + Mathf.Max(left.HeightDiff, right.HeightDiff));

            // Wave down
            if (left.HeightDiff > 0f && right.HeightDiff > 0f)
            {
                float rise = Mathf.Min(left.HeightDiff, right.HeightDiff);
                transform.Translate(Vector3.up * waveRiseRatio * rise);
            }
        }
    }
}
