using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Slider progressBarSlider;

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        progressBarSlider.value = 0f;
        gameObject.SetActive(false);
    }
    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        progressBarSlider.value = e.progressNormalized;
        if (e.progressNormalized == 1f || e.progressNormalized == 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
