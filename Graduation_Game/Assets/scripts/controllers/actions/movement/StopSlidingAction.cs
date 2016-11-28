using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement {
    public class StopSlidingAction :Action{
        private Directionable directionable;

        public StopSlidingAction(Directionable d)  {
            directionable = d;
        }

        public void Setup(GameObject gameObject) {

        }

        public void Execute() {
            directionable.SetSlide(false);
        }
    }
}