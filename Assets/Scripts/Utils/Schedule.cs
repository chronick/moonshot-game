using System;
using Priority_Queue;

namespace Utils {
    public interface IEvent<T> {
        void OnEventTriggered(T arg);
    }
    
    public interface IScheduleEvent : IEvent<float> { }

    public class ActionScheduleEvent : IScheduleEvent {
        private readonly Action<float> cb;

        public ActionScheduleEvent(Action<float> cb) {
            this.cb = cb;
        }

        #region IScheduleEvent Members

        public void OnEventTriggered(float time) {
            this.cb.Invoke(time);
        }

        #endregion
    }

    public interface ISchedule {
        void At(float time, IScheduleEvent scheduleEvent);
        void Tick(float time);
    }

    public class Schedule : ISchedule {
        private readonly SimplePriorityQueue<IScheduleEvent, float> queue =
            new SimplePriorityQueue<IScheduleEvent, float>();

        public void At(float time, IScheduleEvent scheduleEvent) {
            this.queue.Enqueue(scheduleEvent, time);
        }

        public void Tick(float time) {
            while (this.NextTime() <= time) {
                this.ExecuteNext(time);
            }
        }

        private float NextTime() {
            var nextTime = float.MaxValue;
            if (this.queue.TryFirst(out var first)) {
                this.queue.TryGetPriority(first, out nextTime);
            }

            return nextTime;
        }

        private void ExecuteNext(float time) {
            if (this.queue.TryDequeue(out var first)) {
                first.OnEventTriggered(time);
            }
        }
    }
}