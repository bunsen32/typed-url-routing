// -----------------------------------------------------------------------
// <copyright file="TaskAsyncHelper{T}.cs" company="Andrew Forrest">©2022 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dysphoria.Net.UrlRouting.Handlers
{
	/// <summary>
	/// Cribbed from <see cref="System.Web.TaskAsyncHelper"/>
	/// </summary>
	internal static class TaskAsyncHelper<T>
	{
		internal static IAsyncResult BeginTask(
			Func<Task<T>> taskFunc,
			AsyncCallback callback,
			object state)
		{
			var task = taskFunc();
			if (task == null)
				return null;
			var isCompleted = task.IsCompleted;
			var result = new TaskWrapperAsyncResult<T>(task, state, isCompleted);
			if (callback == null) return result;
			
			if (isCompleted)
				callback(result);
			else
				task.ContinueWith(_ => callback(result));
			return result;
		}

		internal static T EndTask(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
				throw new ArgumentNullException(nameof (asyncResult));
			if (asyncResult is not TaskWrapperAsyncResult<T> wrapperAsyncResult)
				throw new ArgumentException($"Given IAsyncResult is not a wrapped task ({asyncResult.GetType().FullName})", nameof (asyncResult));
			return wrapperAsyncResult.Task.GetAwaiter().GetResult();
		}
	}
	
	internal sealed class TaskWrapperAsyncResult<T> : IAsyncResult
	{
		private bool _wasCompletedSynchronously;

		internal TaskWrapperAsyncResult(Task<T> task, object asyncState, bool wasCompletedSynchronously)
		{
			Task = task;
			AsyncState = asyncState;
			_wasCompletedSynchronously = wasCompletedSynchronously;
		}

		internal Task<T> Task { get; }

		public object AsyncState { get; }

		public WaitHandle AsyncWaitHandle => ((IAsyncResult) Task).AsyncWaitHandle;

		public bool CompletedSynchronously => _wasCompletedSynchronously || ((IAsyncResult) Task).CompletedSynchronously;

		public bool IsCompleted => Task.IsCompleted;
	}
}