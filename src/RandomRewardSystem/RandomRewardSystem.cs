namespace muShell.Reward
{
    public struct RandomRewardSystem
    {

        private int[] Probs;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomRewardSystem"/> class.
        /// </summary>
        /// <param name="Probs">The Probs.</param>
        public RandomRewardSystem(int[] Probs)
        {
            this.Probs = Probs;
        }



        /// <summary>
        /// Reveal the winner and return it index in the probability array.
        /// </summary>
        public int Reveal()
        {
            var rand = new System.Random();

            int sum = System.Linq.Enumerable.Sum(this.Probs);
            int val = rand.Next(1, sum);
            int t_val = 0;
            for (int i = 0; i < this.Probs.Length; ++i)
            {
                t_val += this.Probs[i];

                if (val <= t_val)
                    return i;
            }

            return -1;

        }
    }
}
