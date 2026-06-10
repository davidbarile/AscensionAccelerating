using UnityEngine;

[RequireComponent(typeof(UiManager))]
[RequireComponent(typeof(SequenceRandomizer))]
[RequireComponent(typeof(SaveManager))]
public class AppManager : MonoBehaviour
{
	private UiManager uiManager;
	private SequenceRandomizer sequenceRandomizer;
	private SaveManager saveManager;

	private void Awake()
	{
		this.uiManager = GetComponent<UiManager>();
		this.sequenceRandomizer = GetComponent<SequenceRandomizer>();
		this.saveManager = GetComponent<SaveManager>();

		SaveManager.IN = this.saveManager;
	}

    private void Start()
    {
		this.uiManager.Init();

		this.sequenceRandomizer.SetOpeningPattern();

		if (this.saveManager.CheckIfFirstUse())
		{
			Debug.Log($"<color=#00FF00>First Use - GenerateRandomPatterns()</color>");
			this.sequenceRandomizer.GenerateRandomPatterns();
		}
		else
		{
			Debug.Log($"<color=red>Not First Use - LoadSequences()</color>");
			this.sequenceRandomizer.LoadSequences();
		}

		this.saveManager.SetFirstUse();
    }
}