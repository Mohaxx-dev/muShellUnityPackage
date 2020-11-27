using UnityEngine;

namespace muShell.ItemSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private Sprite Thumbnail;
        [SerializeField] private string Name;
        [SerializeField] private int MaxStack = 1;


        /// <summary>
        /// Gets thumbnail.
        /// </summary>
        public Sprite GetThumbnail()
        {
            return this.Thumbnail;
        }

        /// <summary>
        /// Gets name.
        /// </summary>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// Gets max stack.
        /// </summary>
        public int GetMaxStack()
        {
            return this.MaxStack;
        }
    }
}