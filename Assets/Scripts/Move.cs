using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    [NotNull]
    public ViveController left;

    [NotNull]
    public ViveController right;

    [NotNull]
    public Transform eye;

    public float forwardSpeed;

    public float downwardSpeed;

    public float turnSpeed;

    public float strafeSpeed;

    public bool turn;

    public float RotateRat;

    void Start () {
		
	}
	
	void Update () {
        float diff = left.transform.position.y - right.transform.position.y;
        Debug.Log("diff " + diff);

        transform.Translate(Vector3.down * Time.deltaTime * downwardSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        if (turn)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + (Time.deltaTime * turnSpeed * diff),  diff * RotateRat));
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed * diff);
        }

    }
}
