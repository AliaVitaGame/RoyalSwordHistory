using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class NPCController : MonoBehaviour
{
    [Header("BASIC SETTINGS")]
    [SerializeField] private bool isInteractive; 
    [SerializeField] private float checkRadiusOne;
    [SerializeField] private float checkRadiusTwo;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private NPCChatCloudManager _chatCloudManager;

    private float mainCheckRadius;
    private bool playerIsClose = false;

    private bool _playerIsBuyingSMTH;

    public static event Action OnPlayerIsClose;

    private void Start()
    {
        mainCheckRadius = checkRadiusOne;
    }

    private void OnEnable()
    {
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed += NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }
    private void OnDisable()
    {
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed -= NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }

    private void NPCButtonEventTriggers_OnOpenPocketsButtonPressed(bool shopPanelIsActive)
    {
        if(shopPanelIsActive == true)
            _playerIsBuyingSMTH = true;
        else if(shopPanelIsActive == false)
            _playerIsBuyingSMTH = false;
    }

    private void FixedUpdate()
    {
        CheckZone();
    }

    private void CheckZone()
    {
        if (_playerIsBuyingSMTH == true) return;

        if (Physics2D.OverlapCircle(transform.position, checkRadiusOne, _playerLayer))
        {
            OnPlayerIsClose?.Invoke();

            if(playerIsClose == false)
            {
                _chatCloudManager.TurnOnChatPanel(0);
            }

            if (Physics2D.OverlapCircle(transform.position, checkRadiusTwo, _playerLayer) && playerIsClose == false && isInteractive == true)
            {
                playerIsClose = true;
                _chatCloudManager.TurnOnChatPanel(1);
            }
        }
        else
        {
            playerIsClose = false ;
            _chatCloudManager.TurnOnChatPanel(2);
        }
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
