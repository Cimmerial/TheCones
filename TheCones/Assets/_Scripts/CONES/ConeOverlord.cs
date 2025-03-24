using System.Collections.Generic;
using UnityEngine;

namespace Cimmerial
{
    /// <summary>
    /// Manages all cones currently onscreen.
    /// </summary>
    public class ConeOverlord : MonoBehaviour // deez nuts lol gottem xD xD xD 
    {
        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- VARIABLES -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        [SerializeField] private Cone conePrefab;

        private HashSet<Cone> _conesOnTheTrack;
        private HashSet<Cone> _conesInQueueToJoin;
        private HashSet<ConeType> _conesAboutToJoinQueue;


        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- BASE FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        private void Start()
        {
            Instantiate(conePrefab);
        }


        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- CLASS FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-


    }
}
