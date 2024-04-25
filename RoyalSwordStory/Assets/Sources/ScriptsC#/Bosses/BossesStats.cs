using UnityEngine;

public class BossesStats : MonoBehaviour
{
    public float AggroRange;
    public float MoveSpeed;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,AggroRange);
    }
}
