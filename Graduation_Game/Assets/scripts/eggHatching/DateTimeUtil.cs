using System;

namespace Assets.scripts.eggHatching {
    public class DateTimeUtil {
        public static int Seconds() {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
            return currentEpochTime;
        }
    }
}