
using UnityEngine;

namespace Cimmerial
{
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager instance { get; private set; }
        public PlayerInputEvents playerInputEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            instance = this;
            playerInputEvents = new PlayerInputEvents();
        }
    }
}
// example events:
// public event Action<BloodSpatterType, Vector2, Vector2> OnInstantiateBloodSpatter;
//         //===================================================================================================================
//         public void InstantiateBloodSpatter(BloodSpatterType bloodSpatterType, Vector2 position, Vector2 direction) { OnInstantiateBloodSpatter?.Invoke(bloodSpatterType, position, direction); }