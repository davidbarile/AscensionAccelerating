using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatternThumbnail : MonoBehaviour
{
    [SerializeField] private Image patternImage;
    [SerializeField] private TextMeshProUGUI indexText;
    [SerializeField] private PatternsConfig patternsConfig;
    public void SetPattern(int inIndex)
    {
        this.patternImage.sprite = this.patternsConfig.PatternDatas[inIndex].Sprite;
        this.indexText.text = $"{inIndex}";
    }
}