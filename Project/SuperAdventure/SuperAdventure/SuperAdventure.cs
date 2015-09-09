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


            Location location = new Location(1, "Home", "This is your home");
            Location locationTwo = new Location(1, "Bedroom", "This is your bedroom", null, null, null);


            _player = new Player(10, 10, 20, 0, 1);

            MoveTo(World.LocationById(World.LocationIdHome));
            //Creating a player adding a item to inventory list rusty sword quantity 1
            _player.Inventory.Add(new InventoryItem(World.ItemById(World.ItemIdRustySword), 1));



            lblHitPoints.Text = _player.CurrentHitPoints.ToString(CultureInfo.CurrentCulture);
            lblGold.Text = _player.Gold.ToString(CultureInfo.InvariantCulture);
            lblExperience.Text = _player.ExperiencePoints.ToString(CultureInfo.InvariantCulture);
            lblLevel.Text = _player.Level.ToString(CultureInfo.InvariantCulture);
        }



        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void MoveTo(Location newLocation)
        {
            //Checking condition of location
            if (newLocation.ItemRequiredToEnterHere != null)
            {
                //See if the player has the required item in their inventory
                bool playerHasItem = false;

                foreach (InventoryItem ii in _player.Inventory)
                {
                    if (ii.Details.ID == newLocation.ItemRequiredToEnterHere.ID)
                    {
                        //We found the required item
                        playerHasItem = true;
                        //Exit out of the foreach loop
                        break;
                    }
                }
                //Didn't find the item required for entry (not in inventory)
                if (!playerHasItem)
                {
                    rtbMessages.Text += string.Format(
                        "You must have a {0} to " +
                        "to enter this location ",
                         newLocation.
                         ItemRequiredToEnterHere.
                         Name);
                }
                //Update the payer's current locaation
                _player.CurrentLocation = newLocation;

                //Show / hide available movement buttons
                btnNorth.Visible = (newLocation.LocationToNorth != null);
                btnEast.Visible = (newLocation.LocationToEast != null);
                btnSouth.Visible = (newLocation.LocationToSouth != null);

                //Display current location name and description
                rtbLocation.Text = newLocation.Name + Environment.NewLine;
                rtbLocation.Text += newLocation.Description + Environment.NewLine;

                //Completly heal the player
                _player.CurrentHitPoints = _player.MaximumHitPoints;

                //Update hit points in UI
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                //Does the location have a quest?
                if (newLocation.QuestAvailableHere != null)
                {
                    //See if the player already has the quest, & if they've completed it
                    bool playerAlreadyHasQuest = false;
                    bool playerAlreadyCompletedQuest = false;

                    foreach (PlayerQuest playerQuest in _player.Quests)
                    {
                        if (playerQuest.Details.ID == newLocation.QuestAvailableHere.ID)
                        {
                            playerAlreadyHasQuest = true;
                            if (playerQuest.IsCompleted)
                            {
                                playerAlreadyCompletedQuest = true;
                            }
                        }
                    }

                    //Check if the player already has the quest
                    if (playerAlreadyHasQuest)
                    {
                        //If player has not completed the quest yet
                        if (playerAlreadyCompletedQuest)
                        {
                            //See if the player has all the items needed to complete the quest
                            bool playerHasAllItemsToCompleteQuest = true;

                            foreach (QuestCompletionItem qCi in newLocation.QuestAvailableHere.QuestCompletionItems)
                            {
                                bool foundItemInPlayersInventory = false;

                                //Check each item in the players inventory, to see if they have it and the quantity
                                foreach (InventoryItem ii in _player.Inventory)
                                {
                                    //The player has this item in their inventory
                                    if (ii.Details.ID == qCi.Details.ID)
                                    {
                                        foundItemInPlayersInventory = true;
                                        if (ii.Quantity < qCi.Quantity)
                                        {
                                            //The player does not have enough of this item 
                                        }
                                    }
                                }
                            }
                        }
                    }
               
                }

            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {

        }
    }
}
