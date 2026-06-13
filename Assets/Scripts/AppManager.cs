using UnityEngine;

[RequireComponent(typeof(UiManager))]
[RequireComponent(typeof(SequenceRandomizer))]
[RequireComponent(typeof(SaveManager))]
public class AppManager : MonoBehaviour
{
	public static int CurrentSequenceIndex = -1;
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

		var isFirstUse = this.saveManager.CheckIfFirstUse();

		if (isFirstUse)
		{
			this.sequenceRandomizer.GenerateRandomPatterns();
			CurrentSequenceIndex = -1;
			Debug.Log($"<color=#00FF00>First Use - GenerateRandomPatterns()</color>");
		}
		else
		{
			this.sequenceRandomizer.LoadSequences();
			CurrentSequenceIndex = this.saveManager.GetSequenceIndex();
			Debug.Log($"<color=red>Not First Use - LoadSequences().  CurrentSequenceIndex = {CurrentSequenceIndex}</color>");
		}
		
		this.uiManager.HowThisWorksTab.SetSelected(isFirstUse);

		this.saveManager.SetFirstUse();

		this.uiManager.SetTabStatesToIndex(CurrentSequenceIndex);
    }
}