namespace Assets.scripts.UI.inventory {
	public interface Item<T> {
		T GetValue();
		void SetValue(T value);
	}
}