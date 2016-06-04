﻿using Reactive.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reactor.Core.publisher;
using Reactor.Core.subscriber;
using Reactor.Core.flow;
using Reactor.Core.scheduler;

namespace Reactor.Core
{
    /// <summary>
    /// Extension methods for IFlux sources.
    /// </summary>
    public static class Flux
    {
        /// <summary>
        /// The default buffer size and prefetch amount.
        /// </summary>
        public static int BufferSize { get { return 128; } }

        // ---------------------------------------------------------------------------------------------------------
        // Enter the reactive world
        // ---------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Create a new IPublisher that will only emit the passed data then onComplete.
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="value">The unique data to emit</param>
        /// <returns>The new IPublisher instance</returns>
        public static IFlux<T> Just<T>(T value)
        {
            return new PublisherJust<T>(value);
        }

        /// <summary>
        /// Returns an empty instance which completes the ISubscribers immediately.
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <returns>The shared Empty instance.</returns>
        public static IFlux<T> Empty<T>()
        {
            return PublisherEmpty<T>.Instance;
        }

        /// <summary>
        /// Returns an never instance which sets an empty ISubscription and
        /// does nothing further.
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <returns>The shared Never instance.</returns>
        public static IFlux<T> Never<T>()
        {
            return PublisherNever<T>.Instance;
        }

        public static IFlux<R> CombineLatest<T, R>(Func<T[], R> combiner, bool delayError = false, params IPublisher<T>[] sources)
        {
            return CombineLatest(combiner, BufferSize, delayError, sources);
        }

        public static IFlux<R> CombineLatest<T, R>(Func<T[], R> combiner, int prefetch, bool delayError = false, params IPublisher<T>[] sources)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<R> CombineLatest<T1, T2, R>(IPublisher<T1> p1, IPublisher<T2> p2, Func<T1, T2, R> combiner, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<R> CombineLatest<T1, T2, T3, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T3> p3,
            Func<T1, T2, T3, R> combiner, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<R> CombineLatest<T1, T2, T3, T4, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T3> p3, IPublisher<T4> p4,
            Func<T1, T2, T3, T4, R> combiner, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<R> CombineLatest<T1, T2, T3, T4, T5, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T3> p3, IPublisher<T4> p4,
            IPublisher<T5> p5,
            Func<T1, T2, T3, T4, T5, R> combiner, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<R> CombineLatest<T1, T2, T3, T4, T5, T6, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T3> p3, IPublisher<T4> p4,
            IPublisher<T5> p5, IPublisher<T6> p6,
            Func<T1, T2, T3, T4, T5, T6, R> combiner, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<R> CombineLatest<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> combiner, bool delayError = false)
        {
            return CombineLatest(sources, combiner, BufferSize);
        }

        public static IFlux<R> CombineLatest<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> combiner, int prefetch, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<T> Concat<T>(IEnumerable<IPublisher<T>> sources, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<T> Concat<T>(IEnumerable<IPublisher<T>> sources, int prefetch, bool delayError = false)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<T> Concat<T>(bool delayError = false, params IPublisher<T>[] sources)
        {
            return Concat(BufferSize, delayError, sources);
        }

        public static IFlux<T> Concat<T>(int prefetch, bool delayError = false, params IPublisher<T>[] sources)
        {
            // TODO implement CombineLatest
            throw new NotImplementedException();
        }

        public static IFlux<T> Create<T>(Action<IFluxEmitter<T>> emitter, BackpressureHandling backpressure = BackpressureHandling.Error)
        {
            // TODO implement Create
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supply a IPublisher everytime subscribe is called on the returned flux. The passed supplier function
        /// will be invoked and it's up to the developer to choose to return a new instance of a IPublisher or reuse
        /// one effecitvely behaving like from(IPublisher).
        /// </summary>
        /// <typeparam name="T">the type of values passing through the IFlux</typeparam>
        /// <param name="supplier">the IPublisher supplier function to call on subscribe</param>
        /// <returns>a deferred IFlux</returns>
        public static IFlux<T> Defer<T>(Func<IPublisher<T>> supplier)
        {
            return new PublisherDefer<T>(supplier);
        }

        public static IFlux<T> Error<T>(Exception error, bool whenRequested = false)
        {
            // TODO implement Error
            throw new NotImplementedException();
        }

        public static IFlux<T> Error<T>(Func<Exception> errorSupplier, bool whenRequested = false)
        {
            // TODO implement Error
            throw new NotImplementedException();
        }

        public static IFlux<T> FirstEmitting<T>(params IPublisher<T>[] sources)
        {
            // TODO implement FirstEmitting
            throw new NotImplementedException();
        }

        public static IFlux<T> FirstEmitting<T>(IEnumerable<IPublisher<T>> sources)
        {
            // TODO implement FirstEmitting
            throw new NotImplementedException();
        }

        /// <summary>
        /// Expose the specified IPublisher with the IFlux API.
        /// </summary>
        /// <typeparam name="T">the source sequence type</typeparam>
        /// <param name="source">the source to decorate</param>
        /// <returns>a new IFlux</returns>
        public static IFlux<T> From<T>(IPublisher<T> source)
        {
            if (source is IFlux<T>)
            {
                return (IFlux<T>)source;
            }
            if (source is IFuseable)
            {
                return new PublisherWrapFuseable<T>(source);
            }
            return new PublisherWrap<T>(source);
        }

        public static IFlux<T> From<T>(params T[] values)
        {
            int n = values.Length;
            ;
            if (n == 0)
            {
                return Empty<T>();
            }
            else
            if (n == 1)
            {
                return Just(values[0]);
            }
            return new PublisherArray<T>(values);
        }

        public static IFlux<T> From<T>(IEnumerable<T> enumerable)
        {
            return new PublisherEnumerable<T>(enumerable);
        }

        public static IFlux<T> From<T>(IObservable<T> source, BackpressureHandling backpressure = BackpressureHandling.Error)
        {
            // TODO implement From
            throw new NotImplementedException();
        }

        public static IFlux<T> From<T>(Func<T> supplier)
        {
            return new PublisherFunc<T>(supplier);
        }

        public static IFlux<T> Generate<T>(Action<ISignalEmitter<T>> generator)
        {
            return Generate<T, object>(() => default(object), (s, e) => { generator(e); return s; }, s => { });
        }

        public static IFlux<T> Generate<T, S>(Func<S> stateSupplier, Func<S, ISignalEmitter<T>, S> generator)
        {
            return Generate(stateSupplier, generator, s => { });
        }

        public static IFlux<T> Generate<T, S>(Func<S> stateSupplier, Func<S, ISignalEmitter<T>, S> generator, Action<S> stateDisposer)
        {
            // TODO implement Generate
            throw new NotImplementedException();
        }

        public static IFlux<long> Interval(TimeSpan period)
        {
            return Interval(period, period, DefaultScheduler.Instance);
        }

        public static IFlux<long> Interval(TimeSpan period, TimedScheduler scheduler)
        {
            return Interval(period, period, scheduler);
        }

        public static IFlux<long> Interval(TimeSpan initialDelay, TimeSpan period)
        {
            return Interval(initialDelay, period, DefaultScheduler.Instance);
        }

        public static IFlux<long> Interval(TimeSpan initialDelay, TimeSpan period, TimedScheduler scheduler)
        {
            return new PublisherInterval(initialDelay, period, scheduler);
        }

        public static IFlux<T> Merge<T>(int maxConcurrency = int.MaxValue, bool delayErrors = false, params IPublisher<T>[] sources)
        {
            return Merge(BufferSize, maxConcurrency, delayErrors, sources);
        }

        public static IFlux<T> Merge<T>(int prefetch, int maxConcurrency = int.MaxValue, bool delayErrors = false, params IPublisher<T>[] sources)
        {
            // TODO implement Merge
            throw new NotImplementedException();
        }

        public static IFlux<T> Merge<T>(IEnumerable<IPublisher<T>> sources, int maxConcurrency = int.MaxValue, bool delayErrors = false)
        {
            return Merge(sources, BufferSize, maxConcurrency, delayErrors);
        }

        public static IFlux<T> Merge<T>(IEnumerable<IPublisher<T>> sources, int prefetch, int maxConcurrency = int.MaxValue, bool delayErrors = false)
        {
            // TODO implement Merge
            throw new NotImplementedException();
        }

        public static IFlux<T> Merge<T>(this IPublisher<IPublisher<T>> sources, int maxConcurrency = int.MaxValue, bool delayErrors = false)
        {
            return Merge(sources, BufferSize, maxConcurrency, delayErrors);
        }

        public static IFlux<T> Merge<T>(this IPublisher<IPublisher<T>> sources, int prefetch, int maxConcurrency = int.MaxValue, bool delayErrors = false)
        {
            // TODO implement Merge
            throw new NotImplementedException();
        }

        /// <summary>
        /// Signals a range of values from start to start + count (exclusive).
        /// </summary>
        /// <param name="start">The start value</param>
        /// <param name="count">The number of items, non-negative</param>
        /// <returns></returns>
        public static IFlux<int> Range(int start, int count)
        {
            if (count == 0)
            {
                return Empty<int>();
            }
            else
            if (count == 1)
            {
                return Just(start);
            }
            return new PublisherRange(start, count);
        }

        public static IFluxProcessor<IPublisher<T>, T> SwitchOnNext<T>()
        {
            return SwitchOnNext<T>(BufferSize);
        }

        public static IFluxProcessor<IPublisher<T>, T> SwitchOnNext<T>(int prefetch)
        {
            // TODO implement SwitchOnNext
            throw new NotImplementedException();
        }

        public static IFlux<T> SwitchOnNext<T>(this IPublisher<IPublisher<T>> sources)
        {
            return SwitchOnNext<T>(sources, BufferSize);
        }

        public static IFlux<T> SwitchOnNext<T>(IPublisher<IPublisher<T>> sources, int prefetch)
        {
            // TODO implement SwitchOnNext
            throw new NotImplementedException();
        }

        public static IFlux<long> Timer(TimeSpan delay)
        {
            return Timer(delay, DefaultScheduler.Instance);
        }

        public static IFlux<long> Timer(TimeSpan delay, TimedScheduler scheduler)
        {
            return new PublisherTimer(delay, scheduler);
        }

        public static IFlux<T> Using<T, S>(Func<S> resourceSupplier, Func<S, IPublisher<T>> publisherFactory, Action<S> resourceDisposer, bool eager = true)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        public static IFlux<R> Zip<T, R>(Func<T[], R> zipper, bool delayErrors = false, params IPublisher<T>[] sources)
        {
            return Zip(zipper, BufferSize, delayErrors, sources);
        }

        public static IFlux<R> Zip<T, R>(Func<T[], R> zipper, int prefetch, bool delayErrors = false, params IPublisher<T>[] sources)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        public static IFlux<R> Zip<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> zipper, bool delayErrors = false)
        {
            return Zip(sources, zipper, BufferSize);
        }

        public static IFlux<R> Zip<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> zipper, int prefetch, bool delayErrors = false)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        public static IFlux<R> Zip<T1, T2, R>(IPublisher<T1> p1, IPublisher<T2> p2, Func<T1, T2, R> zipper, bool delayErrors = false)
        {
            return Zip(p1, p2, zipper, BufferSize, delayErrors);
        }

        public static IFlux<R> Zip<T1, T2, R>(IPublisher<T1> p1, IPublisher<T2> p2, Func<T1, T2, R> zipper, int prefetch, bool delayErrors = false)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        public static IFlux<R> Zip<T1, T2, T3, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3,
            Func<T1, T2, T3, R> zipper, bool delayErrors = false)
        {
            return Zip(p1, p2, p3, zipper, BufferSize, delayErrors);
        }

        public static IFlux<R> Zip<T1, T2, T3, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3,
            Func<T1, T2, T3, R> zipper,
            int prefetch, bool delayErrors = false)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        public static IFlux<R> Zip<T1, T2, T3, T4, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3, IPublisher<T4> p4,
            Func<T1, T2, T3, T4, R> zipper, bool delayErrors = false)
        {
            return Zip(p1, p2, p3, p4, zipper, BufferSize, delayErrors);
        }

        public static IFlux<R> Zip<T1, T2, T3, T4, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3, IPublisher<T4> p4,
            Func<T1, T2, T3, T4, R> zipper,
            int prefetch, bool delayErrors = false)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        public static IFlux<R> Zip<T1, T2, T3, T4, T5, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3, IPublisher<T4> p4,
            IPublisher<T5> p5,
            Func<T1, T2, T3, T4, T5, R> zipper, bool delayErrors = false)
        {
            return Zip(p1, p2, p3, p4, p5, zipper, BufferSize, delayErrors);
        }

        public static IFlux<R> Zip<T1, T2, T3, T4, T5, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3, IPublisher<T4> p4,
            IPublisher<T5> p5,
            Func<T1, T2, T3, T4, T5, R> zipper,
            int prefetch, bool delayErrors = false)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        public static IFlux<R> Zip<T1, T2, T3, T4, T5, T6, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3, IPublisher<T4> p4,
            IPublisher<T5> p5, IPublisher<T6> p6,
            Func<T1, T2, T3, T4, T5, T6, R> zipper, bool delayErrors = false)
        {
            return Zip(p1, p2, p3, p4, p5, p6, zipper, BufferSize, delayErrors);
        }

        public static IFlux<R> Zip<T1, T2, T3, T4, T5, T6, R>(
            IPublisher<T1> p1, IPublisher<T2> p2,
            IPublisher<T2> p3, IPublisher<T4> p4,
            IPublisher<T5> p5, IPublisher<T6> p6,
            Func<T1, T2, T3, T4, T5, T6, R> zipper,
            int prefetch, bool delayErrors = false)
        {
            // TODO implement Using
            throw new NotImplementedException();
        }

        // ---------------------------------------------------------------------------------------------------------
        // Transform the reactive world
        // ---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Transform the items emitted by this IFlux by applying a function to each item.
        /// </summary>
        /// <typeparam name="T">The input value type</typeparam>
        /// <typeparam name="R">The output value type</typeparam>
        /// <param name="source">The source IFlux</param>
        /// <param name="mapper">The mapper from Ts to Rs</param>
        /// <returns>The new IFlux instance</returns>
        public static IFlux<R> Map<T, R>(this IFlux<T> source, Func<T, R> mapper)
        {
            if (source is IFuseable)
            {
                return new PublisherMapFuseable<T, R>(source, mapper);
            }
            return new PublisherMap<T, R>(source, mapper);
        }

        public static R As<T, R>(this IFlux<T> source, Func<IFlux<T>, R> transformer)
        {
            return transformer(source);
        }

        public static IMono<bool> Any<T>(this IFlux<T> source, Func<T, bool> predicate)
        {
            // TODO implement Any
            throw new NotImplementedException();
        }

        public static IMono<bool> All<T>(this IFlux<T> source, Func<T, bool> predicate)
        {
            // TODO implement All
            throw new NotImplementedException();
        }

        public static IMono<IList<T>> Buffer<T>(this IFlux<T> source)
        {
            // TODO implement Buffer
            throw new NotImplementedException();
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, int size)
        {
            return Buffer(source, size, size);
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, int size, int skip)
        {
            // TODO implement Buffer
            throw new NotImplementedException();
        }

        public static IFlux<IList<T>> Buffer<T, U>(this IFlux<T> source, IPublisher<U> boundary)
        {
            // TODO implement Buffer
            throw new NotImplementedException();
        }

        public static IFlux<IList<T>> Buffer<T, U, V>(this IFlux<T> source, IPublisher<U> open, Func<U, IPublisher<V>> close)
        {
            // TODO implement Buffer
            throw new NotImplementedException();
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, TimeSpan timespan)
        {
            return Buffer(source, timespan, timespan, DefaultScheduler.Instance);
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, TimeSpan timespan, TimedScheduler scheduler)
        {
            return Buffer(source, timespan, timespan, scheduler);
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, TimeSpan timespan, TimeSpan timeskip)
        {
            return Buffer(source, timespan, timeskip, DefaultScheduler.Instance);
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, TimeSpan timespan, TimeSpan timeskip, TimedScheduler scheduler)
        {
            // TODO implement Buffer
            throw new NotImplementedException();
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, int maxSize, TimeSpan timespan)
        {
            return Buffer(source, maxSize, timespan, DefaultScheduler.Instance);
        }

        public static IFlux<IList<T>> Buffer<T>(this IFlux<T> source, int maxSize, TimeSpan timespan, TimedScheduler scheduler)
        {
            // TODO implement Buffer
            throw new NotImplementedException();
        }

        public static IFlux<T> Cache<T>(this IFlux<T> source, int history = int.MaxValue)
        {
            // TODO implement Cache
            throw new NotImplementedException();
        }

        public static IFlux<R> Cast<T, R>(this IFlux<T> source, R witness = default(R))
        {
            // TODO implement Cast
            throw new NotImplementedException();
        }

        public static IMono<C> Collect<T, C>(this IFlux<T> source, Func<C> collectionSupplier, Action<C, T> collector)
        {
            return new PublisherCollect<T, C>(source, collectionSupplier, collector);
        }

        public static IMono<IList<T>> CollectList<T>(this IFlux<T> source, int capacityHint = 16)
        {
            return Collect<T, IList<T>>(source, () => new List<T>(capacityHint), (c, t) => c.Add(t));
        }

        public static IMono<IList<T>> CollectSortedList<T>(this IFlux<T> source, IComparer<T> comparer, int capacityHint = 16)
        {
            return CollectList(source, capacityHint).Map(c => c.OrderBy(v => v, comparer).ToList());
        }

        public static IMono<IDictionary<K, T>> CollectDictionary<T, K>(this IFlux<T> source, Func<T, K> keySelector)
        {
            return CollectDictionary(source, keySelector, v => v);
        }

        public static IMono<IDictionary<K, V>> CollectDictionary<T, K, V>(this IFlux<T> source, Func<T, K> keySelector, Func<T, V> valueSelector)
        {
            return Collect<T, IDictionary<K, V>>(source, () => new Dictionary<K, V>(), (d, t) =>
            {
                var k = keySelector(t);
                if (d.ContainsKey(k))
                {
                    d[k] = valueSelector(t);
                }
                else
                {
                    d.Add(k, valueSelector(t));
                }
            });
        }

        public static IMono<IDictionary<K, IList<T>>> CollectMultiDictionary<T, K>(this IFlux<T> source, Func<T, K> keySelector)
        {
            return CollectMultiDictionary(source, keySelector, v => v);
        }

        public static IMono<IDictionary<K, IList<V>>> CollectMultiDictionary<T, K, V>(this IFlux<T> source, Func<T, K> keySelector, Func<T, V> valueSelector)
        {
            return Collect<T, IDictionary<K, IList<V>>>(source, () => new Dictionary<K, IList<V>>(), (d, t) =>
            {
                var k = keySelector(t);
                if (d.ContainsKey(k))
                {
                    var list = d[k];
                    list.Add(valueSelector(t));
                }
                else
                {
                    var list = new List<V>();
                    list.Add(valueSelector(t));
                    d.Add(k, list);
                }
            });
        }

        public static IFlux<R> Compose<T, R>(this IFlux<T> source, Func<IFlux<T>, IPublisher<R>> composer)
        {
            return Defer(() => composer(source));
        }

        public static IFlux<R> ConcatMap<T, R>(this IFlux<T> source, Func<T, IPublisher<R>> mapper, ConcatErrorMode errorMode = ConcatErrorMode.Immediate)
        {
            return ConcatMap(source, mapper, BufferSize, errorMode);
        }

        public static IFlux<R> ConcatMap<T, R>(this IFlux<T> source, Func<T, IPublisher<R>> mapper, int prefetch, ConcatErrorMode errorMode = ConcatErrorMode.Immediate)
        {
            // TODO implement ConcatMap
            throw new NotImplementedException();
        }

        public static IFlux<T> ConcatWith<T>(this IFlux<T> source, IFlux<T> other, bool delayError = false)
        {
            // TODO implement ConcatMap
            throw new NotImplementedException();
        }

        public static IFlux<T> ConcatMap<T, R>(this IFlux<T> source, Func<T, IEnumerable<R>> mapper, ConcatErrorMode errorMode = ConcatErrorMode.Immediate)
        {
            return ConcatMap(source, mapper, BufferSize, errorMode);
        }

        public static IFlux<T> ConcatMap<T, R>(this IFlux<T> source, Func<T, IEnumerable<R>> mapper, int prefetch, ConcatErrorMode errorMode = ConcatErrorMode.Immediate)
        {
            // TODO implement ConcatMap
            throw new NotImplementedException();
        }

        public static IMono<long> Count<T>(this IFlux<T> source)
        {
            return new PublisherCount<T>(source);
        }

        public static IFlux<T> DefaultIfEmpty<T>(this IFlux<T> source, T defaultValue)
        {
            // TODO implement DefaultIfEmpty
            throw new NotImplementedException();
        }

        public static IFlux<T> Delay<T>(this IFlux<T> source, TimeSpan delay)
        {
            // TODO implement Delay
            throw new NotImplementedException();
        }

        public static IFlux<T> DelaySubscription<T>(this IFlux<T> source, TimeSpan delay)
        {
            // TODO implement DelaySubscription
            throw new NotImplementedException();
        }

        public static IFlux<T> DelaySubscription<T, U>(this IFlux<T> source, IPublisher<U> other)
        {
            // TODO implement DelaySubscription
            throw new NotImplementedException();
        }

        public static IFlux<T> Dematerialize<T>(this IFlux<ISignal<T>> source)
        {
            // TODO implement Dematerialize
            throw new NotImplementedException();
        }

        public static IFlux<T> Distinct<T>(this IFlux<T> source)
        {
            return Distinct(source, v => v, EqualityComparer<T>.Default);
        }

        public static IFlux<T> Distinct<T>(this IFlux<T> source, IEqualityComparer<T> comparer)
        {
            return Distinct(source, v => v, comparer);
        }

        public static IFlux<T> Distinct<T, K>(this IFlux<T> source, Func<T, K> keySelector)
        {
            return Distinct(source, keySelector, EqualityComparer<K>.Default);
        }

        public static IFlux<T> Distinct<T, K>(this IFlux<T> source, Func<T, K> keySelector, IEqualityComparer<K> comparer)
        {
            return new PublisherDistinct<T, K>(source, keySelector, comparer);
        }

        public static IFlux<T> DistinctUntilChanged<T>(this IFlux<T> source)
        {
            return DistinctUntilChanged(source, v => v, EqualityComparer<T>.Default);
        }

        public static IFlux<T> DistinctUntilChanged<T>(this IFlux<T> source, IEqualityComparer<T> comparer)
        {
            return DistinctUntilChanged(source, v => v, comparer);
        }

        public static IFlux<T> DistinctUntilChanged<T, K>(this IFlux<T> source, Func<T, K> keySelector)
        {
            return DistinctUntilChanged(source, keySelector, EqualityComparer<K>.Default);
        }

        public static IFlux<T> DistinctUntilChanged<T, K>(this IFlux<T> source, Func<T, K> keySelector, IEqualityComparer<K> comparer)
        {
            return new PublisherDistinctUntilChanged<T, K>(source, keySelector, comparer);
        }

        public static IFlux<T> DoAfterTerminate<T>(this IFlux<T> source, Action onAfterTerminate)
        {
            return PublisherPeek<T>.withOnAfterTerminate(source, onAfterTerminate);
        }

        public static IFlux<T> DoAfterNext<T>(this IFlux<T> source, Action<T> onAfterNext)
        {
            return PublisherPeek<T>.withOnAfterNext(source, onAfterNext);
        }

        public static IFlux<T> DoOnCancel<T>(this IFlux<T> source, Action onCancel)
        {
            return PublisherPeek<T>.withOnCancel(source, onCancel);
        }

        public static IFlux<T> DoOnComplete<T>(this IFlux<T> source, Action onComplete)
        {
            return PublisherPeek<T>.withOnComplete(source, onComplete);
        }

        public static IFlux<T> DoOnError<T>(this IFlux<T> source, Action<Exception> onError)
        {
            return PublisherPeek<T>.withOnError(source, onError);
        }

        public static IFlux<T> DoOnError<T, E>(this IFlux<T> source, Action<E> onError) where E : Exception
        {
            return DoOnError(source, e =>
            {
                if (e is E)
                {
                    onError(e as E);
                }
            });
        }

        public static IFlux<T> DoOnError<T>(this IFlux<T> source, Func<Exception, bool> predicate, Action<Exception> onError)
        {
            return DoOnError(source, e =>
            {
                if (predicate(e))
                {
                    onError(e);
                }
            });
        }

        public static IFlux<T> DoOnNext<T>(this IFlux<T> source, Action<T> onNext)
        {
            return PublisherPeek<T>.withOnNext(source, onNext);
        }

        public static IFlux<T> DoOnRequest<T>(this IFlux<T> source, Action<long> onRequest)
        {
            return PublisherPeek<T>.withOnRequest(source, onRequest);
        }

        public static IFlux<T> DoOnSubscribe<T>(this IFlux<T> source, Action<ISubscription> onSubscribe)
        {
            return PublisherPeek<T>.withOnSubscribe(source, onSubscribe);
        }

        public static IFlux<T> DoOnTerminate<T>(this IFlux<T> source, Action onTerminate)
        {
            return PublisherPeek<T>.withOnTerminate(source, onTerminate);
        }

        public static IFlux<Timed<T>> Elapsed<T>(this IFlux<T> source)
        {
            return Elapsed(source, DefaultScheduler.Instance);
        }

        public static IFlux<Timed<T>> Elapsed<T>(this IFlux<T> source, TimedScheduler scheduler)
        {
            // TODO implement Elapsed
            throw new NotImplementedException();
        }

        public static IMono<T> ElementAt<T>(this IFlux<T> source)
        {
            // TODO implement ElementAt
            throw new NotImplementedException();
        }

        public static IMono<T> ElementAt<T>(this IFlux<T> source, T defaultValue)
        {
            // TODO implement ElementAt
            throw new NotImplementedException();
        }

        // ---------------------------------------------------------------------------------------------------------
        // Leave the reactive world
        // ---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Subscribes to the IPublisher and consumes only its OnNext signals.
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="source">The source IPublisher</param>
        /// <param name="onNext">The callback for the OnNext signals</param>
        /// <returns>The IDisposable that allows cancelling the subscription.</returns>
        public static IDisposable Subscribe<T>(this IPublisher<T> source, Action<T> onNext)
        {
            var d = new CallbackSubscriber<T>(onNext, e => { ExceptionHelper.OnErrorDropped(e); }, () => { });
            source.Subscribe(d);
            return d;
        }

        /// <summary>
        /// Subscribes to the IPublisher and consumes only its OnNext signals.
        /// </summary>
        /// <remarks>
        /// If the <paramref name="onNext"/> callback crashes, the error is routed
        /// to <paramref name="onError"/> callback.
        /// If the <paramref name="onError"/> callback crashes, the error is routed to the
        /// global error hanlder in <see cref="ExceptionHelper.OnErrorDropped(Exception)"/>.
        /// </remarks>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="source">The source IPublisher</param>
        /// <param name="onNext">The callback for the OnNext signals</param>
        /// <param name="onError">The callback for the OnError signals</param>
        /// <returns>The IDisposable that allows cancelling the subscription.</returns>
        public static IDisposable Subscribe<T>(this IPublisher<T> source, Action<T> onNext, Action<Exception> onError)
        {
            var d = new CallbackSubscriber<T>(onNext, onError, () => { });
            source.Subscribe(d);
            return d;
        }

        /// <summary>
        /// Subscribes to the IPublisher and consumes only its OnNext signals.
        /// </summary>
        /// <remarks>
        /// If the <paramref name="onNext"/> callback crashes, the error is routed
        /// to <paramref name="onError"/> callback.
        /// If the <paramref name="onError"/> or <paramref name="onComplete"/> callbackcrashes, 
        /// the error is routed to the
        /// global error hanlder in <see cref="ExceptionHelper.OnErrorDropped(Exception)"/>.
        /// </remarks>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="source">The source IPublisher</param>
        /// <param name="onNext">The callback for the OnNext signals.</param>
        /// <param name="onError">The callback for the OnError signal.</param>
        /// <param name="onComplete">The callback for the OnComplete signal.</param>
        /// <returns>The IDisposable that allows cancelling the subscription.</returns>
        public static IDisposable Subscribe<T>(this IPublisher<T> source, Action<T> onNext, Action<Exception> onError, Action onComplete)
        {
            var d = new CallbackSubscriber<T>(onNext, onError, onComplete);
            source.Subscribe(d);
            return d;
        }

    }
}