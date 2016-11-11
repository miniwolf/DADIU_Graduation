using UnityEngine;

namespace Assets.scripts.gamestate
{
    public class GameStateManager : MonoBehaviour
    {
        private bool isGameFrozen;

        public void SetGameFrozen(bool frozen)
        {
            isGameFrozen = frozen;
        }

        public bool IsGameFrozen()
        {
            return isGameFrozen;
        }

    }
}