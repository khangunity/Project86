using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LazerHit : MonoBehaviour
{
    public VisualEffect laserVFX;   // gán Laser VFX trong Inspector
    public Transform currentLazer;     // điểm bắn laser
    public Transform lightHit;

    public float upMesh;
    public float maxDistance = 100f;
    public LayerMask hitMask;       // layer nào laser được chạm

    public List<string> nameLayerTerrain;

    bool OnEvent = false;

    void Update()
    {
        laserVFX.SetVector3("Current Position", currentLazer.position);

        RaycastHit hit;
        float distance = maxDistance;

        // Raycast từ firePoint theo hướng forward
        if (Physics.Raycast(currentLazer.position, currentLazer.forward, out hit, maxDistance, hitMask))
        {
            distance = hit.distance;

            lightHit.gameObject.SetActive(true);
            lightHit.position = hit.point - (currentLazer.forward.normalized * upMesh);

            if (!OnEvent)
            {
                OnEvent = true;
                laserVFX.SendEvent("PlaySpawnHit");
            }
            laserVFX.SetVector3("HitPosition", hit.point - (currentLazer.forward.normalized * upMesh));

            for (int i = 0; i < nameLayerTerrain.Count; i++)
            {
                if (LayerMask.LayerToName(hit.collider.gameObject.layer) == nameLayerTerrain[i])
                {
                    laserVFX.SetFloat("AngleHit", 90);
                    break;
                }
                else
                {
                    laserVFX.SetFloat("AngleHit", 0);
                }
            }


        }
        else
        {
            if (OnEvent)
            {
                OnEvent = false;
                laserVFX.SendEvent("StopSpawnHit");
            }
            lightHit.gameObject.SetActive(false);
            distance = maxDistance;
        }

        // Truyền distance vào VFX
        laserVFX.SetFloat("Distance", distance);
    }
}
