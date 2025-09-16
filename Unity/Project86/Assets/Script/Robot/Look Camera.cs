using Unity.Mathematics;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    public GameObject robot;
    public GameObject cam;
    public float speedRotate;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            
        }
    }
}
