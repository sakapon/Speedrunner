using System;
using System.Collections.Generic;

namespace TasSample.Models
{
    public static class ScreenWorkflowManager
    {
        private static Dictionary<Guid, ScreenWorkflowContext> contexts;

        public static Dictionary<Guid, ScreenWorkflowContext> Contexts
        {
            get
            {
                if (contexts == null)
                {
                    contexts = new Dictionary<Guid, ScreenWorkflowContext>();
                }
                return contexts;
            }
        }
    }
}
