using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EagleGames
{
	public enum LeverState
	{
		None,
		Up,
		Down,
		Left,
		Right,
		UpL,
		UpR,
		DownL,
		DownR,
	}

	public enum ButtonState
	{
		Off,
		On,
	}
}
namespace EagleGames.Sys
{
	class PadState
	{
		public PadState()
		{
			LeverInput = LeverState.None;
			LeverTilt = 0;
			FireButton = ButtonState.Off;
		}

		public LeverState LeverInput
		{
			get;
			set;
		}

		/// <summary>
		/// レバーの傾き具合 0.0 - 1.0
		/// </summary>
		public float LeverTilt
		{
			get;
			set;
		}

		public ButtonState FireButton
		{
			get;
			set;
		}

		public static PadState None
		{
			get
			{
				return m_None;
			}
		}
		static PadState m_None = new PadState();

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (GetType() != obj.GetType())
			{
				return false;
			}

			var other = (PadState)obj;
			return this == other;
		}

		public override int GetHashCode()
		{
			return LeverInput.GetHashCode() ^ FireButton.GetHashCode();
		}

		public static bool operator ==(PadState a, PadState b)
		{
			if (ReferenceEquals(a, b) == true)
			{
				return true;
			}

			if (a == null || b == null)
			{
				return false;
			}

			if (a.FireButton != b.FireButton)
			{
				return false;
			}

			if (a.LeverInput != b.LeverInput)
			{
				return false;
			}

			return true;
		}

		public static bool operator !=(PadState a, PadState b)
		{
			return !(a == b);
		}
	}

	class PadInputHistory
	{
		public PadInputHistory(int size)
		{
			History		= new LinkedList<PadState>();
			Capacity	= size;

			for (int i = 0; i < Capacity; i++)
			{
				History.AddLast(PadState.None);
			}
		}


		public void Push(PadState history)
		{
			if (History.Count >= Capacity)
			{
				History.RemoveFirst();
			}

			History.AddLast(history);
		}

		/// <summary>
		/// 最新の入力ログ
		/// </summary>
		public PadState Latest
		{
			get
			{
				return History.Last.Value;
			}
		}

		LinkedList<PadState> History
		{
			get;
			set;
		}

		int Capacity
		{
			get;
			set;
		}
	}
	public class Input : ToolItem
	{
		public override void OnAwake()
		{
			PadHistory	= new PadInputHistory(PadHistorySize);
			CurInput	= new PadState();
		}
		public override void OnUpdate()
		{
			PadHistory.Push(CurInput);

			CurInput = DetectInput();
		}

		private PadState DetectInput()
		{
			var state = new PadState();

			// 発射ボタン
			var isFireButtonDown = UnityEngine.Input.GetButtonDown("Fire1");
			if (isFireButtonDown == true)
			{
				state.FireButton = ButtonState.On;
			}

			// レバー状態
			var hAxis = UnityEngine.Input.GetAxis("Horizontal");
			var vAxis = UnityEngine.Input.GetAxis("Vertical");
			var leverState = CalcLeverState(hAxis, vAxis);

			return state;
		}

		private LeverState CalcLeverState(float hAxis, float vAxis)
		{
			// 正規化した入力
			var tilt = new Vector2(hAxis, vAxis).normalized;

			var math = Toolbox.Instance.Math;

			if (tilt.x > math.Cos(30) && Mathf.Abs(tilt.y) <= math.Sin(30))
			{
				return LeverState.Right;
			}

			if (	(tilt.x > math.Cos(60) && tilt.x <= math.Cos(30))
				&&	(tilt.y > math.Sin(30) && tilt.y <= math.Sin(60))
				)
			{
				return LeverState.UpR;
			}
			return LeverState.DownR;
		}

		PadInputHistory PadHistory
		{
			get;
			set;
		}
		PadState CurInput
		{
			get;
			set;
		}


		int PadHistorySize
		{
			get { return m_PadHistorySize; }
		}

		[SerializeField]
		int m_PadHistorySize = 30;
	}

}
