using Unity.Mathematics;
using UnityEngine;

public class TriggerAmmo : MonoBehaviour
{
    [SerializeField] string[] LayerCollider;
    [SerializeField] float distance;


    void OnCollisionEnter(Collision  other)
    {
        string objectLayerName = LayerMask.LayerToName(other.gameObject.layer);

        for (int i = 0; i < LayerCollider.Length; i++)
        {
            if (objectLayerName == LayerCollider[i])
            {

                if(LayerCollider[i] == "Terrain")
                {
                    SpawnExplosion.instance.location.position = transform.position + transform.up * distance;
                    SpawnExplosion.instance.location.rotation = Quaternion.Euler(90f, 0f, 0f);
                }
                else
                {
                    SpawnExplosion.instance.location.position = transform.position - transform.forward * distance;
                }
                SpawnExplosion.instance.countSpawn += 1;

                NoiseExplosion.instance.targetPosition = transform.position;
                NoiseExplosion.instance.isNoise = true;

                Destroy(gameObject);
                break;
            }
        }
    }
    
}
