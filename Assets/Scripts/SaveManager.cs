using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager IN;
    private readonly string hasOpenedApp = "HasOpenedApp";
    private readonly string sequenceIndex = "SequenceIndex";
    private readonly string volume = "Volume";
    private readonly string dateInt = "DateInt";

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
    
    public void SaveVolume(float inVolume)
    {
        PlayerPrefs.SetFloat(this.volume, inVolume);
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(this.volume);
    }
    
    public void SaveDate(int inDateInt)
    {
        PlayerPrefs.SetInt(this.dateInt, inDateInt); 
    }

    public int GetDateInt()
    {
        return PlayerPrefs.GetInt(this.dateInt);
    }
}