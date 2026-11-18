using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] choppingSounds;
    public AudioClip[] deliverySuccessSounds;
    public AudioClip[] deliveryFailSounds;
    public AudioClip[] footstepSounds;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
