using System.Collections.Generic;
using UnityEngine;

namespace Game.Views
{
    public sealed class PlanetsContainer : MonoBehaviour
    {
        [field: SerializeField] public List<PlanetView> Planets { get; private set; }
    }
}