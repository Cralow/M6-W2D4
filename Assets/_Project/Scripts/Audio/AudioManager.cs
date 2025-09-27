using System;
using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [SerializeField] TerrainSounds[] terrainSounds;

    public void RandomTerrainSound(int terrainLayer)
    {
        foreach (var terrainSound in terrainSounds)
        {
            if (terrainSound.LayerIndex == terrainLayer)
            {
                int randomIndex = UnityEngine.Random.Range(0, terrainSound.SoundClips.Length);
                AudioClip clip = terrainSound.SoundClips[randomIndex];

                if (clip != null)
                {
                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                }
                return;
            }



        }

    }
}
[Serializable]
public class TerrainSounds
{
    public string LayerName;
    public int LayerIndex;
    public AudioClip[] SoundClips;
}

