using System;
using Utils;

namespace Models {
    public class Game {
        private Action<float> cbTimeSpeedMultiplierChanged;

        private readonly Schedule schedule = new Schedule();

        public Game(float missionTime) {
            this.MissionTime = missionTime;
        }

        public float GlobalTime { get; protected set; }
        public float TimeSpeedMultiplier { get; protected set; } = 1f;

        public float MissionTime { get; protected set; }
        
        public InGameDateTime Now => new InGameDateTime(this.GlobalTime);

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
            this.cbTimeSpeedMultiplierChanged?.Invoke(this.TimeSpeedMultiplier);
        }

        public void RegisterTimeSpeedMultiplierChangedCallback(Action<float> cb) {
            this.cbTimeSpeedMultiplierChanged += cb;
        }
    }
}