﻿#region License
// Copyright (c) 2009 Sander van Rossen
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Graph
{
	public sealed class NodeItemEventArgs : EventArgs
	{
		public NodeItemEventArgs(NodeItem item) { Item = item; }
		public NodeItem Item { get; private set; }
	}

	public abstract class NodeItem : IElement
	{
		public NodeItem()
		{
			this.Input		= new NodeInputConnector(this, false);
			this.Output		= new NodeOutputConnector(this, false);
		}

		public NodeItem(bool enableInput, bool enableOutput)
		{
			this.Input		= new NodeInputConnector(this, enableInput);
			this.Output		= new NodeOutputConnector(this, enableOutput);
		}

      public NodeItem(bool enableInput, bool enableOutput, Type[] inputTypes, Type[] outputTypes  )
      {
         this.InputTypes = inputTypes;
         this.OutputTypes = outputTypes;
         this.Input = new NodeInputConnector(this, enableInput);
         this.Output = new NodeOutputConnector(this, enableOutput);
      }

		public Node					Node			{ get; internal set; }
		public object				Tag				{ get; set; }

      public Type[] InputTypes { get; set; }
      public Type[] OutputTypes { get; set; }

      public object OutputData;

		public NodeConnector		Input			{ get; private set; }
		public NodeConnector		Output			{ get; private set; }

		internal RectangleF			bounds;
		internal RenderState		state			= RenderState.None;

		public virtual bool			OnClick()					 { return false; }
		public virtual bool			OnDoubleClick()				 { return false; }
		public virtual bool			OnStartDrag(PointF location, out PointF original_location) { original_location = Point.Empty; return false; }
		public virtual bool			OnDrag(PointF location)		 { return false; }		
		public virtual bool			OnEndDrag() 				 { return false; }
		internal abstract SizeF		Measure(Graphics context);
		internal abstract void		Render(Graphics graphics, SizeF minimumSize, PointF position);

		public ElementType ElementType { get { return ElementType.NodeItem; } }
	}
}
