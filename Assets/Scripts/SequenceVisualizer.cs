using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SequenceVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Transform[] patternParents;
    [Space, SerializeField] private Slider volumeSlider;
    [Space, SerializeField] private PatternsConfig patternsConfig;
    [Range(0f, 5f), SerializeField] private float tweenDuration = 1f;
    [Space, SerializeField] private bool debugMode;

    private int[] sequence = new int[0];
    private PatternVisualization activePattern = null;
    private List<PatternVisualization> patternVisualizations = new();
    private List<string> endMessages = new();

    public void InitSequence(int inIndex)
    {
        if (inIndex == 0)
            this.headerText.text = $"Opening Sequence";
        else
            this.headerText.text = $"Sequence {inIndex}";

        RandomizeMessages(inIndex);

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

        StopAllCoroutines();
        StartCoroutine(PlaySequence());
    }

    private void RandomizeMessages(int inIndex)
    {
        if (this.endMessages.Count == 0)
        {
            this.endMessages.AddRange(this.patternsConfig.PatternCompleteMessages);
            this.endMessages.RandomizeList();
        }

        //add random end message
        if (inIndex == 5)
        {
            var rnd = Random.Range(0, this.patternsConfig.FinalMessages.Length);
            this.messageText.text = this.patternsConfig.FinalMessages[rnd];
        }
        else
        {
            this.messageText.text = this.endMessages[0];
            this.endMessages.RemoveAt(0);
        }
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

        ++AppManager.CurrentSequenceIndex;
        AppManager.CurrentSequenceIndex %= 6;
        SaveManager.IN.SaveSequenceIndex(AppManager.CurrentSequenceIndex);
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

    //called from UI Slider
    public void SetVolume(float inVolume)
    {
        AudioListener.volume = inVolume;
        SaveManager.IN.SaveVolume(inVolume);
    }

    public void SetVolumeSliderValue(float inValue)
    {
        this.volumeSlider.SetValueWithoutNotify(inValue);
    }

#region Debug
    public void DebugRandomizeMessages()
    {
        var rnd = Random.Range(0, 5);
        this.RandomizeMessages(rnd);
    }

    public void DebugRandomizeEndMessage()
    {
        this.RandomizeMessages(5);
    }

    public void DebugShowMessage(int inIndex)
    {
        this.messageText.text = this.patternsConfig.PatternCompleteMessages[inIndex];
    }

    public void DebugShowEndMessage(int inIndex)
    {
        this.messageText.text = this.patternsConfig.FinalMessages[inIndex];
    }
#endregion
}