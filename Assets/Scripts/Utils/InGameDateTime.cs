using System;

namespace Utils {
    public static class InGameDateTimeConversions {
        public const int SecondsPerMinute = 60;
        public const int MinutesPerHour = 60;
        public const int HoursPerDay = 24;
        public const int DaysPerYear = 100;

        public const int SecondsPerHour = SecondsPerMinute * MinutesPerHour;
        public const int SecondsPerDay = SecondsPerHour * HoursPerDay;

        public static int Day(float secondsSinceEpoch) {
            return (int) (secondsSinceEpoch / SecondsPerDay % DaysPerYear);
        }

        public static int Hour(float secondsSinceEpoch) {
            return (int) (secondsSinceEpoch / SecondsPerHour % HoursPerDay);
        }

        public static int Minute(float secondsSinceEpoch) {
            return (int) (secondsSinceEpoch / SecondsPerMinute % MinutesPerHour);
        }

        public static int Second(float secondsSinceEpoch) {
            return (int) (secondsSinceEpoch % SecondsPerMinute);
        }
    }

    public class InGameDateTime : IComparable<InGameDateTime>, IEquatable<InGameDateTime> {
        private readonly float seconds;

        public InGameDateTime(float secondSinceEpoch) {
            this.seconds = secondSinceEpoch;
        }

        public int Day => InGameDateTimeConversions.Day(this.seconds);
        public int Hour => InGameDateTimeConversions.Hour(this.seconds);
        public int Minute => InGameDateTimeConversions.Minute(this.seconds);
        public int Second => InGameDateTimeConversions.Second(this.seconds);

        #region IComparable<InGameDateTime> Members

        public int CompareTo(InGameDateTime other) {
            if (other == null) {
                return 1;
            }

            return this.seconds.CompareTo(other.seconds);
        }

        #endregion

        #region IEquatable<InGameDateTime> Members

        public bool Equals(InGameDateTime other) {
            if (other == null) {
                return false;
            }

            return this.seconds.Equals(other.seconds);
        }

        #endregion
    }
}