using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class NPCController : MonoBehaviour
{
    [Header("BASIC SETTINGS")]
    [SerializeField] private float checkRadiusOne;
    [SerializeField] private float checkRadiusTwo;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private NPCChatCloudManager _chatCloudManager;

    private float mainCheckRadius;
    private bool _isPassedFirstRadius;
    public static event Action OnPlayerIsClose;

    private void Start()
    {
        mainCheckRadius = checkRadiusOne;
    }

    private void FixedUpdate()
    {
        CheckZone();
    }

    private void CheckZone()
    {
        if (Physics2D.OverlapCircle(transform.position, mainCheckRadius, _playerLayer))
        {
            StartCoroutine(ReduceRadius(checkRadiusTwo, true));
            
            _chatCloudManager.TurnOnChatPanel(0);
            OnPlayerIsClose?.Invoke();
        }
        else if (Physics2D.OverlapCircle(transform.position, mainCheckRadius, _playerLayer))
        {
            _chatCloudManager.TurnOnChatPanel(1);
        }
        else
        {
            _isPassedFirstRadius = false;
            StartCoroutine(ReduceRadius(checkRadiusOne , false));
            _chatCloudManager.TurnOnChatPanel(2);
        }
    }

    private IEnumerator ReduceRadius(float radius , bool isPassedFirstRadius)
    {
        yield return new WaitForSeconds(5);
        _isPassedFirstRadius = isPassedFirstRadius;
        mainCheckRadius = radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadiusOne);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadiusTwo);

        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere(transform.position, mainCheckRadius);
    }
}
