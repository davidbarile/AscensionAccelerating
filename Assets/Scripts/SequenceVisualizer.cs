using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SequenceVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private Image imageA, imageB;

    [SerializeField] private Image[] testImages;

    [SerializeField] private PatternsConfig patternsConfig;

    private int[] sequence = new int[0];
    public void PlaySequence(int inIndex)
    {
        if (inIndex == 0)
            this.headerText.text = $"Opening Sequence";
        else
            this.headerText.text = $"Sequence {inIndex}";

        this.sequence = SaveManager.IN.GetSequence($"Sequence_{inIndex}");
        
        for(int i = 0; i < this.testImages.Length; ++i)
        {
            var shouldShow = i < this.sequence.Length;

            var img = this.testImages[i];
            img.gameObject.SetActive(shouldShow);

            if(shouldShow)
            {
                var index = this.sequence[i];
                img.sprite = this.patternsConfig.PatternDatas[index].Sprite;
            }
        }
    }
}