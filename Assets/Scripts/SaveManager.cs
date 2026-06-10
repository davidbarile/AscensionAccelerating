using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager IN;
    private readonly string hasOpenedApp = "HasOpenedApp";
    private readonly string sequenceIndex = "SequenceIndex";

    public bool CheckIfFirstUse()
    {
        return !PlayerPrefsX.GetBool(this.hasOpenedApp);
    }

    public void SetFirstUse()
    {
        PlayerPrefsX.SetBool(this.hasOpenedApp, true);
    }

    public void SaveSequence(string inName, int[] inSequence)
    {
        PlayerPrefsX.SetIntArray(inName, inSequence);
    }

    public int[] GetSequence(string inName)
    {
        return PlayerPrefsX.GetIntArray(inName);
    }

    public void SaveSequenceIndex(int inIndex)
    {
        PlayerPrefs.SetInt(this.sequenceIndex, inIndex);
    }
    
    public int GetSequenceIndex()
    {
        return PlayerPrefs.GetInt(this.sequenceIndex);
    }
}