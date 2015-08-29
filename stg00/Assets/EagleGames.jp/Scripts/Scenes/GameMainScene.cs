using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EagleGames.Scene
{
	public class GameMainScene : MonoBehaviour
	{
		public void OnButtonDown()
		{
			Debug.Log("乱数: " + Toolbox.Instance.Random.Int + "を消費");
		}

		// Use this for initialization
		void Start()
		{
			Application.targetFrameRate = FrameRate;
		}

		// Update is called once per frame
		void Update()
		{
			RandomValue.text = Toolbox.Instance.Random.Int.ToString("000");
		}

		Text RandomValue
		{
			get { return m_RandomValue; }
		}

		int FrameRate
		{
			get { return m_FrameRate; }
		}

		[SerializeField]
		int m_FrameRate = 30;

		[SerializeField]
		Text m_RandomValue;
	}
}
