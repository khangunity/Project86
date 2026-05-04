using UnityEngine;
using UnityEngine.VFX;

public class Shadow : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public VisualEffect visualGraph;

    [Header("Distance Settings")]
    public float minDistance = 40f;   // Lại gần mức này = tối nhất
    public float maxDistance = 70f;  // Xa mức này = sáng nhất

    [Header("Smooth Speed")]
    public float smoothSpeed = 3f;

    [Header("Light Settings")]
    private float currentAlpha;

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        float alpha = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(minDistance, maxDistance, dist));

        // mượt dần
        currentAlpha = Mathf.Lerp(currentAlpha, alpha, Time.deltaTime * smoothSpeed);

        // Gửi vào Visual Graph
        if (visualGraph != null)
            visualGraph.SetFloat("Alpha", currentAlpha);
    }
}
