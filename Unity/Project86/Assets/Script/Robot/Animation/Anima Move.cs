using System.Collections;
using UnityEngine;

public class AnimaMove : MonoBehaviour
{
    [Header("Object/Controller")]
    public GameObject robot;

    [Header("Name String State/Parameter in Animator")]
    public string nameBlendTree_1;
    public string idBlendTree_1;
    public string nameBlendTree_2;
    public string idBlendTree_2;
    public string nameBlendTree_3;
    public string idBlendTree_3;
    public string nameBlendTree_4;
    public string idBlendTree_4;
    public string nameBlendTree_5;
    public string idBlendTree_5;

    [SerializeField] bool isStop = false;
    [SerializeField] bool isSpeed = false;
    [SerializeField] bool isStopFast = false;

    [SerializeField] Animator animator;
    void Start()
    {
        animator = robot.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Stop Animation
        if (Move.instance.stop)
        {
            AnimatorStateInfo blendTree = animator.GetCurrentAnimatorStateInfo(0);
            if (Move.instance.keyNumber == 1)
            {
                stopFunc(blendTree, nameBlendTree_1, idBlendTree_1);
            }
            if (Move.instance.keyNumber == 2)
            {
                stopFunc(blendTree, nameBlendTree_2, idBlendTree_2);
            }
            if (Move.instance.keyNumber == 3)
            {
                stopFunc(blendTree, nameBlendTree_3, idBlendTree_3);
            }
            if (Move.instance.keyNumber == 4)
            {
                stopFunc(blendTree, nameBlendTree_4, idBlendTree_4);
            }
        }
        #endregion

        #region Move Animation
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
        #endregion

        #region Rotate Animation
        if (Move.instance.keyNumber == 0 && LookCamera.instance.isLooking && !Shoot.instance.Shooting)
        {
            if (LookCamera.instance.direction <= -1)
            {
                rotateFunc(0f);
            }
            else if (LookCamera.instance.direction >= 1)
            {
                rotateFunc(1f);
            }
            else
            {
                animator.SetBool("RotateBool", false);
            }
        }
        else
        {
            animator.SetBool("RotateBool", false);
        }
        #endregion

        #region Speed/Fast Animation
        if (Move.instance.isFast && !Move.instance.stopFast)
        {
            AnimatorStateInfo blendTree = animator.GetCurrentAnimatorStateInfo(0);

            if (Move.instance.keyNumber == 1)
            {
                speedFunc(blendTree, nameBlendTree_1, idBlendTree_1);
            }
            else if (Move.instance.keyNumber == 2)
            {
                speedFunc(blendTree, nameBlendTree_2, idBlendTree_2);
            }
            else if (Move.instance.keyNumber == 3)
            {
                speedFunc(blendTree, nameBlendTree_3, idBlendTree_3);
            }
            else if (Move.instance.keyNumber == 4)
            {
                speedFunc(blendTree, nameBlendTree_4, idBlendTree_4);
            }
        }
        #endregion

        #region Stop Fast Animation
        if (Move.instance.stopFast && !Move.instance.isFast)
        {
            isSpeed = false;

            AnimatorStateInfo blendTree = animator.GetCurrentAnimatorStateInfo(0);

            if (Move.instance.keyNumber == 1)
            {
                stopFastFunc(blendTree, nameBlendTree_1, idBlendTree_1);
            }
            else if (Move.instance.keyNumber == 2)
            {
                stopFastFunc(blendTree, nameBlendTree_2, idBlendTree_2);
            }
            else if (Move.instance.keyNumber == 3)
            {
                stopFastFunc(blendTree, nameBlendTree_3, idBlendTree_3);
            }
            else if (Move.instance.keyNumber == 4)
            {
                stopFastFunc(blendTree, nameBlendTree_4, idBlendTree_4);
            }
        }
        
        #endregion
    }

    #region hàm Speed/Fast
    private void speedFunc(AnimatorStateInfo state, string name, string id)
    {
        if (state.IsName(name))
        {
            if (!isSpeed)
            {
                isSpeed = true;
                animator.SetFloat(id, 4f);

                animator.Play(name, 0, 0f);
                animator.Update(0f);

                state = animator.GetCurrentAnimatorStateInfo(0);
            }

            if (isSpeed && state.normalizedTime >= 1f)
            {
                fastFunc(state, name, id);
            }
        }
    }

    private void fastFunc(AnimatorStateInfo state, string name, string id)
    {
        if (state.IsName(name) && isSpeed)
        {
            animator.SetFloat(id, 2f);
        }
    }
    #endregion

    #region Hàm Stop Fast
    private void stopFastFunc(AnimatorStateInfo state, string name, string id)
    {
        if (state.IsName(name))
        {
            if (!isStopFast)
            {
                isStopFast = true;
                animator.SetFloat(id, 3f);

                animator.Play(name, 0, 0f);
                animator.Update(0f);

                state = animator.GetCurrentAnimatorStateInfo(0);

                LookCamera.instance.isLook = false;
            }

            if (isStopFast && state.normalizedTime >= 1f)
            {
                isStopFast = false;

                Move.instance.keyNumber = 0;
                Move.instance.stopFast = false;

                animator.SetFloat(id, 0f);

                animator.SetBool("UpBool", false);
                animator.SetBool("DownBool", false);
                animator.SetBool("RightBool", false);
                animator.SetBool("LeftBool", false);

                LookCamera.instance.isLook = true;
            }
        }

    }
    #endregion

    #region hàm Stop
    private void stopFunc(AnimatorStateInfo state, string name, string id)
    {
        if (state.IsName(name))
        {

            if (!isStop)
            {
                isStop = true;

                animator.SetFloat(id, 1f);

                animator.Play(name, 0, 0f);
                animator.Update(0f);

                state = animator.GetCurrentAnimatorStateInfo(0);

                LookCamera.instance.isLook = false;
            }

            if (isStop && state.normalizedTime >= 1f)
            {
                Move.instance.stop = false;
                Move.instance.keyNumber = 0;

                isStop = false;

                animator.SetBool("UpBool", false);
                animator.SetBool("DownBool", false);
                animator.SetBool("RightBool", false);
                animator.SetBool("LeftBool", false);

                animator.SetFloat(id, 0f);

                LookCamera.instance.isLook = true;
            }
        }
    }
    #endregion

    #region hàm Rotate
    private void rotateFunc(float n)
    {
        animator.SetBool("RotateBool", true);
        animator.SetFloat(idBlendTree_5, n);
    }
    #endregion
}


