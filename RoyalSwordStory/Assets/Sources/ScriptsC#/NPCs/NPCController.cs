using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class NPCController : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask _playerLayer;

    public static event Action OnPlayerIsClose;

    private void FixedUpdate()
    {
        CheckZone();
    }

    private void CheckZone()
    {
        if (Physics2D.OverlapCircle(transform.position, checkRadius, _playerLayer))
        {
            OnPlayerIsClose?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
