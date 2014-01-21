using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace CGF
{
   public class ProgramGraph
   {
      List<Vertex> Vertices;
      List<Edge> Edges;

      private int maxDepth = 0;

      /// <summary>
      /// Initializes a new instance of the ProgramGraph class.
      /// </summary>
      public ProgramGraph(GraphModel model)
      {
         Vertices = new List<Vertex>();
         Edges = new List<Edge>();
         foreach (Node node in model.NodeIdentifierDict.Keys)
            Vertices.Add(new Vertex() { Data = node, Depth = -1 });
         foreach (NodeConnection NC in model.NodeConnections)
         {
            Vertex vFrom, vTo;
            vFrom = vTo = default(Vertex);
            foreach (Vertex v in Vertices)
               if (v.Data == NC.From.Node)
                  vFrom = v;
               else if (v.Data == NC.To.Node)
                  vTo = v;
            Edge edge = new Edge()
            {
               From = vFrom,
               FromItem = NC.From.Item,
               To = vTo,
               ToItem = NC.To.Item
            };
            vTo.EdgesIn.Add(edge);
            vFrom.EdgesOut.Add(edge);
            Edges.Add(edge);
         }
         CalculateDepths();
      }

      private void CalculateDepths()
      {
         List<Vertex> inputVertices = new List<Vertex>();
         foreach (Vertex v in Vertices)
            if (isInputVertex(v))
               inputVertices.Add(v);
         foreach (Vertex inputVertice in inputVertices)
            CalculateDepthForVertex(inputVertice);
      }

      private bool isInputVertex(Vertex v)
      {
         foreach(NodeItem item in v.Data.Items)
            if(item.Input.Enabled)
               return false;
         return true;
      }

      private void CalculateDepthForVertex(Vertex v)
      {
         if (v.Depth == -1)
            v.Depth = 0;
         else
            foreach (Edge e in v.EdgesIn)
               v.Depth = Math.Max(v.Depth, e.From.Depth + 1);
         foreach (Edge e in v.EdgesOut)
         {
            e.To.Depth = v.Depth + 1;
            CalculateDepthForVertex(e.To);
         }
         maxDepth = Math.Max(v.Depth, maxDepth);
      }

      public int getMaxDepth() { return maxDepth; }

      public List<Vertex> getVerticesForLayer(int layer) {
         List<Vertex> retList = new List<Vertex>();
         foreach (Vertex v in Vertices)
            if (v.Depth == layer)
               retList.Add(v);
         return retList;
      }
   }

   public class Edge
   {
      public Vertex From;
      public Vertex To;
      public NodeItem FromItem;
      public NodeItem ToItem;
   }

   public class Vertex
   {
      private List<Edge> _edgesIn;
      private List<Edge> _edgesOut;
      public Node Data;
      public int Depth;
      public List<Edge> EdgesIn
      {
         get
         {
            if (_edgesIn == null)
               _edgesIn = new List<Edge>();
            return _edgesIn;
         }
      }
      public List<Edge> EdgesOut
      {
         get
         {
            if (_edgesOut == null)
               _edgesOut = new List<Edge>();
            return _edgesOut;
         }
      }
   }
}
