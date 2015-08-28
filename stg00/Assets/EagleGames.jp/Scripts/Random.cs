using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EagleGames
{
	[Serializable]
	public class Random : ToolItem
	{
		public int Int
		{
			get
			{
				var randomValue = Table[Index];
				IncrementIndex();
				return randomValue;
			}
		}

		public int Seed
		{
			get
			{
				return m_Seed;
			}

			set
			{
				m_Seed = value;
				Index = CalcIndex(m_Seed);
			}
		}

		public override void OnAwake()
		{
			Table = new List<int>();
		}

		public override void OnStart()
		{
			var nums = TextTable.text.Split(Environment.NewLine.ToCharArray());
			Table.AddRange(nums.Select(s => int.Parse(s)));
			Index = CalcIndex(Seed);
		}

		private int CalcIndex(int seed)
		{
			return seed % Table.Count;
		}

		private void IncrementIndex()
		{
			++Index;
			if (Index >= Table.Count)
			{
				Index = 0;
			}
		}

		List<int> Table
		{
			get;
			set;
		}

		int Index
		{
			get;
			set;
		}

		TextAsset TextTable
		{
			get { return m_TextTable; }
		}

		[SerializeField]
		int m_Seed;

		[SerializeField]
		TextAsset m_TextTable;
	}
}
