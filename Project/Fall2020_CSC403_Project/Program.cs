using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public static class Program {

        public static FrmLevel FrmLevelInstance { get; private set; }
        public static FrmLevel2 FrmLevel2Instance { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          // Created to have the ability to access FrmLevel attributes in other files
          FrmLevelInstance = new FrmLevel();
          Application.Run(FrmLevelInstance);
        }
  }
}
