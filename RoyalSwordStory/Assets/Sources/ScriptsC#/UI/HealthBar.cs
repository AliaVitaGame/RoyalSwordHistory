using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;

    private Vector3 _startPosition;
    private Transform _parent;
    private bool _isUnpin;

    public void Unpin()
    {
        _isUnpin = true;
        _parent = transform.parent;
        _startPosition = transform.position;
        _startPosition.x = 0;
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        if(_parent == null)
        {
            Destroy(gameObject);
            return;
        }

        if (_isUnpin)
        transform.position = _parent.position + _startPosition;
    }

    public void SetHealth(float health, float maxHealth) 
        => healthImage.fillAmount = health / maxHealth;
}
