namespace muShell.ItemSystem
{
    [System.Serializable]
    public class Item
    {
        [UnityEngine.SerializeField] private ItemSO ItemTemplate;
        [UnityEngine.SerializeField] private int StackCount;


        /// <summary>
        /// Unstacks the item.
        /// </summary>
        public bool TryUnstackItem()
        {
            return --StackCount > 0;
        }

        /// <summary>
        /// Stacks the item.
        /// </summary>
        public bool TryStackItem()
        {
            if (StackCount + 1 <= this.ItemTemplate.GetMaxStack())
            {
                ++StackCount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets stack count.
        /// </summary>
        public int GetStackCount()
        {
            return this.StackCount;
        }
        
        /// <summary>
        /// Gets item template.
        /// </summary>
        public ItemSO GetItemTemplate()
        {
            return this.ItemTemplate;
        }

    }
}