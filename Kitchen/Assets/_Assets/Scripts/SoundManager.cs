using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrushCounter.OnAnyObjectTrushed += TrushCounter_OnAnyObjectTrushed;
    }
    private void TrushCounter_OnAnyObjectTrushed(object sender, System.EventArgs e)
    {
        TrushCounter trushCounter = sender as TrushCounter;
        // Play object trushed sound
        PlaySound(audioClipRefsSO.trash, trushCounter.transform.position);
    }
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        // Play object placed sound
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        Player player = sender as Player;
        // Play pickup sound
        PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        // Play cut sound
        PlaySound(audioClipRefsSO.choppingSounds, cuttingCounter.transform.position);
    }
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        // Play success sound
        PlaySound(audioClipRefsSO.deliverySuccessSounds, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        // Play failed sound
        PlaySound(audioClipRefsSO.deliveryFailSounds, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 2f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    private void PlaySound(AudioClip[] audioClipArry, Vector3 position, float volume = 2f)
    {
        if (audioClipArry.Length == 0) return;
        AudioClip clip = audioClipArry[Random.Range(0, audioClipArry.Length)];
        PlaySound(clip, position, volume);
    }
    public void PlayFootstepSound(Vector3 position, float volume = 3f)
    {
        PlaySound(audioClipRefsSO.footstepSounds, position, volume);
    }
}
