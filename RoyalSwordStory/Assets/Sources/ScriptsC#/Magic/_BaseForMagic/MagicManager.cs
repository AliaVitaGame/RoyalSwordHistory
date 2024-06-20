using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : ScriptableObject
{
    public new string Name;
    public float CooldownTime;
    public float ActiveTime;

    public virtual void Activate() { }

    public virtual void PlayEquipAudio(AudioFX audioFX) { }
    
}
