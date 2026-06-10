using System.Linq;
using UnityEngine;

public class SequenceRandomizer : MonoBehaviour
{
    [SerializeField] private int maxPatterns = 5;
    [SerializeField] private SequenceTab openingSequenceTab;
    [SerializeField] private SequenceTab[] sequenceTabs;

    //0 = Aqua, 1 = Blue, 2 = Cyan, 3 = Green, 4 = Lavender, 5 = Magenta, 6 = Violet
    private readonly int[] openingSequence = { 3, 4, 1, 0, 2, 5 };//green, lavender, blue, aqua, cyan, magenta
    private readonly int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };

    public void SetOpeningPattern()
    {
        var openingSequenceList = this.openingSequence.ToList();
        var tab = this.sequenceTabs[0];
        tab.SetNumbers(openingSequenceList);
        tab.SetPatterns(openingSequenceList);
    }

    public void GenerateRandomPatterns()
    {
        var availableFirstNumbers = this.numbers.ToList().CreateRandomizedList<int>();

        if (this.sequenceTabs.Length > availableFirstNumbers.Count)
        {
            Debug.LogWarning($"Cannot assign unique first numbers to {this.sequenceTabs.Length} tabs. Maximum unique starts is {availableFirstNumbers.Count}.");
        }

        int sequenceLength = Mathf.Min(this.maxPatterns, this.numbers.Length);

        for (int i = 1; i < this.sequenceTabs.Length; ++i)
        {
            var tab = this.sequenceTabs[i];
            tab.Numbers.Clear();

            int firstNumber = availableFirstNumbers[i % availableFirstNumbers.Count];
            var remainingNumbers = this.numbers.Where(n => n != firstNumber).ToList().CreateRandomizedList<int>();
            var patternNumbers = new System.Collections.Generic.List<int> { firstNumber };
            patternNumbers.AddRange(remainingNumbers.Take(sequenceLength - 1));

            var clampedNumbers = patternNumbers.GetRange(0, 5);
            tab.SetNumbers(clampedNumbers);
        }

        foreach (var tab in this.sequenceTabs)
        {
            tab.ApplyNumbers();
        }

        SaveSequences();
    }

    private void SaveSequences()
    {
        //SaveManager.IN.SaveSequence($"Sequence_0", this.openingSequenceTab.Numbers.ToArray());
            
        for (int i = 0; i < this.sequenceTabs.Length; ++i)
        {
            var tab = this.sequenceTabs[i];
            SaveManager.IN.SaveSequence($"Sequence_{i}", tab.Numbers.ToArray());
        }
    }

    public void LoadSequences()
    {
        for (int i = 0; i < this.sequenceTabs.Length; ++i)
        {
            var tab = this.sequenceTabs[i];
            var savedSequence = SaveManager.IN.GetSequence($"Sequence_{i}");
            tab.Numbers = savedSequence.ToList();
            tab.ApplyNumbers();
        }
    }
}