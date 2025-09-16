using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public static Move instance;
    public float acceleration = 1f;   // gia toc
    public float maxSpeed = 30f;
    private float currentSpeed = 0f; // toc do hien tai
    private float progress = 0f;
    public GameObject Robot;
    CharacterController character;

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
        if (Input.GetKey(KeyCode.W))
        {
            if (progress == 1 && keyNumber != 1) reset();

            progress += acceleration * Time.deltaTime;
            progress = Mathf.Clamp01(progress);

            currentSpeed = maxSpeed * progress;

            character.Move(Robot.transform.forward.normalized * currentSpeed * Time.deltaTime);

            keyNumber = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (progress == 1 && keyNumber != 2) reset();

            progress += acceleration * Time.deltaTime;
            progress = Mathf.Clamp01(progress);

            currentSpeed = maxSpeed * progress;

            character.Move(Robot.transform.forward.normalized * -currentSpeed * Time.deltaTime);

            keyNumber = 2;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (progress == 1 && keyNumber != 3) reset();

            progress += acceleration * Time.deltaTime;
            progress = Mathf.Clamp01(progress);

            currentSpeed = maxSpeed * progress;

            character.Move(Robot.transform.right.normalized * -currentSpeed * Time.deltaTime);

            keyNumber = 3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (progress == 1 && keyNumber != 4) reset();

            progress += acceleration * Time.deltaTime;
            progress = Mathf.Clamp01(progress);

            currentSpeed = maxSpeed * progress;

            character.Move(Robot.transform.right.normalized * currentSpeed * Time.deltaTime);

            keyNumber = 4;
        }
        else
        {
            keyNumber = 0;
            currentSpeed = 0;
            progress = 0;
        }
    }

    private void reset()
    {
        keyNumber = 0;
        currentSpeed = 0;
        progress = 0;
    }
}
