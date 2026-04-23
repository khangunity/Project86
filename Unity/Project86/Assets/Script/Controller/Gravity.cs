using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Object/Controller")]
    public GameObject target;       // object cần kiểm tra và rơi (set trong Inspector)
    [SerializeField] CharacterController character;

    [Header("Box ground check")]
    [SerializeField] Vector3 boxSize = new Vector3(0.6f, 0.1f, 0.6f);   // full size box
    [SerializeField] Vector3 boxOffset = new Vector3(0f, -0.95f, 0f);  // vị trí box so với target
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float groundSnapOffset = 0.05f;

    [SerializeField] bool IsGrounded = false;
    [SerializeField] float verticalVelocity = 0f;
    

    void Start()
    {
        character = target.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (target == null) return; // nếu chưa gán object thì thoát

        // Tính center & half size cho CheckBox
        Vector3 center = target.transform.TransformPoint(boxOffset);
        Vector3 halfExtents = boxSize * 0.5f;

        // Check ground
        IsGrounded = Physics.CheckBox(center, halfExtents, Quaternion.identity, groundLayer, QueryTriggerInteraction.Ignore);

        if (IsGrounded)
        {

            verticalVelocity = 0f;

            // Snap xuống sát mặt đất
            if (Physics.Raycast(target.transform.position, Vector3.down, out RaycastHit hit, 1.5f, groundLayer))
            {
                Vector3 pos = target.transform.position;
                pos.y = hit.point.y + groundSnapOffset;
                target.transform.position = pos;
            }
        }
        else
        {
            // Không chạm đất → rơi xuống
            verticalVelocity -= gravity * Time.deltaTime;
            character.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));
        }
    }
    
    /*
    void OnDrawGizmos()
    {
        if (target == null) return;

        Vector3 center = target.transform.TransformPoint(boxOffset);

        Gizmos.color = IsGrounded ? new Color(0f, 1f, 0f, 0.3f) : new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(center, boxSize);

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(center, boxSize);
    }
    */
}
