﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graph;

namespace CGF
{
   public interface ISerializationStrategy
   {
      void DeserializeFile(string file, ref Dictionary<Node, int> nodeIdDict, ref Dictionary<int, Node> nodeIdReverseDict, ref List<NodeConnection> connections);

      string SerializeModel(GraphModel model);
   }
}
