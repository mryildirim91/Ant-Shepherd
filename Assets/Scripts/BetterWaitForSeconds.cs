﻿using System.Collections.Generic;
using UnityEngine;

namespace MyUtils
{
    public static class BetterWaitForSeconds
    {
        private class WaitForSeconds : CustomYieldInstruction
        {
            private float _waitUntil;
            public override bool keepWaiting
            {
                get
                {
                    if (Time.time < _waitUntil)
                        return true;

                    Pool(this);
                    return false;
                }
            }

            public void Initialize(float seconds)
            {
                _waitUntil = Time.time + seconds;
            }
        }

        private class WaitForSecondsRealtime : CustomYieldInstruction
        {
            private float _waitUntil;
            public override bool keepWaiting
            {
                get
                {
                    if (Time.realtimeSinceStartup < _waitUntil)
                        return true;

                    Pool(this);
                    return false;
                }
            }

            public void Initialize(float seconds)
            {
                _waitUntil = Time.realtimeSinceStartup + seconds;
            }
        }

        private const int PoolInitialSize = 4;

        private static readonly Stack<WaitForSeconds> waitForSecondsPool;
        private static readonly Stack<WaitForSecondsRealtime> waitForSecondsRealtimePool;

        static BetterWaitForSeconds()
        {
            waitForSecondsPool = new Stack<WaitForSeconds>(PoolInitialSize);
            waitForSecondsRealtimePool = new Stack<WaitForSecondsRealtime>(PoolInitialSize);

            for (int i = 0; i < PoolInitialSize; i++)
            {
                waitForSecondsPool.Push(new WaitForSeconds());
                waitForSecondsRealtimePool.Push(new WaitForSecondsRealtime());
            }
        }

        public static CustomYieldInstruction Wait(float seconds)
        {
            WaitForSeconds instance;
            if (waitForSecondsPool.Count > 0)
                instance = waitForSecondsPool.Pop();
            else
                instance = new WaitForSeconds();

            instance.Initialize(seconds);
            return instance;
        }

        public static CustomYieldInstruction WaitRealtime(float seconds)
        {
            WaitForSecondsRealtime instance;
            if (waitForSecondsRealtimePool.Count > 0)
                instance = waitForSecondsRealtimePool.Pop();
            else
                instance = new WaitForSecondsRealtime();

            instance.Initialize(seconds);
            return instance;
        }
        private static void Pool(WaitForSeconds instance)
        {
            waitForSecondsPool.Push(instance);
        }
        private static void Pool(WaitForSecondsRealtime instance)
        {
            waitForSecondsRealtimePool.Push(instance);
        }
    }
}
