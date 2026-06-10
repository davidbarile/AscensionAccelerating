using System.Linq;
using UnityEngine;

public class SequenceRandomizer : MonoBehaviour
{
    [SerializeField] private int maxPatterns = 5;
    [SerializeField] private SequenceTab openingSequenceTab;
    [SerializeField] private SequenceTab[] sequenceTabs;

    private int[] openingSequence = { 6, 5, 4, 3, 2 };//set custom order here
    private int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };

    public void SetOpeningPattern()
    {
        var openingSequence = this.openingSequence.ToList();
        this.openingSequenceTab.SetPatterns(openingSequence);
    }

    public void GenerateRandomPatterns()
    {
        SetOpeningPattern();

        var availableFirstNumbers = this.numbers.ToList().CreateRandomizedList<int>();

        if (this.sequenceTabs.Length > availableFirstNumbers.Count)
        {
            Debug.LogWarning($"Cannot assign unique first numbers to {this.sequenceTabs.Length} tabs. Maximum unique starts is {availableFirstNumbers.Count}.");
        }

        int sequenceLength = Mathf.Min(this.maxPatterns, this.numbers.Length);

        for (int i = 0; i < this.sequenceTabs.Length; ++i)
        {
            var tab = this.sequenceTabs[i];
            tab.Numbers.Clear();

            int firstNumber = availableFirstNumbers[i % availableFirstNumbers.Count];
            var remainingNumbers = this.numbers.Where(n => n != firstNumber).ToList().CreateRandomizedList<int>();
            var patternNumbers = new System.Collections.Generic.List<int> { firstNumber };
            patternNumbers.AddRange(remainingNumbers.Take(sequenceLength - 1));

            tab.SetNumbers(patternNumbers);
        }

        foreach (var tab in this.sequenceTabs)
        {
            tab.ApplyNumbers();
        }
    }
}