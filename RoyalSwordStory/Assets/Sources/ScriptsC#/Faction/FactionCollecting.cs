using UnityEngine;


[CreateAssetMenu(menuName = "FactionCollecting", fileName = "FactionCollecting")]
public class FactionCollecting : ScriptableObject
{
    [SerializeField] private Faction[] factions;

    public Faction[] Factions => factions;
}
