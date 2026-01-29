using UnityEngine;

public class TriggerAmmo : MonoBehaviour
{
    [SerializeField] string[] LayerCollider;


    void OnTriggerEnter(Collider other)
    {
        string objectLayerName = LayerMask.LayerToName(other.gameObject.layer);

        for (int i = 0; i < LayerCollider.Length; i++)
        {
            if (objectLayerName == LayerCollider[i])
            {
                SpawnExplosion.instance.location.position = transform.position;
                SpawnExplosion.instance.countSpawn += 1;

                NoiseExplosion.instance.targetPosition = transform.position;
                NoiseExplosion.instance.isNoise = true;

                Destroy(gameObject);
                break;
            }
        }
    }
    
}
