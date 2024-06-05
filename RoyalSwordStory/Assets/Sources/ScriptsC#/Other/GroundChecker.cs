using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private LayerMask enemyLayer;

    public bool IsGround;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y, enemyLayer))
        IsGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(layerGround == (1 << collision.gameObject.layer))
            IsGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (layerGround == (1 << collision.gameObject.layer))
            IsGround = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * transform.localScale.y);
    }
}
