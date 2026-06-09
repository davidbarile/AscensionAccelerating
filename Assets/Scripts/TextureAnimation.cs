using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextureAnimation : MonoBehaviour 
{
	public float 						PlaySpeed = 0;
	public int 							NumLoops = -1;
	public bool 						ShouldPlayOnAwake;

	private System.Action				_SoundEffectDelegate = null;
	private int							_FrameToPlaySoundDelegate = -1;
	private int 						_CurrentFrame = 0;
	private int 						_LoopsExecuted = 0;

	[SerializeField] private Image				_Image;
	[SerializeField] private List<Sprite>	 	_AnimationFrames;
	[Header("Auto Fill Mode:")]
	[SerializeField] private bool				_ShouldAutoFillArray;
	[SerializeField] private string				_Path = "";
	[SerializeField] private int				_IncrementAmount = 1;
	[SerializeField] private int				_TotalFrames = 100;
	
	private void Start()
	{
		if( _ShouldAutoFillArray )
		{
			_ShouldAutoFillArray = false;

			_AnimationFrames.Clear();

			int counter = 0;
			string fileName = _Path;

			while( counter < _TotalFrames )
			{
				fileName = _Path + counter.ToString( "0000" );
				//Debug.Log(fileName);

				var tex = (Sprite) Resources.Load( fileName );

				_AnimationFrames.Add( tex );

				counter += _IncrementAmount;
			}
		}

		if( ShouldPlayOnAwake )
		{
			_CurrentFrame = 0;
			_LoopsExecuted = 0;
			Play();
		}
	}

	public void Play()
	{
		CancelInvoke( "NextFrame" );
		InvokeRepeating( "NextFrame", 0, PlaySpeed );
	}

	public void Play( int inDelay )
	{
		CancelInvoke( "NextFrame" );
		InvokeRepeating( "NextFrame", inDelay, PlaySpeed );
	}

	public void Pause()
	{
		CancelInvoke( "NextFrame" );
	}

	public void Stop()
	{
		Pause();
		_LoopsExecuted = 0;
		_CurrentFrame = 0;
		GotoFrame( _CurrentFrame );
	}

	public void GotoFrame( int inFrame )
	{
		_Image.sprite = _AnimationFrames[ inFrame ];
	}
	
	private void NextFrame()
	{
		_Image.sprite = _AnimationFrames[ _CurrentFrame ];
		++_CurrentFrame;

		if( _CurrentFrame == _AnimationFrames.Count )
		{
			if( _LoopsExecuted < NumLoops || NumLoops < 0 )
			{
				++_LoopsExecuted;
				_CurrentFrame = 0;//loop around to 0
			}
			else
			{
				Stop();
			}
		}
	}
}
