namespace muShell.Health
{
    [System.Serializable]
    public class HealthSystem
    {
        [UnityEngine.SerializeField] private float MaxHealth;
        [UnityEngine.SerializeField] private float Health;

        public event Events.HealthSystemEvent onDamaged;
        public event Events.HealthSystemEvent onDead;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthSystem"/> class.
        /// </summary>
        /// <param name="maxHealth">Set max health.</param>
        public HealthSystem(int maxHealth)
        {
            this.MaxHealth = maxHealth;
            this.Health = maxHealth;
        }

        /// <summary>
        /// Invokes Health events.
        /// </summary>
        /// <param name="healthEvent">The health event to invoke.</param>
        private void InvokeEvent(Events.HealthSystemEvent healthEvent)
        {
            healthEvent?.Invoke(this, this.Health);
        }

        /// <summary>
        /// Adds a specific amount of health.
        /// </summary>
        /// <param name="amount">Amount of health to add.</param>
        public void Heal(float amount)
        {
            this.Health = this.Health + amount > this.MaxHealth ? this.MaxHealth : this.Health + amount;
        }

        /// <summary>
        /// Deals a specific amount of damage.
        /// </summary>
        /// <param name="amount">Amount of health to reduce.</param>
        public void DealDamage(float amount)
        {
            this.Health -= amount;
            this.InvokeEvent(this.onDamaged);

            if (this.Health <= 0)
            {
                this.Health = 0;
                this.InvokeEvent(this.onDead);
            }
        }

        /// <summary>
        /// Sets the health back to max.
        /// </summary>
        public void FillHealth()
        {
            this.Health = this.MaxHealth;
        }

        /// <summary>
        /// Gets health.
        /// </summary>
        public float GetHealth()
        {
            return this.Health;
        }

        /// <summary>
        /// Gets max health.
        /// </summary>
        public float GetMaxHealth()
        {
            return this.MaxHealth;
        }
    }
}