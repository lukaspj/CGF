﻿#region License
// Copyright (c) 2009 Sander van Rossen, 2013 Oliver Salzburg
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

namespace Graph
{
	[Flags]
	public enum RenderState
	{
		None			= 0,
		Focus			= 1,	// This element has focus
		Connected		= 2,	// This element is connected to another element
		Hover			= 4,	// We're hovering over this element
		DraggedOver		= 8,	// We're dragging over this element
		Dragging		= 16,	// We're dragging (or dragging from) this element
		Incompatible	= 32,	// Connector is shown as not being able to be connected to
		Compatible		= 64	// Connector is shown as being able to be connected to
	}
}
