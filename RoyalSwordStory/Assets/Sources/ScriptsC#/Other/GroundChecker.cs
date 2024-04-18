using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask layerGround;

    public bool IsGround;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
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
}
