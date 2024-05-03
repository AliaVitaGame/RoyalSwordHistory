using UnityEngine;

[CreateAssetMenu]
public class LaserForward : MagicManager
{
    [SerializeField] private GameObject _lightningPrefab;
    [SerializeField] private LayerMask _enemyLayer;

    public override void Activate()
    {
        ShootLightning();
    }

    private void ShootLightning()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 shootingDirection = player.transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        Transform shootingPos = player.transform;
        GameObject lightning = Instantiate(_lightningPrefab, shootingPos.position, Quaternion.identity);

        lightning.transform.localScale = new Vector3(shootingDirection.x, 1f, 1f);

        RaycastHit2D hit = Physics2D.Raycast(shootingPos.position, shootingDirection, Mathf.Infinity, _enemyLayer);
        if (hit.collider != null)
        {

            Destroy(hit.collider.gameObject);
        }

        // анимации тут будут

        Destroy(lightning, 2f);
    }
}
