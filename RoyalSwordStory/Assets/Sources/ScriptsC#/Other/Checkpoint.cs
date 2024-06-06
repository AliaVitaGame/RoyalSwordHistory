using System;
using UnityEngine;

[RequireComponent(typeof(AudioFX))]
[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flag;
    [SerializeField] private Sprite flagActive;
    [SerializeField] private Sprite flagDontActive;
    [Space]
    [SerializeField] private AudioClip audioActivated;
    [SerializeField] private FactionCollecting factionCollecting;

    private bool _isActive;
    private AudioFX _audioFX;

    private static Action<Checkpoint> _checkpointActivated;

    private void OnEnable()
    {
        _checkpointActivated += Animation;
        DataPlayer.GetDataEvent += LoadFaction;
    }

    private void OnDisable()
    {
        _checkpointActivated -= Animation;
        DataPlayer.GetDataEvent -= LoadFaction;
    }

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        _audioFX = GetComponent<AudioFX>();
        SetSpriteFlag(flagDontActive);

        if(DataPlayer.SDKEnabled)
           LoadFaction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActive) return;

        if (collision.TryGetComponent(out DeadControllerPlayer player))
        {
            _checkpointActivated?.Invoke(this);
            player.SetRespawnPosition(transform.position);
            _audioFX.PlayAudio(audioActivated);
        }
    }

    private void Animation(Checkpoint checkpoint)
    {
        var thisCheckpoint = checkpoint == this;
        SetSpriteFlag(thisCheckpoint ? flagActive : flagDontActive);
        _isActive = thisCheckpoint;
    }

    private void LoadFaction()
    {
        var data = DataPlayer.GetData();
        flagActive = factionCollecting.Factions[data.SelectedFlagID].FlagSprite;
    }

    private void SetSpriteFlag(Sprite sprite) => flag.sprite = sprite;
}
