using System.Collections.Generic;
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
        tab.Numbers = openingSequenceList;
        tab.CreateHash();
        tab.SetPatterns( tab.Numbers);
    }

    public void GenerateRandomPatterns()
    {
        var hashes = new List<int>();
        bool isUnique = true;
        int nextRandomNum = 0;

        //loop thru all tabs
        for (int i = 1; i < this.sequenceTabs.Length; ++i)
        {
            var tab = this.sequenceTabs[i];
            tab.Numbers.Clear();

            //on each tab, loop until there are 5 numbers
            while (tab.Numbers.Count < this.maxPatterns)
            {
                nextRandomNum = this.numbers[Random.Range(0, this.numbers.Length)];//generate random number

                if (tab.Numbers.Count > 0)
                {
                    //check if the number is different than the previous index, and if it has already been used more than 2 times
                    while (tab.Numbers[tab.Numbers.Count - 1] == nextRandomNum || tab.Numbers.Count(n => n == nextRandomNum) >= 2)
                    {
                        nextRandomNum = this.numbers[Random.Range(0, this.numbers.Length)];
                    }
                }

                tab.Numbers.Add(nextRandomNum);
            }

            tab.CreateHash();

            //check if the pattern is unique
            if (!hashes.Contains(tab.OrderHash))
            {
                hashes.Add(tab.OrderHash);
            }
            else
            {
                isUnique = false;
                break;
            }
        }

        if(isUnique)
        {
            Debug.Log($"<color=#00FF00>All PatternsUnique</color>");
            foreach (var tab in this.sequenceTabs)
            {
                tab.ApplyNumbers();
            }

            SaveSequences();
        }
        else
        {
            //if not unique, regenerate
            Debug.Log($"<color=red>NOT Unique</color>");
            GenerateRandomPatterns();
        }
    }

    private void SaveSequences()
    {            
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