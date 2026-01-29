using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public static Move instance;

    [Header("Object/Controller")]
    public GameObject Robot;
    [SerializeField]CharacterController character;

    [Header("Parameter Move")]
    #region biến move
    public float acceleration = 0.5f;   // gia toc
    public float accelerationFast = 2f;
    public float maxFastSpeed = 90f;
    public float FastSpeed = 70f;
    public float maxSpeed = 40f;
    [SerializeField]private float currentSpeed = 0f; // toc do hien tai
    [SerializeField]private float progress = 0f;
    public bool isFast = false;
    public bool stopFast = false;
    public bool stop = false;
    #endregion

    public int keyNumber = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        character = Robot.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        #region nhả move key
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (keyNumber == 1)
            {
                setStopFunc();
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (keyNumber == 2)
            {
                setStopFunc();
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (keyNumber == 3)
            {
                setStopFunc();
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (keyNumber == 4)
            {
                setStopFunc();
            }
        }
        #endregion

        #region dí move key
        if (Input.GetKey(KeyCode.W))
        {
            moveFunc(1, 1, Robot.transform.forward.normalized);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveFunc(2, -1, Robot.transform.forward.normalized);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveFunc(3, 1, Robot.transform.right.normalized);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveFunc(4, -1, Robot.transform.right.normalized);
        }
        #endregion

        #region dí và nhả cuột phải
        if (Input.GetKey(KeyCode.Mouse1) && keyNumber != 0 && !stopFast && !Shoot.instance.Shooting)
        {

            if (keyNumber == 1)
            {
                moveFastFunc(1, 1, Robot.transform.forward.normalized);
            }
            else if (keyNumber == 2)
            {
                moveFastFunc(2, -1, Robot.transform.forward.normalized);
            }
            else if (keyNumber == 3)
            {
                moveFastFunc(3, 1, Robot.transform.right.normalized);
            }
            else if (keyNumber == 4)
            {
                moveFastFunc(4, -1, Robot.transform.right.normalized);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && keyNumber != 0)
        {
            isFast = false;
            stopFast = true;
            currentSpeed = 0;
        }
        #endregion
    }

    #region hàm move và fast
    private void moveFunc(int number, int direction, Vector3 transform)
    {
        if (!stop && !isFast && !stopFast)
        {
            if (progress > 0 && keyNumber == 0)
            {
                currentSpeed = 0;
                progress = 0;
            }

            if (keyNumber == 0)
            {
                keyNumber = number;
            }

            if (keyNumber == number)
            {
                progress += acceleration * Time.deltaTime;
                progress = Mathf.Clamp01(progress);

                currentSpeed = maxSpeed * progress;

                character.Move(transform * currentSpeed * Time.deltaTime * direction);
            }
        }
    }

    private void moveFastFunc(int number, int direction, Vector3 transform)
    {
        if (!isFast && !stopFast)
        {
            isFast = true;

            currentSpeed = 0;
            progress = 0;
        }

        if (!stop && !stopFast && isFast)
        {

            if (keyNumber == number)
            {
                progress += accelerationFast * Time.deltaTime;
                progress = Mathf.Clamp01(progress);

                float t = progress * progress * (3 - 2 * progress);

                currentSpeed = Mathf.Lerp(FastSpeed, maxFastSpeed, t);

                character.Move(transform * currentSpeed * Time.deltaTime * direction);
            }
        }
    }
    #endregion

    #region hàm stop
    private void setStopFunc()
    {
        if (stopFast || isFast) return;
        else
        {
            if (currentSpeed >= maxSpeed)
            {
                stop = true;
            }
            else
            {
                stop = false;
                keyNumber = 0;
            }
        }
        
    }
    #endregion
}
