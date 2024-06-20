using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHolder : MonoBehaviour
{
     public MagicManager MagicManager;
    [SerializeField] private KeyCode _keyCode;
    [SerializeField] public AudioFX audioFX;

    private float _cooldownTime;
    private float _activeTime;

    enum MagicState
    {
        ready,
        active,
        cooldown
    }
    MagicState state = MagicState.ready;

    private void Update()
    {
        if (state == MagicState.ready)
        {
            if (Input.GetKeyDown(_keyCode))
            {
                MagicManager.Activate();
                MagicManager.PlayEquipAudio(audioFX);
                state = MagicState.active;
                _activeTime = MagicManager.ActiveTime;
            }
        }
        else if (state == MagicState.active)
        {
            if (_activeTime > 0)
            {
                _activeTime -= Time.deltaTime;
            }
            else
            {
                state = MagicState.cooldown;
                _cooldownTime = MagicManager.CooldownTime;
            }
        }
        else if (state == MagicState.cooldown)
        {
            if (_cooldownTime > 0)
            {
                _cooldownTime -= Time.deltaTime;
            }
            else
            {
                state = MagicState.ready;
            }
        }

    }
}
