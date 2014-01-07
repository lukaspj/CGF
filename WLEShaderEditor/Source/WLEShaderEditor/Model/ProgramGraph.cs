using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using ShaderModuleAPI;

namespace WLEShaderEditor.Model
{
   class ProgramGraph
   {
      internal class Edge
      {
         public Vertex From;
         public Vertex To;
      }

      internal class Vertex
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

      List<Vertex> Vertices;
      List<Edge> Edges;

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
               To = vTo
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
            CalculateDepthForVertex(e.To);
      }
   }
}
