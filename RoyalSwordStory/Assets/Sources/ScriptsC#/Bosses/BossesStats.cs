using UnityEngine;

public class BossesStats : MonoBehaviour
{
    public float AggroRange;
    public float MoveSpeed;
    public float ShootDistance;
    public float ProjectileSpeed;
    public float RunAwayDistance;
    public bool isWizard = false;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (isWizard) 
        {
            Gizmos.DrawWireSphere(transform.position, ShootDistance);
        }else
        {
            Gizmos.DrawWireSphere(transform.position, AggroRange);
        }
    }
}
