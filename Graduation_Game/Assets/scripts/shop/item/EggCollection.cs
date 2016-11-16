using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.scripts.shop.item {
	[Serializable]
	public class EggCollection {
		private Dictionary<string, PenguinEgg> eggs = new Dictionary<string, PenguinEgg>();

		public void Save() {
			var bf = new BinaryFormatter();
			var file = File.Open(Application.persistentDataPath + "/eggCollection", FileMode.OpenOrCreate);

			bf.Serialize(file, this);
			file.Close();
		}

		public void Load() {
			if ( !File.Exists(Application.persistentDataPath + "/eggCollection") ) {
				return;
			}

			var bf = new BinaryFormatter();
			var file = File.Open(Application.persistentDataPath + "/eggCollection", FileMode.Open);

			var collection = (EggCollection) bf.Deserialize(file);
			eggs = collection.eggs;
		}

		public void AddEgg(PenguinEgg egg) {
			eggs.Add(egg.ID, egg);
		}
	}
}