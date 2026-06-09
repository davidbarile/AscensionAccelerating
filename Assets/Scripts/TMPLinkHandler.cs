using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TMPLinkHandler : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text p_TextMeshPro;

    void Awake()
    {
        p_TextMeshPro = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Find which link index was clicked based on the pointer screen position
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(p_TextMeshPro, eventData.position, eventData.pressEventCamera);

        // If a valid link index was clicked (-1 means nothing was clicked)
        if (linkIndex != -1)
        {
            // Get information about the specific link
            TMP_LinkInfo linkInfo = p_TextMeshPro.textInfo.linkInfo[linkIndex];
            string linkId = linkInfo.GetLinkID();

            // Handle the link action
            HandleLinkClick(linkId);
        }
    }

    private void HandleLinkClick(string linkId)
    {
        // OPTION A: If it's a web URL, open it in a browser
        if (linkId.StartsWith("http://") || linkId.StartsWith("https://") || linkId.StartsWith("mailto:"))
        {
            Application.OpenURL(linkId);
            Debug.Log($"Opening URL: {linkId}");
        }
    }
}