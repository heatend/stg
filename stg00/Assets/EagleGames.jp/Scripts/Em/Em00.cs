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

			while (elapsedSec < 3f)
			{
				transform.Translate(Vector3.forward * Time.deltaTime);

				elapsedSec += Time.deltaTime;
				yield return null;
			}
		}

		IEnumerator MoveBack()
		{
			var elapsedSec = 0f;
			while (elapsedSec < 3f)
			{
				transform.Translate(Vector3.back * Time.deltaTime);

				elapsedSec += Time.deltaTime;
				yield return null;
			}
		}

		IEnumerator MoveRight()
		{
			var elapsedSec = 0f;
			while (elapsedSec < 3f)
			{
				transform.Translate(Vector3.right * Time.deltaTime);

				elapsedSec += Time.deltaTime;
				yield return null;
			}
		}

		IEnumerator MoveLeft()
		{
			var elapsedSec = 0f;
			while (elapsedSec < 3f)
			{
				transform.Translate(Vector3.left * Time.deltaTime);

				elapsedSec += Time.deltaTime;
				yield return null;
			}
		}
	}

}
