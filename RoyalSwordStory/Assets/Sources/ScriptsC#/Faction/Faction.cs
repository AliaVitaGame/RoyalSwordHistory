using UnityEngine;

[CreateAssetMenu(menuName = "Faction", fileName = "Faction")]
public class Faction : ScriptableObject
{
    [SerializeField] private Sprite flag;

    public Sprite FlagSprite => flag;
}
