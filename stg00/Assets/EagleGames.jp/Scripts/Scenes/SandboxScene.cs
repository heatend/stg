using UnityEngine;
using System.Collections;

namespace EagleGames.Scene
{
	public class SandboxScene : MonoBehaviour
	{

		// Use this for initialization
		void Start()
		{
			var math = Toolbox.Instance.Math;

			Debug.Log("Sin -------------------------");
			for (int i = 0; i < 360; i++)
			{
				Debug.Log(math.Sin(i));
			}

			Debug.Log("Cos -------------------------");
			for (int i = 0; i < 360; i++)
			{
				Debug.Log(math.Cos(i));
			}
		}

		// Update is called once per frame
		void Update()
		{
		}
	}

}
