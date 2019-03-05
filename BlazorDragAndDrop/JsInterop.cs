using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDragAndDrop
{
    public class JsInterop
    {

        internal static Task<bool> HandleDrag(string elementID, int id, DotNetObjectRef dotnetHelper)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsFunctions.handleDragStart", elementID, id, dotnetHelper);
        }


        internal static Task<bool> HandleDrop(string elementID, int id, DotNetObjectRef dotnetHelper)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsFunctions.handleDrop", elementID, id, dotnetHelper);
        }


    }
}
