﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Streams;
using Reactor.Core;
using System.Threading;
using Reactor.Core.flow;
using Reactor.Core.subscription;
using Reactor.Core.util;

namespace Reactor.Core.util
{
    /// <summary>
    /// Helper methods to work with the regular queue-drain serialization approach
    /// </summary>
    public static class QueueDrainHelper
    {
        /// <summary>
        /// Atomically increment the work-in-progress counter and return true if
        /// it transitioned from 0 to 1.
        /// </summary>
        /// <param name="wip">The work-in-progress field</param>
        /// <returns>True if the counter transitioned from 0 to 1</returns>
        public static bool Enter(ref int wip)
        {
            return Interlocked.Increment(ref wip) == 1;
        }

        /// <summary>
        /// Atomically try to decrement the work-in-progress counter and return
        /// its new value.
        /// </summary>
        /// <param name="wip">The target work-in-progress counter field</param>
        /// <param name="missed">The number to decrement the counter, positive (not verified)</param>
        /// <returns>The new work-in-progress value</returns>
        public static int Leave(ref int wip, int missed)
        {
            int w = Volatile.Read(ref wip);
            if (w == missed)
            {
                return Interlocked.Add(ref wip, -missed);
            }
            else
            {
                return w;
            }
        }

        /// <summary>
        /// Checks for a terminal condition and signals errors eagerly.
        /// </summary>
        /// <typeparam name="T">The input value type.</typeparam>
        /// <typeparam name="U">The output value type.</typeparam>
        /// <param name="cancelled">The cancelled field</param>
        /// <param name="done">The done field</param>
        /// <param name="error">The error field</param>
        /// <param name="actual">The receivin ISubscriber</param>
        /// <param name="queue">The queue to check for emptiness.</param>
        /// <param name="s">The subscription to upstream</param>
        /// <param name="d">The optional disposable.</param>
        /// <returns>True if a terminal state has been reached</returns>
        public static bool CheckTerminated<T, U>(ref bool cancelled, ref bool done, ref Exception error, 
            ISubscriber<T> actual, IQueue<U> queue, ISubscription s, IDisposable d)
        {
            if (Volatile.Read(ref cancelled))
            {
                queue.Clear();
                return true;
            }

            if (Volatile.Read(ref done))
            {
                Exception ex = Volatile.Read(ref error);
                if (ex != null)
                {
                    ex = ExceptionHelper.Terminate(ref error);
                    queue.Clear();
                    actual.OnError(ex);

                    d?.Dispose();
                    return true;
                }
                else
                {
                    bool empty;

                    try
                    {
                        empty = queue.IsEmpty();
                    }
                    catch (Exception exc)
                    {
                        ExceptionHelper.ThrowIfFatal(exc);

                        queue.Clear();
                        s.Cancel();

                        ExceptionHelper.AddError(ref error, exc);
                        exc = ExceptionHelper.Terminate(ref error);

                        actual.OnError(exc);

                        d?.Dispose();
                        return true;
                    }

                    if (empty)
                    {
                        actual.OnComplete();

                        d?.Dispose();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for a terminal condition and signals errors if the queue
        /// is also empty.
        /// </summary>
        /// <typeparam name="T">The input value type.</typeparam>
        /// <typeparam name="U">The output value type.</typeparam>
        /// <param name="cancelled">The cancelled field</param>
        /// <param name="done">The done field</param>
        /// <param name="error">The error field</param>
        /// <param name="actual">The receivin ISubscriber</param>
        /// <param name="queue">The queue to check for emptiness.</param>
        /// <param name="s">The subscription to upstream</param>
        /// <param name="d">The optional disposable.</param>
        /// <returns>True if a terminal state has been reached</returns>
        public static bool CheckTerminatedDelayed<T, U>(ref bool cancelled, ref bool done, ref Exception error, 
            ISubscriber<T> actual, IQueue<U> queue, ISubscription s, IDisposable d)
        {
            if (Volatile.Read(ref cancelled))
            {
                queue.Clear();
                return true;
            }

            if (Volatile.Read(ref done))
            {
                bool empty;

                try
                {
                    empty = queue.IsEmpty();
                }
                catch (Exception exc)
                {
                    ExceptionHelper.ThrowIfFatal(exc);

                    queue.Clear();
                    s.Cancel();

                    ExceptionHelper.AddError(ref error, exc);
                    exc = ExceptionHelper.Terminate(ref error);

                    actual.OnError(exc);

                    d?.Dispose();
                    return true;
                }

                if (empty)
                {
                    Exception ex = Volatile.Read(ref error);
                    if (ex != null)
                    {
                        ex = ExceptionHelper.Terminate(ref error);
                        actual.OnError(ex);
                    }
                    else
                    {
                        actual.OnComplete();
                    }

                    d?.Dispose();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Constructs a queue based on the prefetch value.
        /// </summary>
        /// <typeparam name="T">The queue element type</typeparam>
        /// <param name="capacityHint">If negative, an SpscLinkedArrayQueue is created with
        /// capacity hint as the absolute of capacityHint,
        /// if one, an SpscOneQueue is created. Otherwise, an SpscArrayQueue is created with
        /// the capacityHint.</param>
        /// <returns></returns>
        public static IQueue<T> CreateQueue<T>(int capacityHint)
        {
            if (capacityHint < 0)
            {
                return new SpscLinkedArrayQueue<T>(-capacityHint);
            }
            else
            if (capacityHint == 1)
            {
                return new SpscOneQueue<T>();
            }
            return new SpscArrayQueue<T>(capacityHint);
        }

        /// <summary>
        /// Tries to enter the drain mode via a fast-path method.
        /// </summary>
        /// <param name="wip">The work-in-progress field to change</param>
        /// <returns>True if successful</returns>
        public static bool TryEnter(ref int wip)
        {
            return Volatile.Read(ref wip) == 0 && Interlocked.CompareExchange(ref wip, 1, 0) == 0;
        }
    }
}
