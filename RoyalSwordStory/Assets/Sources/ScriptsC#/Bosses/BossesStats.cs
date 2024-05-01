using UnityEngine;

public class BossesStats : MonoBehaviour
{
    public float AggroRange;
    public float MoveSpeed;
    public float ShootDistance;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,AggroRange);
    }
}
