using NUnit.Framework;

namespace Tests.Utils {
    public class InGameDateTimeConversions {
        [Test]
        public void DateTimeSecond() {
            Assert.AreEqual(1, global::Utils.InGameDateTimeConversions.Second(1));
            Assert.AreEqual(0, global::Utils.InGameDateTimeConversions.Second(60));
        }

        [Test]
        public void DateTimeMinute() {
            Assert.AreEqual(0, global::Utils.InGameDateTimeConversions.Minute(1));
            Assert.AreEqual(1, global::Utils.InGameDateTimeConversions.Minute(60));
            Assert.AreEqual(0, global::Utils.InGameDateTimeConversions.Minute(3600));
        }

        [Test]
        public void DateTimeHour() {
            Assert.AreEqual(0, global::Utils.InGameDateTimeConversions.Hour(1));
            Assert.AreEqual(1, global::Utils.InGameDateTimeConversions.Hour(3600));
            Assert.AreEqual(0, global::Utils.InGameDateTimeConversions.Hour(86400));
        }
    }
}