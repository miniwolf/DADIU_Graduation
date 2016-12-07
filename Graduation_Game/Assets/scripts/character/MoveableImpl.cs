namespace Assets.scripts.character {
	public class MoveableImpl : Moveable {
		private bool isStopped;

		public void SetStop(bool stop){
			isStopped = stop;
		}
	}
}
