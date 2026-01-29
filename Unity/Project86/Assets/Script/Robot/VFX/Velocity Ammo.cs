using System;
using TreeEditor;
using UnityEngine;

public class VelocityAmmo : MonoBehaviour
{
    [SerializeField] GameObject Robot;
    [SerializeField] AnimationCurve curveZ;
    [SerializeField] AnimationCurve curveY;
    [SerializeField] float maxZ;
    [SerializeField] float maxY;
    [SerializeField] float maxTime;
    [SerializeField] float speed;
    float valueTime;

    [SerializeField] CharacterController characterController;
    [SerializeField] Transform transformRobot;

    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            if(Robot == null)
            {
                for(int i = 0; i < transform.parent.parent.childCount; i++)
                {
                    if(transform.parent.parent.GetChild(i).name == "Model")
                    {
                        for(int j = 0; j < transform.parent.parent.GetChild(i).childCount; j++)
                        {
                            if(transform.parent.parent.GetChild(i).GetChild(j).name == "Basic User's Robot")
                            {
                                Robot = transform.parent.parent.GetChild(i).GetChild(j).gameObject;
                            }
                        }
                    }
                }
            }
            if(characterController == null)
            {
                characterController = gameObject.GetComponent<CharacterController>();
            }
            if(transformRobot == null && Robot != null)
            {
                transformRobot = this.transform;
                transformRobot.position = Robot.transform.position;
                transformRobot.rotation = Robot.transform.rotation;
            } 

            if(Robot != null && characterController != null)
            {
                valueTime += Time.deltaTime;
                valueTime = Math.Clamp(valueTime, 0, maxTime);

                float valueCurveZ = curveZ.Evaluate(valueTime/maxTime);
                float valueCurveY = curveY.Evaluate(valueTime/maxTime);

                float valueZ = valueCurveZ * maxZ;
                float valueY = valueCurveY * maxY;

                Vector3 forward = transformRobot.forward.normalized * valueZ;
                Vector3 up = transformRobot.up.normalized * valueY;

                Vector3 move = forward + up;

                characterController.Move(move.normalized * Time.deltaTime * speed);
                transform.forward = move.normalized;
            }

        }

        if (valueTime >= maxTime)
        {
            Destroy(gameObject);
            valueTime = 0;
        }
    }
}
