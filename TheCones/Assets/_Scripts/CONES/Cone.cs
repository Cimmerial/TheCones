using System;
using UnityEngine;

namespace Cimmerial
{
    /// <summary>
    /// Brain of the Cone prefab.
    /// </summary>
    public class Cone : MonoBehaviour
    {

        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- VARIABLES -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        [Header("STATS")]
        [SerializeField] private ConeStats coneStats;
        [Serializable]
        public class ConeStats
        {
            public float movementSpeed;
            public float currentHealth;
            public float maxHealth;
            // bobbing speed
            // bobbing height
        }

        [Header("STATE")]
        [SerializeField] private ConeState coneState;
        [Serializable]
        public class ConeState
        {
            public Toggleables toggleables;
            [Serializable]
            public class Toggleables
            {
                public bool isMoving;
                public bool isStalled;
            }
        }

        [Header("REFERENCES")]
        [SerializeField] private ConeReferences coneReferences;
        [Serializable]
        public class ConeReferences
        {
            // meshes
            // materials
            // colors
            // audio clips (not the clip itself but a reference)
        }

        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- BASE FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-



        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- CLASS FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

    }
}