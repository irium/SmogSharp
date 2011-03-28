// 
// ForceBasedLayout.cs
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
using System.Collections.Generic;
using GraphUnfolding.Layout.Physics;
using GraphUnfolding.Layout.Physics.Force;
using GraphUnfolding.Layout.Utils;

namespace GraphUnfolding.Layout.Layout
{
    /// <summary>
    /// 	Represents a layout computed by simulating forces applied
    /// 	on particles by a set of forces. <code>TNode</code> is the type
    /// 	attached to particles, and <code>TEdge</code> is the type attached
    /// 	to springs.
    /// </summary>
    public class ForceBasedLayout<TNode, TEdge> : IPhysicalLayout<TNode, TEdge>
        where TNode: Node
        where TEdge: Edge<TNode>
    {
        /// <summary>
        /// 	Whether bounds are enforced.
        /// </summary>
        private bool enforceBounds;
        
        /// <summary>
        /// 	Wheter bounds are enforced. It is not possible
        ///  	to enforce bounds without proper bounds set.
        /// </summary>
        public bool EnforceBounds  {
            get { return enforceBounds; } 
            set { enforceBounds = value && (topBound > bottomBound && rightBound > leftBound); }
        }
        
        /// <summary>
        /// 	The top bound.
        /// </summary>
        private double topBound;
        
        /// <summary>
        /// 	The bottom bound.
        /// </summary>
        private double bottomBound;
        
        /// <summary>
        /// 	The left bound.
        /// </summary>
        private double leftBound;
        
        /// <summary>
        /// 	The right bound.
        /// </summary>
        private double rightBound;
        
        /// <summary>
        /// 	The bounds of the layout. Bounds are represented clockwise.
        /// 	There are 4 ways to set bounds:
        /// 	<list type="bullet">
        /// 		<item><code>{ top, right, bottom, left }</code></item>
        /// 		<item><code>{ top, right and left, bottom }</code></item>
        /// 		<item><code>{ top and bottom, right and left }</code></item>
        /// 		<item><code>{ top and right and bottom and left }</code></item>
        /// 	</list>
        /// 	When setting bounds the bounds are automatically enforced
        /// 	if valid.
        /// </summary>
        public double[] Bounds {
            get { return new[] { topBound, rightBound, bottomBound, leftBound }; }
            set
            {
                EnforceBounds = true;
                switch (value.Length)
                {
                    case 4:
                        topBound = value[0];
                        rightBound = value[1];
                        bottomBound = value[2];
                        leftBound = value[3];
                        break;
                    case 3:
                        topBound = value[0];
                        rightBound = value[1];
                        bottomBound = value[2];
                        leftBound = value[1];
                        break;
                    case 2:
                        topBound = value[0];
                        rightBound = value[1];
                        bottomBound = value[0];
                        leftBound = value[1];
                        break;
                    case 1:
                        topBound = value[0];
                        rightBound = value[0];
                        bottomBound = value[0];
                        leftBound = value[0];
                        break;
                    default:
                        EnforceBounds = false;
                        break;
                }
            }
        }
        
        /// <summary>
        /// 	The list of particles. See <see cref="P:Smog.PhysicalLayout.Particles"/>.
        /// </summary>
        public List<Particle<TNode>> Particles  { get; private set; }
        
        /// <summary>
        /// 	The list of springs. See <see cref="P:Smog.PhysicalLayout.Springs"/>.
        /// </summary>
        public List<Spring<TNode, TEdge>> Springs  { get; private set; }
        
        /// <summary>
        /// 	The list of forces acting for the simulation.
        /// </summary>
        public List<IForce> Forces  { get; private set; }
        
        /// <summary>
        /// 	The threshold of energy indicating when the simulation ends.
        /// </summary>
        public double Threshold  { get; private set; }
        
        /// <summary>
        /// 	The damping factor of the graph.
        /// </summary>
        public double Damping  { get; private set; }
        
        /// <summary>
        /// 	Creates a new layout with no particles, no springs and no forces.
        /// </summary>
        public ForceBasedLayout ()
        {
            Particles = new List<Particle<TNode>>();
            Springs = new List<Spring<TNode,TEdge>>();
            Forces = new List<IForce>();
            Threshold = .1; Damping = .9;
        }
        
        /// <summary>
        /// 	Creates a new layout with no particles, no springs and no force.
        /// </summary>
        /// <param name="threshold">
        /// 	A <see cref="T:System.Double"/> representing the threshold.
        /// </param>
        public ForceBasedLayout (double threshold) : this()
        {
            Threshold = threshold;
        }
        
        /// <summary>
        /// 	See <see cref="M:Smog.PhysicalLayout.Init(TNode[],TEdge[])"/>
        /// </summary>
        /// <param name="nodes">
        /// 	A <see cref="N[]"/> represents the nodes of the graph.
        /// </param>
        /// <param name="edges">
        /// 	A <see cref="E[]"/> represents the edges of the graph.
        /// </param>
        public void Init (TNode[] nodes, TEdge[] edges)
        {
            // Reset the nodes and particles
            Particles = new List<Particle<TNode>>(nodes.Length);
            Springs = new List<Spring<TNode,TEdge>>(edges.Length);
            
            var attachedParticle = new Dictionary<TNode, Particle<TNode>>();
            
            // Creates a particle for each node
            var rnd = new Random();
            foreach(var node in nodes) 
            {
                var particle = new Particle<TNode>(rnd.NextDouble()-.5, rnd.NextDouble()-.5) { Value = node };
                Particles.Add(particle);
                attachedParticle.Add(node, particle);
            }
            
            // Creates a spring for each edge
            foreach (var e in edges)
            {
                Springs.Add(new Spring<TNode, TEdge>(attachedParticle[e.Source], attachedParticle[e.Target])
                    {Value = e});
            }
        }
        
        /// <summary>
        /// 	See <see cref="IPhysicalLayout.Terminate()"/>.
        /// </summary>
        public void Terminate ()
        {
        }
        
        /// <summary>
        /// 	See <see cref="M:Smog.PhysicalLayout.ComputeNextStep(int)"/>.
        /// </summary>
        /// <param name="timeStep">
        /// 	A <see cref="T:System.Double"/> representing the timestep
        /// </param>
        /// <returns>
        /// 	A <see cref="T:System.Boolean"/> representing wheter the are
        /// 	more steps.
        /// </returns>
        public bool ComputeNextStep (double timeStep)
        {	
            foreach (var f in Forces)
            {
                f.Apply(this);
            }

            foreach (var p in Particles)
            {
                double ax = p.XForce / p.Mass;
                double ay = p.YForce / p.Mass;

                p.XSpeed = (p.XSpeed + ax * timeStep * timeStep / 2) * Damping;
                p.YSpeed = (p.YSpeed + ay * timeStep * timeStep / 2) * Damping;

                p.X += p.XSpeed * timeStep;
                p.Y += p.YSpeed * timeStep;

                p.XForce = 0;
                p.YForce = 0;
            }

            // Enforce bounds
            if (EnforceBounds)
            {
                foreach (var p in Particles)
                {
                    p.X = Math.Min(topBound, p.X);
                    p.Y = Math.Min(rightBound, p.Y);
                    p.X = Math.Max(bottomBound, p.X);
                    p.Y = Math.Max(leftBound, p.Y);
                }
            }

            // Compute energy
            double kineticEnergy = 0;
            foreach (var p in Particles)
            {
                double speed2 = p.XSpeed * p.XSpeed + p.YSpeed * p.YSpeed;
                kineticEnergy += p.Mass * speed2 / 2;
            }
            
            return kineticEnergy > Threshold;
        }
        
    }
}

