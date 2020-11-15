using System;
using UnityEngine;
using Utils;

namespace Models {
    public class GameEndData {
        public readonly Game Game;
        public readonly bool IsWin;
        public readonly string Reason;

        public GameEndData(Game game, bool isWin, string reason = null) {
            this.Game = game;
            this.IsWin = isWin;
            this.Reason = reason;
        }
    }

    public class Game {
        private readonly Schedule schedule = new Schedule();

        private Action<GameEndData> cbOnGameEnded;
        private Action<float> cbTimeSpeedMultiplierChanged;

        public Game(float missionTime) {
            this.MissionTime = missionTime;
            this.Ship = new Ship(1000);
        }

        public readonly Ship Ship;

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

        public void Tick(float time) {
            this.GlobalTime = time;
            this.schedule.Tick(time);
            
            // Game Loss condition
            if (!this.Ship.IsAlive()) {
                this.LoseGame("Ship has died somehow.");
                return;
            }

            // Game win condition
            if (this.Ship.IsAlive() && this.GlobalTime >= this.MissionTime) {
                this.WinGame();
            }
        }

        public void SetTimeSpeedMultiplier(float multiplier) {
            this.TimeSpeedMultiplier = multiplier;
            this.cbTimeSpeedMultiplierChanged?.Invoke(this.TimeSpeedMultiplier);
        }

        public void RegisterTimeSpeedMultiplierChangedCallback(Action<float> cb) {
            this.cbTimeSpeedMultiplierChanged += cb;
        }

        public void RegisterOnGameEndCallback(Action<GameEndData> cb) {
            this.cbOnGameEnded += cb;
        }

        private void EndGame(GameEndData data) {
            if (this.cbOnGameEnded != null) {
                this.cbOnGameEnded.Invoke(data);
                return;
            }

            Debug.LogError("Game Ended but no callbacks available to respond!");
        }

        private void WinGame() {
            this.EndGame(new GameEndData(this, true));
        }

        private void LoseGame(string reason) {
            this.EndGame(new GameEndData(this, false, reason));
        }
    }
}