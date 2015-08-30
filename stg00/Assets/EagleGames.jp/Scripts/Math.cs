using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EagleGames
{
	[Serializable]
	public class Math : ToolItem
	{
		public override void OnAwake()
		{
			CreateSinTable();
		}

		public float Sin(int degree)
		{
			var ndegree = NormalizeDegree(degree);
			var index	= CalcSinTableIndex(ndegree);
			int sign	= CalcSign(ndegree);
			return sign * SinTable[index];
		}

		private int NormalizeDegree(int degree)
		{
			while (degree < 0)
			{
				degree += 360;
			}

			while (degree >= 360)
			{
				degree -= 360;
			}

			return degree;
		}

		public float Cos(int degree)
		{
			return Sin(degree + 90);
		}

		private int CalcSinTableIndex(int degree)
		{
			if (degree >= 90 && degree < 180)
			{
				return 180 - degree;
			}

			if (degree >= 180 && degree < 270)
			{
				return degree - 180;
			}

			if (degree >= 270 && degree < 360)
			{
				return 360 - degree;
			}

			return degree;
		}

		private int CalcSign(int degree)
		{
			if (degree >= 180 && degree < 360)
			{
				return -1;
			}
			return 1;
		}

		private void CreateSinTable()
		{
			SinTable = new List<float>();
			for (int i = 0; i <= 90; i++)
			{
				SinTable.Add(Mathf.Sin(i));
			}
		}

		List<float> SinTable
		{
			get;
			set;
		}
	}

}
