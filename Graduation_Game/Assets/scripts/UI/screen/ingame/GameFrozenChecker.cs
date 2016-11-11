using Assets.scripts.gamestate;

namespace Assets.scripts.UI.screen.ingame {
    /// <summary>
    /// Classes that implement this interface should check if GameStateManager.IsGameFrozen is true/false and act accordingly
    /// </summary>
    public interface GameFrozenChecker {
        void SetGameStateManager(GameStateManager manager);
    }
}