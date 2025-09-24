using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] GameObject target;

    [SerializeField] bool following = true;

    [SerializeField] float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            transform.position = target.transform.position + (target.transform.up * distance);
            transform.rotation = target.transform.rotation * Quaternion.Euler(-90f, 0f, 0f);

        }
    }
}
