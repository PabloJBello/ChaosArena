using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject videoContent;
    [SerializeField] private GameObject audioContent;
    [SerializeField] private GameObject controlsContent;
    private GameObject currentContent;

    private void Awake()
    {
        // Set default tab (e.g., Video)
        currentContent = videoContent;
        videoContent.SetActive(true);
        audioContent.SetActive(false);
        controlsContent.SetActive(false);
    }

    public void OnVideoClicked()
    {
        SwitchContent(videoContent);
    }

    public void OnAudioClicked()
    {
        SwitchContent(audioContent);
    }

    public void OnControlsClicked()
    {
        SwitchContent(controlsContent);
    }

    private void SwitchContent(GameObject newContent)
    {
        if (currentContent == newContent)
            return; // already on this tab

        // Hide the current panel and show the new one
        currentContent.SetActive(false);
        newContent.SetActive(true);

        // Update reference
        currentContent = newContent;
    }
}