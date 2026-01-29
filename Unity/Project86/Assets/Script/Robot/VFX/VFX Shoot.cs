using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class VFXShoot : MonoBehaviour
{
    [SerializeField] GameObject GObject;
    [SerializeField] Transform ShootLocation;
    [SerializeField] Transform GunPosition;
    [SerializeField] VisualEffect vfx;

    [SerializeField] GameObject GOClone;

    [SerializeField] GameObject Parent;

    // Update is called once per frame
    void Update()
    {
        if (Shoot.instance.Shooting && GOClone == null && vfx == null)
        {
            GOClone = Instantiate(GObject, ShootLocation.position, ShootLocation.rotation, Parent.transform);
            GOClone.SetActive(true);

            vfx = GOClone.GetComponent<VisualEffect>();

            vfx.SetVector3("CenterPosition", GOClone.transform.position);
            
        }

        if(GOClone != null && vfx != null)
        {
            vfx.SetVector3("Gun Position", GunPosition.position);
        }

        if (vfx != null && vfx.aliveParticleCount <= 0 && !Shoot.instance.Shooting)
        {
            Destroy(GOClone);
        }


    }
}
