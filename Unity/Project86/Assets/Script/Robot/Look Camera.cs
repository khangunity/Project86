using Unity.Mathematics;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    public static LookCamera instance;
    public GameObject robot;
    public GameObject cam;
    public float speedRotate;

    public bool isLook = true;
    public bool isLooking = false;
    public float direction = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLook)
        {
            Vector3 camForwad = cam.transform.forward;
            camForwad.y = 0f;
            camForwad = camForwad.normalized;

            Vector3 robotForwad = robot.transform.forward;
            robotForwad.y = 0f;
            robotForwad = robotForwad.normalized;

            Vector3 cross = Vector3.Cross(camForwad, robotForwad);

            float angle = Vector3.Angle(camForwad, robotForwad);

            if (cross.y < 0) angle = -angle;

            if (angle != 0)
            {
                Quaternion camRotate = cam.transform.rotation;
                camRotate.z = 0f;
                camRotate.x = 0f;

                robot.transform.rotation = Quaternion.Slerp(robot.transform.rotation, camRotate, speedRotate * Time.deltaTime);

                isLooking = true;
                direction = angle;
            }
            else
            {
                isLooking = false;
                direction = 0f;
            }
        }
        else
        {
            isLooking = false;
            direction = 0f;
        }
    }
}
