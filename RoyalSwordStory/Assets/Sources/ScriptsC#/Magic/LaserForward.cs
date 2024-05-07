using UnityEngine;

[CreateAssetMenu]
public class LaserForward : MagicManager
{
    [SerializeField] private GameObject _lightningPrefab;
    [SerializeField] private GameObject _lightningSpherePrefab;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private int _lightningSpawnDistance;
    [Space(20)]
    [SerializeField] private float _radiusDamage;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeStun;
    [SerializeField] private float _repulsion;

    private Transform _shootingPos;


    public override void Activate()
    {
        ShootLightning();
    }

    private void ShootLightning()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 shootingDirection = player.transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        _shootingPos = player.transform;
        _lightningPrefab.transform.position = _shootingPos.transform.position;
        GameObject lightningSphere = Instantiate(_lightningSpherePrefab, _shootingPos.position, Quaternion.identity);
        GameObject lightning = Instantiate(_lightningPrefab, DirectionReturn(shootingDirection).position, Quaternion.Euler(0, 90, 0));

        lightningSphere.transform.localScale = new Vector3(shootingDirection.x, 1f, 1f);
        lightning.transform.localScale = new Vector3(shootingDirection.x, 1f, 1f);      

        // анимации тут будут

        var temp = Physics2D.OverlapCircleAll(_lightningPrefab.transform.position, _radiusDamage, _enemyLayer);
        for (int i = 0; i < temp.Length; i++)
        {
            if(temp[i].TryGetComponent(out IUnitHealthStats unitHealth))
            {
                unitHealth.TakeDamage(_damage, _timeStun, _repulsion);
            }
        }

        Destroy(lightningSphere, .5f);
        Destroy(lightning, .5f);
    }

    private Transform DirectionReturn(Vector3 direction)
    {
        if (direction == Vector3.right)
        {
            _lightningPrefab.transform.position += new Vector3(_lightningSpawnDistance, 0,0);
        }
        else
        {
            _lightningPrefab.transform.position -= new Vector3(_lightningSpawnDistance, 0, 0);
        }
        return _lightningPrefab.transform;
    }
}
