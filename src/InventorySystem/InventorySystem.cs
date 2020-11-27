using System.Collections.Generic;
using muShell.ItemSystem;

namespace muShell.Inventory
{
    [System.Serializable]
    public class InventorySystem
    {

        private List<Item> Items;

        [UnityEngine.SerializeField] private int Size = 1;

        public event Events.InventorySystemEvent onItemAdded;
        public event Events.InventorySystemEvent onItemRemoved;


        /// <summary>
        /// Initializes a new instance of the <see cref="InventorySystem"/> class.
        /// </summary>
        public InventorySystem()
        {
            Items = new List<Item>();

            if (this.Size < 1) this.Size = 1;
        }

        /// <summary>
        /// Invokes Inventory events.
        /// </summary>
        /// <param name="inventoryEvent">The inventory event.</param>
        /// <param name="item">The item.</param>
        private void InvokeEvent(Events.InventorySystemEvent inventoryEvent, Item item)
        {
            inventoryEvent?.Invoke(this, item);
        }

        /// <summary>
        /// Removes item from the inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        public void RemoveItem(Item item)
        {
            foreach (Item item_ in this.Items)
            {
                if (item_.GetItemTemplate() == item.GetItemTemplate())
                {
                    if (!item_.TryUnstackItem())
                    {
                        this.Items.Remove(item_);
                        this.InvokeEvent(this.onItemRemoved, item_);
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// Adds item to the inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        public bool TryAddItem(Item item)
        {
            foreach (Item item_ in this.Items)
            {
                if (item_.GetItemTemplate() == item.GetItemTemplate())
                {
                    while (item.GetStackCount() > 0 && item_.TryStackItem() && item.TryUnstackItem()) ;

                    if (item.GetStackCount() == 0)
                    {
                        this.InvokeEvent(this.onItemAdded, item_);
                        return true;
                    }
                }
            }

            if (this.Items.Count < this.Size && item.GetStackCount() > 0)
            {
                this.Items.Add(item);
                this.InvokeEvent(this.onItemAdded, item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets size of the inventory.
        /// </summary>
        /// <param name="size">The size.</param>
        public void SetSize(int size)
        {
            this.Size = size;
        }

        /// <summary>
        /// Gets all items in the inventory.
        /// </summary>
        public List<Item> GetItems()
        {
            return this.Items;
        }
    }
}
