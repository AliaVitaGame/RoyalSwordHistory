using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class ButtonSelectLevel : MonoBehaviour
{
    [SerializeField] private int sceneID;
    [SerializeField] private Color selectColor = Color.white;

    private Color _startColor;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    private void OnMouseDown()
    {
        SceneLoadingManager.Instance.LoadSceneID(sceneID);
    }

    private void OnMouseEnter()
    {
        SetColor(selectColor);
    }

    private void OnMouseExit()
    {
        SetColor(_startColor);
    }

    private void SetColor(Color color) => _spriteRenderer.color = color;
}
