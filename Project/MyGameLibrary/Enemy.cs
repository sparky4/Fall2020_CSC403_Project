﻿using System.Drawing;

namespace Fall2020_CSC403_Project.code {
  /// <summary>
  /// This is the class for an enemy
  /// </summary>
  public class Enemy : BattleCharacter {
    /// <summary>
    /// THis is the image for an enemy
    /// </summary>
    public Image Img { get; set; }

    /// <summary>
    /// this is the background color for the fight form for this enemy
    /// </summary>
    public Color Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initPos">this is the initial position of the enemy</param>
        /// <param name="collider">this is the collider for the enemy</param>
        /// 
    public string Name { get; set; }
    public Enemy(Vector2 initPos, Collider collider, string name) : base(initPos, collider) {
      Name = name;
    }

    public override string ToString(){
      return $"{Name}";
    }
  }
}
