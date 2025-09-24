using UnityEngine;

public class AnimaShoot : MonoBehaviour
{
    public GameObject robot;

    [SerializeField] Animator animator;
    [SerializeField] string nameLayerTorso;
    [SerializeField] string nameLayerLeg;
    [SerializeField] string nameState;
    [SerializeField] int shootLayerTorso;
    [SerializeField] int shootLayerLeg;
    [SerializeField] bool isShooting = false;
    void Start()
    {
        animator = robot.GetComponent<Animator>();

        shootLayerTorso = animator.GetLayerIndex(nameLayerTorso);
        animator.SetLayerWeight(shootLayerTorso, 0f);

        shootLayerLeg = animator.GetLayerIndex(nameLayerLeg);
        animator.SetLayerWeight(shootLayerLeg, 0f);

    }

    void Update()
    {
        if (Shoot.instance.Shooting)
        {
            shootFunc();
        }

    }

    private void shootFunc()
    {
        animator.SetLayerWeight(shootLayerTorso, 1f);
        AnimatorStateInfo stateInfoTorso = animator.GetCurrentAnimatorStateInfo(shootLayerTorso);

        animator.SetLayerWeight(shootLayerLeg, 0.7f);
        AnimatorStateInfo stateInfoLeg = animator.GetCurrentAnimatorStateInfo(shootLayerLeg);

        if (stateInfoTorso.IsName(nameState) && stateInfoLeg.IsName(nameState))
        {
            if (!isShooting)
            {
                isShooting = true;

                animator.Play(nameState, shootLayerTorso, 0f);
                animator.Update(0f);

                stateInfoTorso = animator.GetCurrentAnimatorStateInfo(shootLayerTorso);
                
                animator.Play(nameState, shootLayerLeg, 0f);
                animator.Update(0f);

                stateInfoLeg = animator.GetCurrentAnimatorStateInfo(shootLayerLeg);
            }
            if (isShooting && stateInfoTorso.normalizedTime >= 1.0f && stateInfoLeg.normalizedTime >= 1f)
            {
                Shoot.instance.Shooting = false;
                animator.SetLayerWeight(shootLayerTorso, 0f);
                animator.SetLayerWeight(shootLayerLeg, 0f);

                isShooting = false;
            }
        }
    }
}
