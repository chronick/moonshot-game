namespace Models {
    public class Ship {
        public float Hull;

        public Ship(float hull) {
            this.Hull = hull;
        }

        public bool IsAlive() {
            if (this.Hull <= 0) return false;

            return true;
        }
    }
}