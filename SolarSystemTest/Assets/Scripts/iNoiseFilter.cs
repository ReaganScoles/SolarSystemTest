using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iNoiseFilter
{
    float Evaluate(Vector3 point);
}
