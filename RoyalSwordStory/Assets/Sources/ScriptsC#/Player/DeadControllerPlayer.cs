using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAttacking))]
public class DeadControllerPlayer : MonoBehaviour
{
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject[] disabledObjects;

    private PlayerStats _playerStats;
    private PlayerMove _playerMove;
    private PlayerAttacking _playerAttacking;
    private Vector3 _respawnPosition;

    private void OnEnable()
    {
        PlayerStats.PlayerDaadEvent += Dead;
    }

    private void OnDisable()
    {
        PlayerStats.PlayerDaadEvent -= Dead;
    }

    private void Start()
    {
        _respawnPosition = transform.position;
        _playerStats = GetComponent<PlayerStats>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttacking = GetComponent<PlayerAttacking>();
    }

    public void Resurrect()
    {
        deadPanel.SetActive(false);
        _playerMove.SetStopMove(true);
        ManagerUI.Instance.OpenUI(false);
        transform.position = _respawnPosition;
        _playerStats.Resurrect();
        _playerMove.SetStopMove(false);
        _playerAttacking.IsAttacking = false;
    }

    private void Dead()
    {
        deadPanel.SetActive(true);
        _playerMove.SetStopMove(true);
        _playerMove.SetVelosity(Vector2.zero);
        _playerAttacking.IsAttacking = true;

        for (int i = 0; i < disabledObjects.Length; i++)
        {
            disabledObjects[i].SetActive(false);
        }
    }

    public void SetRespawnPosition(Vector2 vector)
        => _respawnPosition = vector;
}
