using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSkin", fileName = "PlayerSkin")]
public class PlayerSkin : ScriptableObject
{
    [SerializeField] private Sprite previewSprite;
    [SerializeField] private AnimatorOverrideController animator;

    public AnimatorOverrideController Animator => animator;
    public Sprite PreviewSprite => previewSprite;
}
