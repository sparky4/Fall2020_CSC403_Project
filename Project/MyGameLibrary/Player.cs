﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code {
  public class Player : BattleCharacter {
        public Inventory inventory;
    public Player(Vector2 initPos, Collider collider) : base(initPos, collider) {
        this.inventory = new Inventory();
    }
  }
}
