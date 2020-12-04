using UnityEngine;

namespace muShell.Sandbox
{
    public class Builder : MonoBehaviour
    {

        public static Builder Instance { get; set; }
        public new bool enabled 
        {
            get
            {
                return this.StartBuilding == this.__StartBuilding;
            }
            
            set
            {
                this.SetEnabled(value);
            }
        }
        public delegate void BuildingProcessBeginFunction(GameObject obj);
        public BuildingProcessBeginFunction StartBuilding;
        
        private GameObject Object;
        private GameObject HintObject;

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Cannot have more than one instances of the Builder object !");
                return;
            }

            Instance = this;

            Instance.enabled = true;
        }

        void Start()
        {
            this.StartBuilding = this.__StartBuilding;
        }

        /// <summary>
        /// Removes the hint gameobject.
        /// </summary>
        private void RemoveHint()
        {
            if (this.HintObject != null)
            {
                Destroy(this.HintObject);
                this.HintObject = null;
            }
        }

        /// <summary>
        /// Resets the builder script.
        /// </summary>
        private void ResetBuilder()
        {
            this.RemoveHint();

            this.Object = null;
        }

        /// <summary>
        /// Removes scripts, Rigidbodies, Colliders from the object and it childrens.
        /// </summary>
        private void ClearComponents()
        {
            if (!this.isBuilding())
            {
                return;
            }

            MonoBehaviour[] scripts = this.HintObject.GetComponentsInChildren<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                Destroy(script);
            }


            Rigidbody[] rigidbodies = this.HintObject.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                Destroy(rigidbody);
            }


            Collider[] colliders = this.HintObject.GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                collider.isTrigger = true;
            }

        }

        /// <summary>
        /// Starts building the prefab.
        /// </summary>
        /// <param name="obj">The building prefab.</param>
        private void __StartBuilding(GameObject obj)
        {
            this.ResetBuilder();

            this.Object = obj;

            this.HintObject = Instantiate(this.Object);
            this.ClearComponents();

        }

        public void SetEnabled(bool value)
        {
            if ( value )
            {
                this.StartBuilding = this.__StartBuilding;
                return;
            }

            this.StartBuilding = (GameObject obj) => {};
        }

        public bool isBuilding()
        {
            return this.HintObject != null;
        }

        /// <summary>
        /// Updates hint object position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="normal">The normal vector.</param>
        public void UpdateHintObjectPosition(Vector3 position, Vector3 normal)
        {
            if (this.HintObject == null)
            {
                Debug.LogWarning("No Hint object to update position");
                return;
            }

            this.HintObject.transform.localPosition = position;

            if (normal != null)
            {
                this.HintObject.transform.localRotation = Quaternion.FromToRotation(Vector3.up, normal);
            }
        }

        /// <summary>
        /// Places the object.
        /// </summary>
        public void Build()
        {
            Instantiate(this.Object, this.HintObject.transform.position, this.HintObject.transform.rotation);

            this.ResetBuilder();
        }

    }
}