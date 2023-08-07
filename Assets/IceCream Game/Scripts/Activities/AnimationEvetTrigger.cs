using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvetTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent Event;

    public void call()
    {
        Event.Raise();
    }
}
