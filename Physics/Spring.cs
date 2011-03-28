// 
// Spring.cs
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
namespace GraphUnfolding.Layout.Physics
{
    /// <summary>
    /// 	Represents a physical spring. A spring is defined by its strenght
    /// 	and its natural length. An other important characteristics is the
    /// 	damping factor of the spring.
    /// </summary>
    public class Spring<TNode, TEdge>
    {
        /// <summary>
        /// 	One of the particle attached to the spring.
        /// </summary>
        public Particle<TNode> Particle1  { get; private set; }
        
        /// <summary>
        /// 	The other particle attached to the spring.
        /// </summary>
        public Particle<TNode> Particle2  { get; private set; }
        
        /// <summary>
        /// 	The strength of the spring.
        /// </summary>
        public double Strength  { get; private set; }
        
        /// <summary>
        /// 	The natural length of the spring.
        /// </summary>
        public double Length  { get; private set; }
        
        /// <summary>
        /// 	The damping factor of the spring.
        /// </summary>
        public double Damping  { get; private set; }
        
        /// <summary>
        /// 	A tag to attach elements to the spring.
        /// </summary>
        public TEdge Value  { get; set; }
        
        /// <summary>
        /// 	Creates a new spring between the two given particles, of
        /// 	unitary length and strenght.
        /// </summary>
        /// <param name="p1">One particle.</param>
        /// <param name="p2">Other particle/.
        /// </param>
        public Spring (Particle<TNode> p1, Particle<TNode> p2)
        {
            Particle1 = p1;
            Particle2 = p2;
            Strength = 1;
            Length = 1;
            Damping = 1;//Length / 10;
        }
        
        /// <summary>
        ///		Creates a new spring between the two given particles, of unitary
        ///		length and given strenght.
        /// </summary>
        /// <param name="particle1">The one particle.</param>
        /// <param name="particle2">The other particle.</param>
        /// <param name="strenght">The strenght of the spring.</param>
        public Spring (Particle<TNode> particle1, Particle<TNode> particle2, double strenght) 
            : this(particle1, particle2)
        {
            Strength = strenght;
        }
        
        /// <summary>
        /// 	Creates a new spring between the given particles of given length
        /// 	and strenght.
        /// </summary>
        /// <param name="particle1">The one particle.</param>
        /// <param name="particle2">The other particle.</param>
        /// <param name="strenght">The strenght of the spring.</param>
        /// <param name="length">The length of the spring.</param>
        public Spring (Particle<TNode> particle1, Particle<TNode> particle2, double strenght, double length)
            : this(particle1, particle2, strenght)
        {
            Length = length;
            Damping = Length / 10;
        }
    
        /// <summary>
        /// 	Creates a new spring between the given particles of given length
        /// 	and strenght. The damping factor is equal to the given one.
        /// </summary>
        /// <param name="particle1">The one particle.</param>
        /// <param name="particle2">The other particle.</param>
        /// <param name="strenght">The strenght of the spring.</param>
        /// <param name="length">The length of the spring.</param>
        /// <param name="damping">The damping factor of the spring.</param>
        public Spring (Particle<TNode> particle1, Particle<TNode> particle2, double strenght, double length, double damping)
        {
            Particle1 = particle1;
            Particle2 = particle2;
            Strength = strenght;
            Length = length;
            Damping = damping;
        }
        
    }
}

