using System;
// using Priority_Queue;

namespace Utils {
    public interface IScheduleEvent {
        void OnEventTriggered(float time);
    }

    public class ActionScheduleEvent : IScheduleEvent {
        private readonly Action<float> cb;

        public ActionScheduleEvent(Action<float> cb) {
            this.cb = cb;
        }

        public void OnEventTriggered(float time) {
            this.cb.Invoke(time);
        }
    }

    public class Schedule {
        // private readonly SimplePriorityQueue<IScheduleEvent, float> queue =
        //     new SimplePriorityQueue<IScheduleEvent, float>();
        
        public void At(float time, IScheduleEvent scheduleEvent) {
            // this.queue.Enqueue(scheduleEvent, time);
        }
        
        public void Tick(float time) {
            // while (this.NextTime() <= time) {
            //     this.ExecuteNext(time);
            // }
        }
        
        private float NextTime() {
            // var nextTime = float.MaxValue;
            // if (this.queue.TryFirst(out var first)) {
            //     this.queue.TryGetPriority(first, out nextTime);
            // }
            //
            // return nextTime;
            return 0f;
        }
        
        private void ExecuteNext(float time) {
            // if (this.queue.TryDequeue(out var first)) {
            //     first.OnEventTriggered(time);
            // }
        }
    }
}