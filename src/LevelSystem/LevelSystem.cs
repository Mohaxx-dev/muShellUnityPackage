namespace muShell.Level
{
    [System.Serializable]
    public class LevelSystem
    {
        [UnityEngine.SerializeField] private int Exp;
        [UnityEngine.SerializeField] private int Level;
        private ExperienceCalculator ExpCalculator;
        public event Events.LevelSystemEvent onExpGain;
        public event Events.LevelSystemEvent onLevelUp;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSystem"/> class.
        /// </summary>
        /// <param name="expCalculator">Sets the exp calculator.</param>
        public LevelSystem(ExperienceCalculator expCalculator)
        {
            this.Level = 1;
            this.Exp = 0;
            this.ExpCalculator = expCalculator;
        }

        /// <summary>
        /// Invokes Level event.
        /// </summary>
        /// <param name="levelEvent">The level event to invoke.</param>
        private void InvokeEvent(Events.LevelSystemEvent levelEvent)
        {
            levelEvent?.Invoke(this, this.Exp, this.Level);
        }

        /// <summary>
        /// Adds a specific amount of levels.
        /// </summary>
        /// <param name="amount">Amount of levels to add.</param>
        public void AddLevels(int amount)
        {
            while (amount-- != 0)
                this.LevelUp();
        }

        /// <summary>
        /// Adds one level.
        /// </summary>
        public void LevelUp()
        {
            this.Level++;

            
            if(this.Exp < this.ExpCalculator(this.Level))
                this.AddExp(this.ExpCalculator(this.Level) - this.Exp);
            

            this.InvokeEvent(this.onLevelUp);
        }

        /// <summary>
        /// Adds a specific amount of exp.
        /// </summary>
        /// <param name="amount">Amount of exp to add.</param>
        public void AddExp(int amount)
        {
            this.Exp += amount;
            this.InvokeEvent(this.onExpGain);

            if (this.Exp >= this.ExpCalculator(this.Level + 1))
                this.LevelUp();
        }

        /// <summary>
        /// Gets level.
        /// </summary>
        public int GetLevel() => this.Level;

        /// <summary>
        /// Gets exp.
        /// </summary>
        public int GetExp() => this.Exp;

        /// <summary>
        /// Gets next level exp.
        /// </summary>
        public int GetNextLevelExp() => this.ExpCalculator(this.Level + 1);

        /// <summary>
        /// Gets needed exp to next level.
        /// </summary>
        public int GetNeededExpToNextLevel() => this.GetNextLevelExp() - this.Exp;
    }
}