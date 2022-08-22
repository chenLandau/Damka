// $G$ SFN-012 (+10) Bonus: Events in the Logic layer are handled by the UI.
// $G$ SFN-012 (+10) Bonus: Implemented user interface with richer graphics / motion.
// $G$ CSS-027 (-8) Unnecessary blank line - relevant for your all solution
using System.Windows.Forms;

namespace UserInterface
{
    internal class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            GameManager manager = new GameManager();
            manager.Run();
        }
    }
}
