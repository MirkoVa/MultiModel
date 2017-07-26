﻿using System;
using UnityEngine;

namespace Mirko.HoloToolkitExtensions
{
    public class GameObjectEventArgs : EventArgs
    {
        public GameObject GameObject { get; private set; }

        public GameObjectEventArgs(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}