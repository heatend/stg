using UnityEngine;
using System.Collections;
using System;

namespace EagleGames.Sys
{
	[System.Serializable]
	public class Time : ToolItem
	{
		public override void OnAwake()
		{
			DeltaTime = 1f / 30f;
		}

		public float DeltaTime
		{
			get;
			private set;
		}
	}

}
