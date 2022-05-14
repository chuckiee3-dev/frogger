using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioClip moveSfx;
    [SerializeField] private AudioClip loseLifeSfx;
    [SerializeField] private AudioClip fillSlotSfx;

    
    private AudioSource[] _sources;

    private void Awake()
    {
        _sources = GetComponentsInChildren<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerActions.onMovementStart += PlayMoveSfx;
        GameActions.onPlayerFillSlot += PlayFillSlotSfx;
        GameActions.onPlayerLoseLife += PlayLoseLifeSfx;
    }
    private void OnDisable()
    {
        PlayerActions.onMovementStart -= PlayMoveSfx;
        GameActions.onPlayerFillSlot -= PlayFillSlotSfx;
        GameActions.onPlayerLoseLife -= PlayLoseLifeSfx;
    }

    private void PlayMoveSfx()
    {
        PlayClipOnFreeSource(moveSfx);
    }

    private void PlayLoseLifeSfx()
    {
        PlayClipOnFreeSource(loseLifeSfx);
    }

    private void PlayFillSlotSfx()
    {
        PlayClipOnFreeSource(fillSlotSfx);
    }

    private void PlayClipOnFreeSource(AudioClip audioClip)
    {
        for (int i = 0; i < _sources.Length; i++)
        {
            if(_sources[i].isPlaying) continue;
            
            _sources[i].clip = audioClip;
            _sources[i].Play();
            
            break;
        }
    }

}
