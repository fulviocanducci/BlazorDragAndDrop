using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDragAndDrop.Pages
{
    public class IndexBase : ComponentBase
    {
        bool firtsLoad = true;
        public List<MyDraggable> listDraggable = new List<MyDraggable>();

        public List<MyDragTarget> listDragTarget = new List<MyDragTarget>();

        protected override void OnInit()
        {

            for (int i = 0; i < 3; i++)
            {
                listDragTarget.Add(new MyDragTarget
                {
                    ID = listDragTarget.Count,
                    ElementID = "DropTargetdDiv" + listDragTarget.Count,

                });
            }

            foreach (var item in listDragTarget)
            {
                for (int i = 0; i < 5; i++)
                {
                    AddItem(item.ID);
                }
            }


            base.OnInit();
        }


        private void AddItem(int parentID)
        {
            int _id = listDraggable.Count + 1;
            listDraggable.Add(new MyDraggable
            {
                ID = _id,
                Name = "item" + _id,
                ElementID = "draggableDiv" + _id,
                ParentID = parentID,
            });
        }


        protected override void OnAfterRender()
        {
            if (firtsLoad)
            {
                firtsLoad = false;
                RegisterJsEvents();
            }


            base.OnAfterRender();
        }

        public void RegisterJsEvents()
        {
            foreach (var item in listDragTarget)
            {
                JsInterop.HandleDrop(item.ElementID, item.ID, new DotNetObjectRef(this));
            }
        }


        public void OnMouseDown(UIMouseEventArgs e, MyDraggable item)
        {
            JsInterop.HandleDrag(item.ElementID, item.ID, new DotNetObjectRef(this));
        }

        [JSInvokable]
        public void InvokeDragStartFromJS(int id)
        {
            //if you need to know for some reason when drag is started this method will be invoked
        }

        [JSInvokable]
        public void InvokeDropFromJS(int parentID, int id)
        {
            if (listDraggable.Any(x => x.ID == id))
            {
                listDraggable.Single(x => x.ID == id).ParentID = parentID;

                StateHasChanged();
            }
        }

    }


    public class MyDraggable
    {

        public int ID { get; set; }

        public string ElementID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
    }

    public class MyDragTarget
    {

        public int ID { get; set; }

        public string ElementID { get; set; }
    }
}
