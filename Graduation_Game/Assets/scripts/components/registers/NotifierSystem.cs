using System;
using System.Collections.Generic;
using Assets.scripts.character;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.components.registers
{
    public class NotifierSystem : MonoBehaviour
    {
        public enum Event
        {
            PenguinDied
        }

        private Dictionary<Event, List<Notifiable>> notifiers = new Dictionary<Event, List<Notifiable>> {{Event.PenguinDied, new List<Notifiable>()}};

        public void Register(Event eve, Notifiable n)
        {
            Debug.Log("Penguin added" + n);
            notifiers[eve].Add(n);
        }

        public void PenguinDied(GameObject penguin)
        {
            // Penguin just died, so it doesn't make sense to notify him about his death :P
            Notifiable killed = penguin.GetComponent<Notifiable>();
            Unregister(Event.PenguinDied, killed);

            foreach (var notifiable in notifiers[Event.PenguinDied]) {
                notifiable.Notify(penguin);
            }
            Debug.Log("---------------------");
        }

        public void Unregister(Event ev, Notifiable listener)
        {
            notifiers[ev].Remove(listener);
        }
    }
}