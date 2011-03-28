// 
// PhysicalLayout.cs
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
using System.Collections.Generic;
using GraphUnfolding.Layout.Physics;
using GraphUnfolding.Layout.Utils;

namespace GraphUnfolding.Layout.Layout
{
	/// <summary>
	/// 	Represents a physical layout, i.e. a layout computed by
	/// 	simulating a physical system composed by particles and springs.
	/// </summary>
	public interface IPhysicalLayout<TNode, TEdge> : IGraphLayout<TNode, TEdge>
		where TNode: Node where TEdge: Edge<TNode>
	{
		/// <summary>
		/// 	The list of springs
		/// </summary>
		List<Spring<TNode, TEdge>> Springs { get ; }
		
		/// <summary>
		/// 	The list of particles
		/// </summary>
		List<Particle<TNode>> Particles { get; }
	}
}

