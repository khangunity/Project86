using System.Collections;
using UnityEngine;

public class AnimaMove : MonoBehaviour
{
    public GameObject robot;
    public float delayReset;
    Animator animator;
    void Start()
    {
        animator = robot.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Move.instance.keyNumber == 1)
        {
            animator.SetBool("UpBool", true);
            animator.SetBool("DownBool", false);
            animator.SetBool("RightBool", false);
            animator.SetBool("LeftBool", false);
        }
        else if (Move.instance.keyNumber == 2)
        {
            animator.SetBool("DownBool", true);
            animator.SetBool("UpBool", false);
            animator.SetBool("RightBool", false);
            animator.SetBool("LeftBool", false);
        }
        else if (Move.instance.keyNumber == 3)
        {
            animator.SetBool("RightBool", true);
            animator.SetBool("UpBool", false);
            animator.SetBool("DownBool", false);
            animator.SetBool("LeftBool", false);
        }
        else if (Move.instance.keyNumber == 4)
        {
            animator.SetBool("LeftBool", true);
            animator.SetBool("UpBool", false);
            animator.SetBool("DownBool", false);
            animator.SetBool("RightBool", false);
        }
        else
        {
            animator.SetBool("UpBool", false);
            animator.SetBool("DownBool", false);
            animator.SetBool("RightBool", false);
            animator.SetBool("LeftBool", false);
        }
    }
}
