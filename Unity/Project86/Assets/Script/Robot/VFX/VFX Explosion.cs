using UnityEngine;
using UnityEngine.VFX;

public class VFXExplosion : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;

    void Update()
    {
        if(vfx == null)
        {
            vfx = transform.GetChild(0).gameObject.GetComponent<VisualEffect>();
            vfx.SetVector3("Center Position", transform.position);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    
}
