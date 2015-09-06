using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;

namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private Player _player;
        public SuperAdventure()
        {
            InitializeComponent();

            
            Location location = new Location(1, "Home","This is your home");
            Location locationTwo = new Location(1,"Bedroom", "This is your bedroom", null, null, null);
        
            
            _player = new Player(10,10,20,0,1);
            
            
            lblHitPoints.Text = _player.CurrentHitPoints.ToString(CultureInfo.CurrentCulture);
            lblGold.Text = _player.Gold.ToString(CultureInfo.InvariantCulture);
            lblExperience.Text = _player.ExperiencePoints.ToString(CultureInfo.InvariantCulture);
            lblLevel.Text = _player.Level.ToString(CultureInfo.InvariantCulture);
        }
    }
}
