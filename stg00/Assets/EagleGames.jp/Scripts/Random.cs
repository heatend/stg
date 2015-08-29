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

		public int Range(params IEnumerable<int>[] ranges)
		{
			var rand = Int;
			foreach (var item in ranges)
			{
				if( item.Any(p => p == rand))
				{
					return Array.IndexOf(ranges, item);
				}
			}

			// 範囲内に適切な値が含まれてなかった
			throw new System.NotImplementedException();
		}

		public override void OnAwake()
		{
			Table = new List<int>();
		}

		public override void OnStart()
		{
			var separators = new string []{ Environment.NewLine };
			var nums = TextTable.text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
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
