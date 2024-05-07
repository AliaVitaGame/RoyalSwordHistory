using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CutSceneActivator : MonoBehaviour
{
    [SerializeField] private int _cutSceneID;

    private void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMove player))
        {
            CutSceneManager.Instance.Play(_cutSceneID);
            Destroy(gameObject);
        }
    }
}
