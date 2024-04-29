using UnityEngine;

public class LimbFX : MonoBehaviour
{
    [SerializeField] private SpriteRenderer limb;
    [SerializeField] private Rigidbody2D limbRigidbody2D;

    public void SetStats(Sprite sprite, Vector2 velocity, float angularVelocity)
    {
        limb.sprite = sprite;
        limbRigidbody2D.velocity = velocity;
        limbRigidbody2D.angularVelocity = angularVelocity;
    }
}
