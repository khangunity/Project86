using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CaveStart : MonoBehaviour
{
    public GameObject playerRoot;
    public Volume globalVolume;
    public GameObject light;
    public GameObject shadow;
    public float speed = 2f;
    public float cooldown = 1f; // thời gian chờ trigger lại

    private ColorAdjustments colorAdjustments;
    private Renderer[] renderers;
    private bool insideCave = false;
    private float nextTriggerTime = 0f;

    void Start()
    {
        globalVolume.profile.TryGet(out colorAdjustments);

        if (playerRoot != null)
            renderers = playerRoot.GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // Tối màn hình khi vào hang
        if (colorAdjustments != null)
        {
            float targetExposure = insideCave ? -0.5f : 1f;

            colorAdjustments.postExposure.value =
                Mathf.Lerp(
                    colorAdjustments.postExposure.value,
                    targetExposure,
                    Time.deltaTime * speed
                );
        }

        // Chỉnh Value Roughness cho tất cả material player
        if (renderers != null)
        {
            foreach (Renderer r in renderers)
            {
                Material[] mats = r.materials;

                foreach (Material mat in mats)
                {
                    if (mat.HasProperty("_Value_Roughness"))
                    {
                        float target = insideCave ? 0f : 0.3f;

                        float current = mat.GetFloat("_Value_Roughness");
                        float next = Mathf.Lerp(current, target, Time.deltaTime * speed);

                        mat.SetFloat("_Value_Roughness", next);
                    }
                }
            }
        }
        light.SetActive(insideCave);
        shadow.SetActive(!insideCave);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // đang cooldown thì bỏ qua
        if (Time.time < nextTriggerTime) return;

        insideCave = !insideCave;

        // set thời gian được phép trigger tiếp theo
        nextTriggerTime = Time.time + cooldown;
    }
}