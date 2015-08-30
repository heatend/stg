using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EagleGames
{
	public class ToolItem
	{
		public virtual void OnAwake() { }
		public virtual void OnStart() { }
		public virtual void OnUpdate() { }

		/// <summary>
		/// 実行優先順序
		/// </summary>
		public int Priority
		{
			get { return m_Priority; }
		}

		[SerializeField]
		int m_Priority = 0;
	}

	public class Toolbox : Sys.Singleton<Toolbox>
	{
		void Awake()
		{
			Items = new List<ToolItem>();

			AddTool(Math);
			AddTool(Time);
			AddTool(Random);

			Items.Sort((t1, t2) => {
				return t1.Priority - t2.Priority;
			});

			foreach (var item in Items)
			{
				item.OnAwake();
			}
		}

		void AddTool(ToolItem item)
		{
			Items.Add(item);
		}

		void Start()
		{
			foreach (var item in Items)
			{
				item.OnStart();
			}
		}


		List<ToolItem> Items
		{
			get;
			set;
		}

		// 数学関係
		public Math Math
		{
			get { return m_Math; }
		}
		[SerializeField]
		Math m_Math;

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
