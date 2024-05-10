using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class NPCController : MonoBehaviour
{
    [Header("BASIC SETTINGS")]
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkRadiusDevident;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private NPCChatCloud _chatCloud;



    private void Start()
    {
        _interactionPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        CheckZone();
    }

    private void CheckZone()
    {
        if (Physics2D.OverlapCircle(transform.position, checkRadius, _playerLayer))
        {

        }
        else
        {
            _interactionPanel.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius - checkRadiusDevident);
    }
}
