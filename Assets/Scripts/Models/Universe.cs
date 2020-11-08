using System;
using Utils;

namespace Models {
    public class Universe {
        private Action<float> cbTimeSpeedMultiplierChanged;

        public float GlobalTime { get; protected set; }
        public float TimeSpeedMultiplier { get; protected set; } = 85f;

        public int Hour => InGameDateTimeConversions.Hour(this.GlobalTime);
        public int Minute => InGameDateTimeConversions.Minute(this.GlobalTime);
        public int Second => InGameDateTimeConversions.Second(this.GlobalTime);

        private Schedule schedule = new Schedule();

        public void At(float time, Action<float> cb) {
            this.schedule.At(time, new ActionScheduleEvent(cb));
        }
        
        public void At(float time, IScheduleEvent scheduleEvent) {
            this.schedule.At(time, scheduleEvent);
        }

        public void SetGlobalTime(float time) {
            this.GlobalTime = time;
            this.schedule.Tick(time);
        }

        public void SetTimeSpeedMultiplier(float multiplier) {
            this.TimeSpeedMultiplier = multiplier;
            this.cbTimeSpeedMultiplierChanged.Invoke(this.TimeSpeedMultiplier);
        }

        public void RegisterTimeSpeedMultiplierChangedCallback(Action<float> cb) {
            this.cbTimeSpeedMultiplierChanged += cb;
        }
    }
}