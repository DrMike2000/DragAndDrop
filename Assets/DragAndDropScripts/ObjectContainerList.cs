using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
    public abstract class ObjectContainerList<T> : ObjectContainer where T : UnityEngine.Object
    {
        // Start is called before the first frame update
        protected List<T> objects;
        protected Slot[] slots;

        public override void Drop()
        {
            // after a drop, update our internal array based on the UI charms list's contents
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != slots[i].item.obj)
                {
                    objects[i] = slots[i].item.obj as T;
                    NotifyChange(i);
                }
            }
        }

        public virtual void NotifyChange(int index) { }

        // Use this for initialization
        protected void CreateSlots(List<T> list)
        {
            // hook up the appropriate array. This is a reference, so we're now writing to the player data if we change this
            objects = list;
            slots = new Slot[objects.Count];

            if (preMadeSlots.Length == 0)
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

            // create a Slot for each object in the list, or use a premade one
            for (int i = 0; i < objects.Count; i++)
            {
                Slot premade = preMadeSlots != null && preMadeSlots.Length > i ? preMadeSlots[i] : null;
                slots[i] = MakeSlot(objects[i], premade);
            }
        }

        protected void UpdateSlots(int notUsed)
        {
            for (int i = 0; i < objects.Count; i++)
                slots[i].item.SetObject(objects[i]);
        }

        // to be called from events
        public void HighlightSlots(bool on)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (on)
                    slots[i].OnDraggableEnter();
                else
                    slots[i].OnDraggableExit();
            }
        }
    }
}