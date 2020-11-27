using UnityEngine;

namespace muShell.ItemSystem
{
    public class WorldItem : UnityEngine.MonoBehaviour, Interfaces.Interactable
    {

        [SerializeField] private muShell.ItemSystem.Item Item;
        
        public void interact()
        {
            if ( this.Item.GetItemTemplate() == null )
            {
                Debug.Log("Item Template not found !! ");
                return;
            }
            
            if( Player.InventorySystem.TryAddItem(this.Item) )
                Destroy(gameObject);        
        }


    }
}