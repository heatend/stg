using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EagleGames.Em
{
	public class Em00 : EmBase
	{
		List<Func<IEnumerator>> ActionTable
		{
			get;
			set;
		}
		protected override void OnAwake()
		{
			ActionTable = new List<Func<IEnumerator>>();
			ActionTable.Add(MoveForward);
			ActionTable.Add(MoveBack);
			ActionTable.Add(MoveRight);
			ActionTable.Add(MoveLeft);
		}

		protected override void OnStart()
		{
			PushCommand(MoveForward(), LotAction);
		}

		void LotAction()
		{
			var index = Toolbox.Instance.Random.Range(
				Enumerable.Range(  0, 64),
				Enumerable.Range( 64, 64),
				Enumerable.Range(128, 64),
				Enumerable.Range(192, 64)
				);

			var action = ActionTable[index];
			PushCommand(action(), LotAction);
		}

		IEnumerator MoveForward()
		{
			var elapsedSec = 0f;
			while (elapsedSec < MoveSec)
			{
				Translate(Vector3.forward, ref elapsedSec);
				yield return null;
			}
		}

		IEnumerator MoveBack()
		{
			var elapsedSec = 0f;
			while (elapsedSec < MoveSec)
			{
				Translate(Vector3.back, ref elapsedSec);
				yield return null;
			}
		}

		IEnumerator MoveRight()
		{
			var elapsedSec = 0f;
			while (elapsedSec < MoveSec)
			{
				Translate(Vector3.right, ref elapsedSec);
				yield return null;
			}
		}

		IEnumerator MoveLeft()
		{
			var elapsedSec = 0f;
			while (elapsedSec < MoveSec)
			{
				Translate(Vector3.left, ref elapsedSec);
				yield return null;
			}
		}

		void Translate(Vector3 dir, ref float elapsedSec)
		{
			var t = elapsedSec / MoveSec;
			var accel = Accel.Evaluate(1f - (t * t));
			transform.Translate(dir * DeltaTime * accel * Speed);

			elapsedSec += DeltaTime;
		}

		AnimationCurve Accel
		{
			get { return m_Accel; }
		}
		[SerializeField]
		AnimationCurve m_Accel;

		float MoveSec
		{
			get { return m_MoveSec; }
		}
		[SerializeField]
		float m_MoveSec = 3f;

		float Speed
		{
			get { return m_Speed; }
		}
		[SerializeField]
		float m_Speed = 1f;
	}
}
