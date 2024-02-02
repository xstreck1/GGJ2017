using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    [NotNull]
    public Transform leftWing;

    [NotNull]
    public Transform rightWing;

    [NotNull]
    public ViveController left;

    [NotNull]
    public ViveController right;

    [NotNull]
    public Transform eye;

    [NotNull]
    public DragonFollow dragon;

    float currentForwardSpeed;

    public float forwardSpeed;

    public float timeToAccelerate;

    public float downwardSpeed;

    public float turnSpeed;

    public float strafeSpeed;

    public bool turn;

    public float RotateRat;

    public float waveRiseRatio;

    private void Start()
    {
        currentForwardSpeed = 0f;
    }

    void Update()
    {
        if (dragon.Timer < 1f)
            return;

        if (left.MenuPressed && right.MenuPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Vector3 leftRotate = leftWing.localEulerAngles;
        leftRotate.y = 160 - ((left.transform.localPosition.y - 1.7f) * 60f);
        leftWing.localEulerAngles = leftRotate;


        Vector3 rightRotate = rightWing.localEulerAngles;
        rightRotate.y = ((right.transform.localPosition.y - 1.6f) * 60f);
        rightWing.localEulerAngles = rightRotate;

        if (!dragon.Collided)
        {
            if (currentForwardSpeed < forwardSpeed)
            {
                currentForwardSpeed += forwardSpeed * (Time.deltaTime / timeToAccelerate);
            }

            float diff = left.transform.position.y - right.transform.position.y;

            transform.Translate(Vector3.down * Time.deltaTime * downwardSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * currentForwardSpeed);
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
        }
        else
        {
            currentForwardSpeed = 0f;
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
