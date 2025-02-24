using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private DiceRollMediator _mediator;

    public void Run()
    {
        DiceFactory factory = new();

        //foreach (var point in _points)
        //{
        //    factory.Get(point.position);
        //}

        for (int i = 0; i < _points.Count; i++)
        {
            if (i == 0)
                _mediator.Initialize(factory.Get(_points[i].position));
            else
                factory.Get(_points[i].position);
        }
    }
}
