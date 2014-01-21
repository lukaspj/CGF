using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Graph.Compatibility;

namespace PostFxUI.Variant
{
   class TypeCompatibilityStrategy : ICompatibilityStrategy
   {
      public bool CanConnect(NodeConnector from, NodeConnector to)
      {
         if (from.Item.OutputTypes == null ||
             to.Item.InputTypes == null)
            return false;
         foreach (var outputType in from.Item.OutputTypes)
         {
            foreach (var inputType in to.Item.InputTypes)
            {
               if (outputType == inputType)
                  return true;
            }
         }
         return false;
      }
   }
}
