using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSkinsCollecting", fileName = "PlayerSkinsCollecting")]
public class PlayerSkinsCollecting : ScriptableObject
{
    [SerializeField] private PlayerSkin[] skins;

    public PlayerSkin[] Skins => skins;
}
