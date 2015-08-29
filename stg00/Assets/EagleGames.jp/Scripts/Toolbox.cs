using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EagleGames
{
	public class ToolItem
	{
		public virtual void OnAwake() { }
		public virtual void OnStart() { }
	}

	public class Toolbox : Sys.Singleton<Toolbox>
	{
		void Awake()
		{
			Items = new List<ToolItem>();

			AddTool(Time);
			AddTool(Random);

			foreach (var item in Items)
			{
				item.OnAwake();
			}
		}

		void Start()
		{
			foreach (var item in Items)
			{
				item.OnStart();
			}
		}

		void AddTool(ToolItem item)
		{
			Items.Add(item);
		}

		List<ToolItem> Items
		{
			get;
			set;
		}

		// 時間関係
		public Sys.Time Time
		{
			get { return m_Time; }
		}

		[SerializeField]
		Sys.Time m_Time;

		// ランダムまわり
		public Random Random
		{
			get
			{
				return m_Random;
			}
		}

		[SerializeField]
		Random m_Random;
	}
}
