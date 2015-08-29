using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace EagleGames.Em
{
	public class Command
	{
		public Command(IEnumerator coroutine, Action onFuncEnd)
		{
			Coroutine = coroutine;
			OnFuncEnd = onFuncEnd;
		}

		public IEnumerator Execute(MonoBehaviour executer)
		{
			yield return executer.StartCoroutine(Coroutine);
			OnFuncEnd();
		}

		IEnumerator Coroutine
		{
			get;
			set;
		}

		Action OnFuncEnd
		{
			get;
			set;
		}
	}
	public class CommandExecuter
	{
		public CommandExecuter(MonoBehaviour behavior)
		{
			CommandQueue = new Queue<Command>();
			CoroutineExecuter = behavior;
		}

		public void Push(IEnumerator coroutine, Action onFinished)
		{
			Action hook = delegate ()
			{
				OnFuncEnd();
				onFinished();
			};
			var command = new Command(coroutine, hook);
			CommandQueue.Enqueue(command);
		}

		public void Update()
		{
			if (CommandQueue.Count == 0)
			{
				return;
			}

			if (CurCommand != null)
			{
				return;
			}

			CurCommand = CommandQueue.Dequeue();

			CoroutineExecuter.StartCoroutine(CurCommand.Execute(CoroutineExecuter));
		}

		Command CurCommand
		{
			get;
			set;
		}

		void OnFuncEnd()
		{
			CurCommand = null;
		}

		Queue<Command> CommandQueue
		{
			get;
			set;
		}

		MonoBehaviour CoroutineExecuter
		{
			get;
			set;
		}
	}
	public abstract class EmBase : MonoBehaviour
	{
		protected abstract void OnAwake();
		protected abstract void OnStart();

		protected void PushCommand(IEnumerator coroutine, Action onFinished)
		{
			Cmd.Push(coroutine, onFinished);
		}

		CommandExecuter Cmd
		{
			get;
			set;
		}

		void Awake()
		{
			Cmd = new CommandExecuter(this);
			OnAwake();
		}

		// Use this for initialization
		void Start()
		{
			OnStart();
		}

		// Update is called once per frame
		void Update()
		{
			Cmd.Update();
		}

		protected float DeltaTime
		{
			get
			{
				return Toolbox.Instance.Time.DeltaTime;
			}
		}
	}
}
