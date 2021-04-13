using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

/// <summary>
/// Command that creates a particle effect
/// </summary>
public class CreateParticleEffectCommand : ICommand
{
    ParticleSystem _particleSystem;
    float _particleAffectLifeTime;
    Tile _tile;
    /// <summary>
    /// Command that creates a particle effect
    /// </summary>
    public CreateParticleEffectCommand(ParticleSystem particleSystem, float particleAffectLifeTime, Tile tile)
    {
        _particleSystem = particleSystem;
        _particleAffectLifeTime = particleAffectLifeTime;
        _tile = tile;
    }

    public IEnumerator Execute()
    {
        var particleAffect = UnityEngine.Object.Instantiate(_particleSystem);
        particleAffect.gameObject.name = $"ParticleAffect_{_tile.GridX}_{_tile.GridY}";
        particleAffect.transform.position = TileGridController.Instance.GetGrid().GetWorldPositionCentered(_tile.GridX, _tile.GridY);
        particleAffect.Play();
        UnityEngine.Object.Destroy(particleAffect.gameObject, _particleAffectLifeTime);
        yield return new WaitForSeconds(10f);

    }

}
