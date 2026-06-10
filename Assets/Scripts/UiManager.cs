using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
	private int selectedPageIndex = 0;

	[SerializeField] private CanvasGroup splashCanvasGroup;
	[SerializeField] private CanvasGroup headerCanvasGroup;

	[SerializeField] private VerticalLayoutGroup menuTabsVLG;
	[SerializeField] private Button[] tabs;
	[SerializeField] private CanvasGroup[] pages;
	[SerializeField] private RectTransform backButton;

	[SerializeField] private float tweenDuration = .3f;
	[SerializeField] private float delayBetweenTweens = .1f;

	private Sequence showTabsSequence;
	private Sequence hideTabsSequence;

	private List<Sequence> tabTweens = new();
	private List<Sequence> pageTweens = new();

	private float tabWidth = 0;
	private float pageWidth = 0;
	private float backButtonWidth = 0;

	public void Init()
	{
		DOTween.Init();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//this.splashCanvasGroup.gameObject.SetActive(true);

#if UNITY_EDITOR
		//check 32 or 64 bit
		if (IntPtr.Size == 4)
            Debug.Log("32 Bit");
        else
            Debug.Log("64 Bit");
#endif
		Invoke(nameof(InitTweens), .1f);
	}

	private void InitTweens()
    {
		this.menuTabsVLG.enabled = false;

		var offset = 50f;

        this.tabWidth = this.tabs[0].GetComponent<RectTransform>().rect.width + offset;
        this.pageWidth = this.pages[0].GetComponent<RectTransform>().rect.width + offset;
        this.backButtonWidth = this.backButton.GetComponent<RectTransform>().rect.width + offset;

		this.backButton.anchoredPosition = new Vector2(-this.backButtonWidth, this.backButton.anchoredPosition.y);

        foreach (var page in this.pages)
        {
            page.gameObject.SetActive(false);
            page.transform.localPosition = new Vector3(-this.pageWidth, page.transform.localPosition.y, 0);
        }
    }

    private void PlayHideTabsTween()
    {
        this.hideTabsSequence = DOTween.Sequence();
		this.hideTabsSequence.Insert(0, this.headerCanvasGroup.DOFade(0, this.tweenDuration));

		var delay = 0f;

		foreach (var tab in this.tabs)
		{
			this.hideTabsSequence.Insert(delay, tab.transform.DOLocalMoveX(-this.tabWidth, this.tweenDuration));
			delay += this.delayBetweenTweens;
		}

		this.hideTabsSequence.InsertCallback(delay - (2 * this.delayBetweenTweens),() =>
		{
			this.pages[this.selectedPageIndex].gameObject.SetActive(true);
			this.pages[this.selectedPageIndex].transform.DOLocalMoveX(0, this.tweenDuration);
			this.pages[this.selectedPageIndex].DOFade(1f, this.tweenDuration);
		});

		var backPos = new Vector2(0, this.backButton.anchoredPosition.y);
		this.hideTabsSequence.Insert(this.delayBetweenTweens * 3f, this.backButton.DOAnchorPos(backPos, this.tweenDuration));

        this.hideTabsSequence.Play();
    }

    private void PlayShowTabsTween()
    {
        this.showTabsSequence = DOTween.Sequence();
		this.showTabsSequence.Insert(0, this.headerCanvasGroup.DOFade(1, this.tweenDuration));
		
		var delay = 0f;

        foreach (var tab in this.tabs)
        {
			this.showTabsSequence.Insert(delay, tab.transform.DOLocalMoveX(0, this.tweenDuration));
			delay += this.delayBetweenTweens;
        }

        var backPos = new Vector2(-this.backButtonWidth, this.backButton.anchoredPosition.y);
		this.hideTabsSequence.Insert(delay - this.delayBetweenTweens, this.backButton.DOAnchorPos(backPos, this.tweenDuration));
        this.showTabsSequence.Play();
    }

    public void HandleBackButtonPress()
	{
		PlayShowTabsTween();

		this.pages[this.selectedPageIndex].transform.DOLocalMoveX(-this.pageWidth, this.tweenDuration).OnComplete(() =>
		{
			this.pages[this.selectedPageIndex].gameObject.SetActive(false);
		});

		this.pages[this.selectedPageIndex].DOFade(0, this.tweenDuration);

		//if( this.pages[this.selectedPageIndex].TryGetComponent<VideoPage>(out var vp)) vp.StopVideo();
	}

	public void SelectPage(int index)
	{
		this.selectedPageIndex = index;

		PlayHideTabsTween();
	}

	public void PlaySequence(int index)
	{

	}

	public void HandleSplashPageClick()
	{
		this.splashCanvasGroup.DOFade(0, this.tweenDuration).OnComplete(() => this.splashCanvasGroup.gameObject.SetActive(false));
	}
}