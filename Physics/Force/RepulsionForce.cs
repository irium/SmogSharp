// 
// RepulsionForce.cs
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
using System;
using GraphUnfolding.Layout.Layout;
using GraphUnfolding.Layout.Utils;

namespace GraphUnfolding.Layout.Physics.Force
{
	/// <summary>
	/// 	Represents the force of repulsion between particles. This
	/// 	is simulating the coulomb force.
	/// </summary>
	public class RepulsionForce : IForce
	{
		/// <summary>
		/// 	See <see cref="IForce.Apply{TNode,TEdge}"/>.
		/// </summary>
		/// <param name="layout">The layout.</param>
		public void Apply<S, T> (IPhysicalLayout<S, T> layout)
			where S: Node where T: Edge<S>
		{
			foreach (var p in layout.Particles) {
				foreach (var p2 in layout.Particles) {
				    if (p == p2) continue;

				    double dx = p.X - p2.X;
				    double dy = p.Y - p2.Y;
				    double d = Math.Sqrt(dx*dx + dy*dy);
				    double dd = d < 1 ? 1 : d;
				    double ke = .05;
				    double q1 = 1;
				    double q2 = 1;
				    double F = (ke * q1 * q2) / dd * dd;
						
				    p.XForce += F * dx / dd;
				    p.YForce += F * dy / dd;
				}
			}
		}
	}
}

