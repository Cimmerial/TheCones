using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Cimmerial
{
    public interface IBaseReboost
    {
        public void ResetReboostiplier();
    }
    [System.Serializable]
    public class BaseBoostedFloat : IBaseReboost
    {
        public float baseF;
        public float boostF = 0;
        public float multiplierF = 1;
        public bool multiplierDefaultIs0F = false;
        public float BaseBoostedF { get { return Mathf.Max((baseF + boostF) * multiplierF, 0); } }
        public void ResetReboostiplier() { boostF = 0; multiplierF = multiplierDefaultIs0F ? 0 : 1; }
    }
    [System.Serializable]
    public class OldNewBBF : IBaseReboost // OldNewBaseBoostedFloat
    {
        public float baseF;
        public float boostF = 0;
        public float multiplierF = 1;
        public bool multiplierDefaultIs0F = false;

        public float oldBaseF;
        public float oldBoostF = 0;
        public float oldMultiplierF = 1;

        public float BaseBoostedF { get { return Mathf.Max((baseF + boostF) * multiplierF, 0); } }

        public float OldBaseBoostedF { get { return Mathf.Max((oldBaseF + oldBoostF) * oldMultiplierF, 0); } }

        public float Difference { get { return BaseBoostedF - OldBaseBoostedF; } }

        public void UpdateOldToNew()
        {
            oldBaseF = baseF;
            oldBoostF = boostF;
            oldMultiplierF = multiplierF;
        }
        public void ResetReboostiplier() { boostF = 0; multiplierF = multiplierDefaultIs0F ? 0 : 1; }
    }
    [System.Serializable]
    public class BaseBoostedInt : IBaseReboost
    {
        public int baseI;
        public int boostI = 0;
        public int multiplierI = 1;
        public int BaseBoostedI { get { return Mathf.Max((baseI + boostI) * multiplierI, 0); } }
        public void ResetReboostiplier() { boostI = 0; multiplierI = 1; }
    }
    [System.Serializable]
    public class BaseReducedFloat : IBaseReboost
    {
        public float baseF;
        public float reductionF = 0;
        public float multiplierF = 1;
        public float BaseReducedF { get { return Mathf.Max((baseF - reductionF) * multiplierF, 0); } }
        public void ResetReboostiplier() { reductionF = 0; multiplierF = 1; }
    }

    [System.Serializable]
    public class BasePermaTempBool : IBaseReboost
    {
        public bool baseB;
        public bool adminOverrideOn;
        public bool adminOverrideState;

        [SerializeField] private readonly object stateLock = new object();
        [SerializeField] private readonly Dictionary<string, StateChange> changes = new Dictionary<string, StateChange>();
        [SerializeField] private bool isDirty = true;
        [SerializeField] private bool cachedState;

        public delegate void StateChangedEventHandler(bool newState);
        public event StateChangedEventHandler OnStateChanged;

        private class StateChange
        {
            public bool state;
            public int priority;
            public string marker;

            public StateChange(bool state, int priority, string marker)
            {
                this.state = state;
                this.priority = priority;
                this.marker = marker;
            }
        }

        public bool BasePermaTemp
        {
            get
            {
                if (adminOverrideOn) return adminOverrideState;

                lock (stateLock)
                {
                    if (isDirty)
                    {
                        UpdateCachedState();
                    }
                    return cachedState;
                }
            }
        }

        private void UpdateCachedState()
        {
            bool previousState = cachedState;

            var highestPriority = -1;
            var highestState = baseB;

            foreach (var change in changes.Values.OrderByDescending(c => c.priority))
            {
                if (change.priority > highestPriority)
                {
                    highestPriority = change.priority;
                    highestState = change.state;
                }
            }

            cachedState = highestState;
            isDirty = false;

            if (previousState != cachedState)
            {
                OnStateChanged?.Invoke(cachedState);
            }
        }

        public void SetState(bool state, int priority, string marker)
        {
            lock (stateLock)
            {
                if (changes.TryGetValue(marker, out var existing))
                {
                    if (existing.state != state) // If states are opposite
                    {
                        changes.Remove(marker); // Remove the marker entirely
                    }
                    else if (priority >= existing.priority)
                    {
                        existing.state = state;
                        existing.priority = priority;
                    }
                }
                else
                {
                    changes.Add(marker, new StateChange(state, priority, marker));
                }

                isDirty = true;
                UpdateCachedState();
            }
        }

        public void CancelChange(string marker)
        {
            lock (stateLock)
            {
                if (changes.Remove(marker))
                {
                    isDirty = true;
                    UpdateCachedState();
                }
            }
        }

        public void ResetReboostiplier()
        {
            lock (stateLock)
            {
                changes.Clear();
                adminOverrideOn = false;
                isDirty = true;
                UpdateCachedState();
            }
        }

    }

}
