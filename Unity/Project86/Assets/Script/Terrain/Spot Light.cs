using UnityEngine;
using UnityEngine.VFX;

public class SpotLight : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public VisualEffect visualGraph;
    public Light pointLight;

    [Header("Distance Settings")]
    public float minDistance = 50f;   // Lại gần mức này = tối nhất
    public float maxDistance = 500f;  // Xa mức này = sáng nhất
    public float minDistance1 = 300f;   // Lại gần mức này = tối nhất
    public float maxDistance1 = 500f;
    public float minDistance2 = 150f;   // Lại gần mức này = tối nhất
    public float maxDistance2 = 500f;

    [Header("Smooth Speed")]
    public float smoothSpeed = 3f;

    [Header("Light Settings")]
    public float maxIntensity = 200000f;

    private float currentAlphaLaser;
    private float currentAlphaAssit;
    private float currentAlphaPart;
    private float currentIntensity;

    void Start()
    {
        currentIntensity = maxIntensity;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        float alphaLaser = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(minDistance2, maxDistance2, dist));
        float alphaAssit = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(minDistance1, maxDistance1, dist));
        float alphaPart = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(minDistance, maxDistance, dist));
        float intensity = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(minDistance, maxDistance, dist));

        // mượt dần
        currentAlphaLaser = Mathf.Lerp(currentAlphaLaser, alphaLaser, Time.deltaTime * smoothSpeed);
        currentAlphaAssit = Mathf.Lerp(currentAlphaAssit, alphaAssit, Time.deltaTime * smoothSpeed);
        currentAlphaPart = Mathf.Lerp(currentAlphaPart, alphaPart, Time.deltaTime * smoothSpeed);
        currentIntensity = Mathf.Lerp(currentIntensity, intensity * maxIntensity, Time.deltaTime * smoothSpeed);

        // Gửi vào Visual Graph
        if (visualGraph != null)
            visualGraph.SetFloat("AlphaLaser", currentAlphaLaser);
            visualGraph.SetFloat("AlphaAssit", currentAlphaAssit);
            visualGraph.SetFloat("AlphaPart", currentAlphaPart);

        // Chỉnh light
        if (pointLight != null)
            pointLight.intensity = currentIntensity;
    }
}