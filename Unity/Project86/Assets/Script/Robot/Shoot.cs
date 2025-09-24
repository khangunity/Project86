using NUnit.Framework;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public static Shoot instance;
    public bool Shooting;
    [SerializeField] float ValueCoolDown;
    float time;
    [SerializeField] bool isCoolDown = false;

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
