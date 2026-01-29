using NUnit.Framework;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public static Shoot instance;
    public bool Shooting;
    [SerializeField] float ValueCoolDown;
    float time;
    [SerializeField] bool isCoolDown = false;
    [SerializeField] Transform parentSpawn;
    [SerializeField] GameObject Ammo;
    [SerializeField] Transform AmmoLocation;
    public GameObject AmmoClone;

    void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Shooting && !Move.instance.stop && !Move.instance.isFast && !Move.instance.stopFast && !isCoolDown)
        {
            Shooting = true;
            isCoolDown = true;

            AmmoClone = Instantiate(Ammo, AmmoLocation.position, AmmoLocation.rotation);
            AmmoClone.SetActive(true);
            AmmoClone.transform.SetParent(parentSpawn);

            NoiseShoot.instance.isNoise = true;
        }

        if (isCoolDown)
        {
            time += Time.deltaTime;

            if (isCoolDown && time >= ValueCoolDown)
            {
                time = 0;
                isCoolDown = false;
            }
        }
    }


}
