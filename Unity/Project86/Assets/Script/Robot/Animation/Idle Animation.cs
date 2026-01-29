using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    [Header("Object/Controller")]
    public GameObject robot;
    [SerializeField] Animator animator;

    [Header("Parameter")]
    public string nameBlendTree;
    public string idBlendTree;
    public float time = 30f;
    [SerializeField] bool isIdle = false;
    [SerializeField] float timeClone = 0f;

    void Start()
    {
        animator = robot.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Move.instance.keyNumber == 0 && stateInfo.IsName(nameBlendTree) && !LookCamera.instance.isLooking)
        {
            if (timeClone < time)
            {
                timeClone += Time.deltaTime;
            }
            else if (timeClone >= time && !isIdle)
            {
                isIdle = true;

                animator.SetFloat(idBlendTree, Random.Range(1, 3));

                animator.Play(nameBlendTree, 0, 0f);
                animator.Update(0f);

                stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            }
            else if (isIdle && stateInfo.normalizedTime >= 1f)
            {
                timeClone = 0f;
                animator.SetFloat(idBlendTree, 0f);

                isIdle = false;
            }
        }
        else
        {
            isIdle = false;
            timeClone = 0f;
            animator.SetFloat(idBlendTree, 0f);
        }
    }
}
