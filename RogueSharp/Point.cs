﻿using System;

namespace RogueSharp
{
   /// <summary>
   /// A class that defines a point in 2D space
   /// </summary>
   public class Point : IEquatable<Point>
   {
      private static readonly Point _zeroPoint = new Point();
      /// <summary>
      /// Specifies the x-coordinate of the Point
      /// </summary>
      public int X { get; set; }
      /// <summary>
      /// Specifies the y-coordinate of the Point
      /// </summary>
      public int Y { get; set; }
      /// <summary>
      /// Initializes a new instace of Point
      /// </summary>
      public Point()
      {
      }
      /// <summary>
      /// Initializes a new instance of Point
      /// </summary>
      /// <param name="x">The x-coordinate of the Point</param>
      /// <param name="y">The y-coordinate of the Point</param>
      public Point( int x, int y )
      {
         X = x;
         Y = y;
      }
      /// <summary>
      /// Returns the point (0,0)
      /// </summary>
      public static Point Zero
      {
         get
         {
            return _zeroPoint;
         }
      }
      /// <summary>
      /// Determines whether two Point instances are equal
      /// </summary>
      /// <param name="other">The Point to compare this instance to</param>
      /// <returns>True if the instances are equal; False otherwise</returns>
      public bool Equals( Point other )
      {
         return ( ( X == other.X ) && ( Y == other.Y ) );
      }
      /// <summary>
      /// Determines whether two Point instances are equal
      /// </summary>
      /// <param name="a">Point on the left side of the equal sign</param>
      /// <param name="b">Point on the right side of the equal sign</param>
      /// <returns>True if a and b are equal; False otherwise</returns>
      public static bool operator ==( Point a, Point b )
      {
         return a.Equals( b );
      }
      /// <summary>
      /// Determines whether two Point instances are not equal
      /// </summary>
      /// <param name="a">Point on the left side of the equal sign</param>
      /// <param name="b">Point on the right side of the equal sign</param>
      /// <returns>True if a and b are not equal; False otherwise</returns>
      public static bool operator !=( Point a, Point b )
      {
         return !a.Equals( b );
      }
      /// <summary>
      /// Determines whether two Point instances are equal
      /// </summary>
      /// <param name="obj">The Object to compare this instance to</param>
      /// <returns>True if the instances are equal; False otherwise</returns>
      public override bool Equals( object obj )
      {
         return ( obj is Point ) ? Equals( (Point) obj ) : false;
      }
      /// <summary>
      /// Gets the hash code for this object which can help for quick checks of equality
      /// or when inserting this Point into a hash-based collection such as a Dictionary or Hashtable 
      /// </summary>
      /// <returns>An integer hash used to identify this Point</returns>
      public override int GetHashCode()
      {
         return X ^ Y;
      }
      /// <summary>
      /// Returns a string that represents the current Point
      /// </summary>
      /// <returns>A string that represents the current Point</returns>
      public override string ToString()
      {
         return string.Format( "{{X:{0} Y:{1}}}", X, Y );
      }
   }
}