using System.Collections;
using UnityEngine;

public class SpawnExplosion : MonoBehaviour
{
    public static SpawnExplosion instance;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject Parent;
    [SerializeField] float lifeTime;
    [SerializeField] float distance;
    float valueTime;
    GameObject explosionClone;
    public int countSpawn = 0;
    public Transform location;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(location == null)
        {
            location = this.transform;
        }
    }


    void Update()
    {
        if(countSpawn > 0 && location != null)
        {
            explosionClone = Instantiate(explosion, location.position, explosion.transform.rotation, Parent.transform);
            explosionClone.transform.position = location.position;
            explosionClone.transform.rotation = location.rotation;
            explosionClone.SetActive(true);

            countSpawn -= 1;

            StartCoroutine(delayDestroy(explosionClone));
        }
    }

    private IEnumerator delayDestroy(GameObject GO)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(GO);
    }
}
