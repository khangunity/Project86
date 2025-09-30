using UnityEngine;
using UnityEngine.VFX;

public class VFXShoot : MonoBehaviour
{
    [SerializeField] GameObject GObject;
    [SerializeField] VisualEffect vfx;
    [SerializeField] bool setPosition = false;
    [SerializeField] string namePosition;

    void Start()
    {
        vfx = GObject.GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Shoot.instance.Shooting)
        {
            GObject.SetActive(true);
            if (!setPosition)
            {
                setPosition = true;
                vfx.SetVector3(namePosition, GObject.transform.position);
            }
        }

        if (vfx != null && vfx.aliveParticleCount <= 0 && !Shoot.instance.Shooting)
        {
            GObject.SetActive(false);
            setPosition = false;
        }

    }
}
