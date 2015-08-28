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
			Items.Add(Random);

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

		public Random Random
		{
			get
			{
				return m_Random;
			}
		}

		List<ToolItem> Items
		{
			get;
			set;
		}

		[SerializeField]
		Random m_Random;
	}
}
