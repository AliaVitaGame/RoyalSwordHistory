using Unity.VisualScripting;
using UnityEngine;

public class ExplosionLimbs : MonoBehaviour
{
    [SerializeField] private float forceUp = 10;
    [SerializeField] private float forceHorizontal = 10;
    [SerializeField] private float forceAngularVelocity = 100;
    [SerializeField] private Sprite[] limbsSprite;
    [SerializeField] private LimbFX limbPrefab;

    private float lifeTimeLimb = 5;

    public void PlayFX()
    {
        for (int i = 0; i < limbsSprite.Length; i++)
        {
            var limb = Instantiate(limbPrefab, transform.position, Quaternion.identity);
            limb.SetStats(limbsSprite[i], new Vector2(GetRandom(-forceHorizontal, forceHorizontal), GetRandom(0, forceUp)), GetRandom(-forceAngularVelocity, forceAngularVelocity));
            Destroy(limb.gameObject, lifeTimeLimb);
        }
    }

    private float GetRandom(float min, float max) => Random.Range(min, max);
}
