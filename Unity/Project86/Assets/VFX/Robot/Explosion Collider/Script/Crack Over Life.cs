using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class CrackOverLife : MonoBehaviour
{
    [SerializeField] Renderer targetRenderer;
    public string propertyName = "_Alpha";

    [Range(0f, 1f)]
    public float value = 1f;
    [SerializeField] float Speed;
    [SerializeField] GameObject Targetobject;
    [SerializeField] VisualEffect vfx;
    [SerializeField] DecalProjector decal;

    void Start()
    {
        decal = GetComponent<DecalProjector>();

        // Clone đúng loại material của Decal Projector
        decal.material = new Material(decal.material);
        Targetobject = transform.parent.gameObject;
        vfx = transform.parent.GetChild(0).GetComponent<VisualEffect>();
    }

    void Update()
    {
        if (Targetobject.activeSelf && vfx.aliveParticleCount > 0f)
        {
            gameObject.SetActive(true);

            value = 1f;
        }
        else if (vfx.aliveParticleCount <= 0f && value > 0f)
        {
            value -= Speed * Time.deltaTime;
        }

        decal.material.SetFloat(propertyName, value);
    }
}
