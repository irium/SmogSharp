// 
// Edge.cs
//  
// Author:
//       Antoine Cailliau <antoine.cailliau@uclouvain.be>
// 
// Copyright (c) 2010 UCLouvain
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

namespace GraphUnfolding.Layout.Utils
{
	
	/// <summary>
	///		Represents a directed edge between two nodes
	/// </summary>
	public class Edge<TNode> where TNode: Node
	{
		/// <summary>
		/// 	The source node of the edge
		/// </summary>
		public TNode Source  { get; private set; }
		
		/// <summary>
		/// 	The target node of the edge.
		/// </summary>
		public TNode Target  { get; private set; }
		
		/// <summary>
		/// 	Creates a new edge from <c>source</c> to <c>target</c>.
		/// </summary>
		/// <param name="source">
		/// 	A node representing the source of the edge.
		/// </param>
		/// <param name="target">
		/// 	A node representing the target of the edge.
		/// </param>
		public Edge (TNode source, TNode target)
		{
			Source = source;
			Target = target;
		}
	}
}

