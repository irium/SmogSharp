// 
// SpringForce.cs
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
	/// 	Represents the force applied by springs on particles.
	/// </summary>
	public class SpringForce : IForce
	{
	    /// <summary>
		/// 	See <see cref="M:Smog.Force.Apply(PhysicalLayout)"/>.
		/// </summary>
		/// <param name="layout">
		/// 	A <see cref="T:PhysicalLayout"/> representing a physical layout.
		/// </param>
		public void Apply<TNode, TEdge> (IPhysicalLayout<TNode, TEdge> layout)
			where TNode: Node where TEdge: Edge<TNode>
		{
	        foreach (var p in layout.Particles)
	        {
	            foreach (var spring in layout.Springs)
	            {
	                if (!(spring.Particle1 == p || spring.Particle2 == p)) continue;

	                var adjacent = spring.Particle1 == p ? spring.Particle2 : spring.Particle1;

	                double dx = p.X - adjacent.X;
	                double dy = p.Y - adjacent.Y;

	                double distance = Math.Sqrt(dx * dx + dy * dy);
	                double dd = distance < 1 ? 1 : distance;

	                if (distance == 0)
	                {
	                    var random = new Random();
	                    dx = .01 * (random.NextDouble() - 0.5);
	                    dy = .01 * (random.NextDouble() - 0.5);
	                }

	                double k = spring.Strength * (distance - spring.Length);

	                double fx = -k * dx / dd;
	                double fy = -k * dy / dd;

	                p.XForce += fx;
	                p.YForce += fy;
	            }
	        }
		}
	}
}

