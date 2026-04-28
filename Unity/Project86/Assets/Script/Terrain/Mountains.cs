using UnityEngine;

public class Mountains : MonoBehaviour
{
    public Transform camera;
    public Transform mount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mount.transform.position = new Vector3 (camera.position.x, mount.transform.position.y, camera.position.z);

    }
}
