using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SequenceVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private Transform[] patternParents;

    [SerializeField] private bool debugMode;

    private List<PatternVisualization> patternVisualizations = new();

    [SerializeField] private Image[] testImages;

    [SerializeField] private PatternsConfig patternsConfig;

    [Range(0f, 5f), SerializeField] private float tweenDuration = 1f;

    private int[] sequence = new int[0];
    private PatternVisualization activePattern = null;

    public void InitSequence(int inIndex)
    {
        if (inIndex == 0)
            this.headerText.text = $"Opening Sequence";
        else
            this.headerText.text = $"Sequence {inIndex}";

        this.sequence = SaveManager.IN.GetSequence($"Sequence_{inIndex}");

        DestroyAllVisualizations();

        //spawn new visualizations
        for (int i = 0; i < this.patternParents.Length; ++i)
        {
            this.patternParents[i].GetComponent<CanvasGroup>().alpha = 1f;

            if (i < this.sequence.Length)
            {
                var index = this.sequence[i];
                var patternData = this.patternsConfig.PatternDatas[index];

                PatternVisualization patternVis = Instantiate(patternData.Prefab, this.patternParents[i]);
                patternVis.AudioSource.clip = patternData.AudioClip;

                this.patternVisualizations.Add(patternVis);
            }
        }

        // for(int i = 0; i < this.testImages.Length; ++i)
        // {
        //     var shouldShow = i < this.sequence.Length;

        //     var img = this.testImages[i];
        //     img.gameObject.SetActive(shouldShow);

        //     if(shouldShow)
        //     {
        //         var index = this.sequence[i];
        //         img.sprite = this.patternsConfig.PatternDatas[index].Sprite;
        //     }
        // }

        StopAllCoroutines();
        StartCoroutine(PlaySequence());
    }


    private IEnumerator PlaySequence()
    {
        var delay = this.debugMode ? 0.2f : this.tweenDuration;

        for (int i = 0; i < this.patternVisualizations.Count; ++i)
        {
            this.activePattern = this.patternVisualizations[i];

            this.activePattern.AudioSource.volume = 0f;
            this.activePattern.Play();

            this.activePattern.AudioSource.DOFade(1f, delay).SetEase(Ease.InSine);

            var clipDuration = this.debugMode ? 2f : this.activePattern.AudioSource.clip.length;

            yield return new WaitForSeconds(clipDuration - delay);

            this.activePattern.AudioSource.DOFade(0f, delay);

            this.patternParents[i].GetComponent<CanvasGroup>().DOFade(0f, delay).SetEase(Ease.OutSine);

            yield return new WaitForSeconds(delay);
            this.activePattern.Stop();
        }
    }

    //called from back button
    public void Stop()
    {
        StopAllCoroutines();

        if (this.activePattern != null)
        {
            this.activePattern.AudioSource.DOFade(0f, 1f).OnComplete(() => this.activePattern.Stop());
        }
    }
    
    private void DestroyAllVisualizations()
    {
         for (int i = 0; i < this.patternVisualizations.Count; ++i)
        {
            if (this.patternVisualizations[i] != null)
                Destroy(this.patternVisualizations[i].gameObject);
        }

        this.patternVisualizations.Clear();
    }
}