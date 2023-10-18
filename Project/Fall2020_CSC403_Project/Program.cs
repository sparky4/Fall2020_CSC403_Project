using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  static class Program {

        public static FrmLevel FrmLevelInstance { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          FrmLevelInstance = new FrmLevel();
          Application.Run(FrmLevelInstance);
        }
  }
}
