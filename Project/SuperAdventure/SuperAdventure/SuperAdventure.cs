using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
        private Monster _currentMonster;

        public SuperAdventure()
        {
            InitializeComponent();

            _player = new Player(10, 10, 20, 0, 1);

            MoveTo(World.LocationById(World.LocationIdHome));

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();

        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void MoveTo(Location newLocation)
        {
            //Does the location have any required items
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessages.Text += string.Format("You must have  a {0} to enter this location.{1}",
                                                   newLocation.ItemRequiredToEnterHere.Name,
                                                   Environment.NewLine);
                return;
            }

            //Update the player's current location
            _player.CurrentLocation = newLocation;

            //Show/hide available movement buttons
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            //Dispaly current location name description
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text = newLocation.Description + Environment.NewLine;

            //Completly heal the player
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            //Update hit points in the UI
            lblHitPoints.Text = _player.MaximumHitPoints.ToString();

            //Does the location have a quest?
            if (newLocation.QuestAvailableHere != null)
            {
               //Variables to hold if the player already has the quest, and if they've completed it
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                //See if the playeralready has the quest
                if (playerAlreadyHasQuest)
                {
                    //if player has all items needed to complete the quest
                    if (!playerAlreadyCompletedQuest)
                    {
                        //See if the player has all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest =
                            _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);
                        
                        //The player has alll items required to complete the quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            //Display message
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += string.Format("You complete the {0} ' quest.{1}",
                                                               newLocation.QuestAvailableHere.Name,
                                                               Environment.NewLine);
                        
                            //Remove quest items from inventory
                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);
                        
                            //Give quest rewards
                            rtbMessages.Text += string.Format("You recieve: {0}", Environment.NewLine);
                            rtbMessages.Text += string.Format("{0} experience points{1}", newLocation.QuestAvailableHere.RewardExperiencePoints, Environment.NewLine);
                            rtbMessages.Text += string.Format("{0} gold{1}", newLocation.QuestAvailableHere.RewardGold, Environment.NewLine);
                            rtbMessages.Text += string.Format("{0}{1}", newLocation.QuestAvailableHere.RewardItem.Name, Environment.NewLine);
                            rtbMessages.Text += Environment.NewLine;

                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                            //Add the reward item to the players's inventory
                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                            //Mark the quest as completed
                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                        }
                    }
                }
                else
                {
                    //The player does not already have the quest

                    //Display the messages
                    rtbMessages.Text += string.Format("You recieve the {0} quest.{1}",
                                                       newLocation.QuestAvailableHere,
                                                       Environment.NewLine);
                    rtbMessages.Text += string.Format(newLocation.QuestAvailableHere.Description + Environment.NewLine);
                    rtbMessages.Text += string.Format("To complete it, return with:{0}", Environment.NewLine);
                    foreach (QuestCompletionItem qCi in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qCi.Quantity == 1)
                        {
                            rtbMessages.Text += string.Format("{0} {1} {2}",
                                qCi.Quantity,
                                qCi.Details.Name,
                                Environment.NewLine);
                        }
                        else
                        {
                            rtbMessages.Text += string.Format("{0} {1} {2}",
                                qCi.Quantity,
                                qCi.Details.NamePlural,
                                Environment.NewLine);
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;

                        //Add the quest to the player's quest list
                        _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere, true));
                }
            }

                //Does the location have a monster?
                if (newLocation.MonsterLivingHere != null)
                {
                    rtbMessages.Text += string.Format("You see a {0} {1}", 
                                                       newLocation.MonsterLivingHere.Name,
                                                       Environment.NewLine);

                    //Make a new Monster, using the new values from the standard monster in the World.Monster list
                    Monster standardMonster = World.MonsterById(newLocation.MonsterLivingHere.ID);

                    _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage, standardMonster.RewardExperiencePoints,
                                                  standardMonster.RewardExperiencePoints, standardMonster.CurrentHitPoints, standardMonster.MaximumHitPoints);
                
                    foreach (LootItem lootItem in standardMonster.LootTable)
                    {
                        _currentMonster.LootTable.Add(lootItem);
                    }

                    cboWeapons.Visible = true;
                    cboPotions.Visible = true;
                    btnUsePotion.Visible = true;
                    btnUsePotion.Visible = true;
                }
                else
                {
                    _currentMonster = null;

                    cboWeapons.Visible = false;
                    cboPotions.Visible = false;
                    btnUsePotion.Visible = false;
                    btnUsePotion.Visible = false;
                }
                
                //Refresh player's inventory list
                UpdateInventoryListInUI();

                //Refresh players quest list
                UpdateQuestListInUI();

                //Refresh player's weapons combobox
                UpdateWeaponListInUI();

                //Refresh player's potions combobox
                UpdatePotionListInUI();
        }

        private void UpdateInventoryListInUI()
        {
            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantiy";

            dgvInventory.Rows.Clear();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.RowHeadersVisible = false;

                    dgvQuests.ColumnCount = 2;
                    dgvQuests.Columns[0].Name = "Name";
                    dgvQuests.Columns[0].Width = 197;
                    dgvQuests.Columns[1].Name = "Done?";

                    dgvQuests.Rows.Clear();

                    foreach (PlayerQuest playerQuest in _player.Quests)
                    {
                        dgvQuests.Rows.Add(new[] {playerQuest.Details.Name, playerQuest.IsCompleted.ToString()});
                    }
                }
            }
        }

        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }

        }
        
        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon) inventoryItem.Details);
                    }
                }
            }
            if (weapons.Count == 0)
            {
                //The player doesn't have any weapons, so hide the weapons combobox and 'Use' button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListInUI()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPotion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add((HealingPotion)inventoryItem.Details);
                    }
                }
            }
            if (healingPotions.Count == 0)
            {
                //The player doesn't have any potions, so hide the potion combobox and 'Use' button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            //get the currently selected weapon from cboWeapons ComboBox
            Weapon currentWeapon = (Weapon) cboWeapons.SelectedItem;

            //Determine the amount of damage to do to a monster
            int damageToMonster = NumberGeneratorSimple.NumberBetween(currentWeapon.MaximumDamage, currentWeapon.MaximumDamage);

            //Apply the damage to the monster's currentHitPoints
            _currentMonster.CurrentHitPoints -= damageToMonster;

            //Display message\
            rtbMessages.Text += string.Format("You hit the {0} for {1} points. {2}",
                                               _currentMonster.Name,
                                               damageToMonster,
                                               Environment.NewLine);

            //Check if the monster is dead
            if (_currentMonster.CurrentHitPoints <= 0)
            {
                //Monster is dead
                rtbMessages.Text += string.Format("{0}", Environment.NewLine);
                rtbMessages.Text += string.Format("You defeated the {0} {1}",
                                                   _currentMonster.Name,
                                                   Environment.NewLine);

                //Give the player experience points for killing the monster
                _player.ExperiencePoints += _currentMonster.RewardExperiencePoints;
                rtbMessages.Text += string.Format("You recieve {0} experience points {1}",
                                                   _currentMonster.RewardExperiencePoints,
                                                   Environment.NewLine);

                //Give player gold for killing the monster
                _player.Gold += _currentMonster.RewardGold;
                rtbMessages.Text += string.Format("You recieve {0} gold {1}",
                                                   _currentMonster.RewardGold,
                                                   Environment.NewLine);

                //get random loot items from the monster
                List<InventoryItem> lootedItems = new List<InventoryItem>();

                //Add items to the lootedItems list, comparing a random number to the drop percentage
                foreach (LootItem lootItem in _currentMonster.LootTable)
                {
                    if (NumberGeneratorSimple.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                //if no items were randomly selected, then add the defalult loot item(s).
                if (lootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in _currentMonster.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details,1));
                        }
                    }
                }
                //Add the looted items to the player's inventory
                foreach (InventoryItem inventoryItem in lootedItems)
                {
                   _player.AddItemToInventory(inventoryItem.Details);
                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += string.Format("You loot {0} {1} {2}",
                                                           inventoryItem.Quantity,
                                                           inventoryItem.Details.Name,
                                                           Environment.NewLine);
                    }
                    else
                    {
                        rtbMessages.Text += string.Format("You loot {0} {1} {2}",
                                                           inventoryItem.Quantity,
                                                           inventoryItem.Details.Name,
                                                           Environment.NewLine);
                    }
                }

                //Refresh player information and inventory controls
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                lblGold.Text = _player.Gold.ToString();
                lblExperience.Text = _player.ExperiencePoints.ToString();
                lblLevel.Text = _player.Level.ToString();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();

                //Add a blank line to the messafw box for apperance purposes
                rtbMessages.Text = Environment.NewLine;

                //Move player to current location (to heal player and create a new monster to fight)
                MoveTo(_player.CurrentLocation);
            }
            else
            {
                //Monster is still alive

                //Determine the amount of damage the monser does to the player
                int damageToPlayer = NumberGeneratorSimple.NumberBetween(0, _currentMonster.MaximumDamage);

                //Display message
                rtbMessages.Text = string.Format("The {0} did {1} points of damage.{2}",
                                                  _currentMonster.Name,
                                                  damageToPlayer,
                                                  Environment.NewLine);
                
                //Subtract damage from the player
                _player.CurrentHitPoints -= damageToPlayer;

                //Refresh player data in the UI
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                if (_player.CurrentHitPoints <= 0)
                {
                    //Display message
                    rtbMessages.Text += string.Format("The {0} killed you {1}",
                                                       _currentMonster.Name,
                                                       Environment.NewLine);

                    //Move player to "Home" location
                    MoveTo(World.LocationById(World.LocationIdHome));
                }
            }
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
        }

    }

}
   

    #region Pre Refactor Code 
/*
    public partial class SuperAdventure : Form
    {
        private Player _player;
        private Monster _currentMonster;

        public SuperAdventure()
        {
            InitializeComponent();

            //Creating a player adding a item to inventory list rusty sword quantity 1
            _player = new Player(10, 10, 20, 0, 1);
            MoveTo(World.LocationById(World.LocationIdHome));
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
                //See if the player HAS the required item in their inventory
                bool playerHasRequiredItem = false;

                foreach (InventoryItem ii in _player.Inventory)
                {
                    if (ii.Details.ID == newLocation.ItemRequiredToEnterHere.ID)
                    {
                        //We found the required item
                        playerHasRequiredItem = true;
                        //Exit out of the foreach loop
                        break;
                    }
                }

                //Did NOT find the item required for entry (not in inventory)
                if (!playerHasRequiredItem)
                {
                    //Did NOT find required item in the players inventory
                    //Display the message stop attempting player movement
                    rtbMessages.Text += string.Format("You must have a {0} to " +
                                                      "to enter this location ",
                            newLocation.
                            ItemRequiredToEnterHere.
                            Name);
                    return;
                }
            }
            //Update the payer's current locaation
            _player.CurrentLocation = newLocation;

            //Show / hide available movement buttons
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToSouth != null);

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
                //See if the player already HAS the quest, & if they've completed it
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

                //Check if the player already HAS the quest
                if (playerAlreadyHasQuest)
                {
                    //If player has NOT completed the quest yet
                    if (playerAlreadyCompletedQuest)
                    {
                        //See if the player HAS all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest = true;

                        foreach (QuestCompletionItem qCi in newLocation.QuestAvailableHere.QuestCompletionItems)
                        {
                            bool foundItemInPlayersInventory = false;

                            //Check each item in the players inventory, to see if they have it and the quantity
                            foreach (InventoryItem ii in _player.Inventory)
                            {
                                //The player HAS this item in their inventory
                                if (ii.Details.ID == qCi.Details.ID)
                                {
                                    foundItemInPlayersInventory = true;
                                    if (ii.Quantity < qCi.Quantity)
                                    {
                                        //The player does NOT have enough of this item 
                                        playerHasAllItemsToCompleteQuest = false;

                                        //No reason to continue checking for the other quest completion Items
                                        break;
                                    }
                                    //Quest completion item found no need to continue checking rest of inventory
                                    break;
                                }
                            }

                            //If we did NOT find the required item, set the variable & stop looking for other items
                            if (!foundItemInPlayersInventory)
                            {
                                //The player does not have this item in their inventory
                                playerHasAllItemsToCompleteQuest = false;

                                //No reason to continue checking for the other quest completion items
                                break;
                            }
                        }


                        //The player HAS all items required to complete the quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            //Display message
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += string.Format("You complete the '{0}' quest. {1}",
                                newLocation.QuestAvailableHere.Name,
                                Environment.NewLine);

                            //Remove the items from inventory
                            foreach (QuestCompletionItem qCi in newLocation.QuestAvailableHere.QuestCompletionItems)
                            {
                                foreach (InventoryItem ii in _player.Inventory)
                                {
                                    if (ii.Details.ID == qCi.Details.ID)
                                    {
                                        //Subtract the quantiyy from the player's inventory equal
                                        //to the amount needed to complete the Quest
                                        ii.Quantity -= qCi.Quantity;
                                        break;
                                    }
                                }
                            }

                            //Issue quest rewards
                            rtbMessages.Text += string.Format("You recieve: {0}", Environment.NewLine);
                            rtbMessages.Text +=
                                newLocation.QuestAvailableHere.RewardExperiencePoints.ToString(
                                    string.Format(" experience points {0}",
                                        Environment.NewLine));
                            rtbMessages.Text +=
                                newLocation.QuestAvailableHere.RewardGold.ToString(string.Format(" gold {0}",
                                    Environment.NewLine));
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Gold += newLocation.QuestAvailableHere.RewardExperiencePoints;

                            //Add the reward item to the player's inventory
                            bool addedItemToPlayerInventory = false;

                            foreach (InventoryItem ii in _player.Inventory)
                            {
                                if (ii.Details.ID == newLocation.QuestAvailableHere.RewardItem.ID)
                                {
                                    //The player HAS the necessary item in inventory so increment quantity by one
                                    ii.Quantity++;

                                    addedItemToPlayerInventory = true;
                                    break;
                                }
                            }

                            //Player did NOT have the item in inventory
                            if (!addedItemToPlayerInventory)
                            {
                                _player.Inventory.Add(new InventoryItem(newLocation.QuestAvailableHere.RewardItem, 1));
                            }

                            //Mark the quest as completed
                            //Find the quest int the players quest list
                            foreach (PlayerQuest pQ in _player.Quests)
                            {
                                if (pQ.Details.ID == newLocation.QuestAvailableHere.ID)
                                {
                                    //Mark quest completed
                                    pQ.IsCompleted = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //The player does NOT already have the quest 
                    //Display the messeges
                    rtbMessages.Text += string.Format("You recieve the {0} quest.{1}",
                        newLocation.QuestAvailableHere.Name,
                        Environment.NewLine);
                    rtbMessages.Text += string.Format("{0} {1}",
                        newLocation.QuestAvailableHere.Description,
                        Environment.NewLine);
                    rtbMessages.Text += string.Format("To complete it, return with:{0}",
                        Environment.NewLine);
                    foreach (QuestCompletionItem qCi in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qCi.Quantity == 1)
                        {
                            rtbMessages.Text += qCi.Quantity.ToString(string.Format(" {0} {1}",
                                qCi.Details.Name,
                                Environment.NewLine));
                        }
                        else
                        {
                            rtbMessages.Text += qCi.Quantity.ToString(string.Format(" {0} {1}",
                                qCi.Details.Name,
                                Environment.NewLine));
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;

                    //Add quest to the player's quest list
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere, false));
                }
            }

            //Dos the location have a monster living there?
            if (newLocation.MonsterLivingHere != null)
            {
                rtbMessages.Text += string.Format("You see a {0} {1}",
                    newLocation.MonsterLivingHere,
                    Environment.NewLine);
                //Make a new monster, using values from the standard moster int the World.Moster list
                Monster standardMonster = World.MonsterById(newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage,
                    standardMonster.RewardExperiencePoints, standardMonster.RewardGold, standardMonster.CurrentHitPoints,
                    standardMonster.MaximumHitPoints);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _currentMonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;
            }

            //Refresh player's inventory list
            dgvInventory.Rows.Clear();

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }
            }

            //Refresh player's quest list

            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }

            //Refresh player's weapons combobox
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }
            if (weapons.Count == 0)
            {
                //The player doesnt have any waapons...
                //Hide the weapon combo box and 'Use' button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }

            //Refresh player's potions sombobox
            List<HealingPotion> healingPotions = new List<HealingPotion>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPotion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add((HealingPotion)inventoryItem.Details);
                    }
                }
            }
            if (healingPotions.Count == 0)
            {
                //Player does NOT have any potions, 
                //Hide the potion combobox and 'Use' button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }

        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    } 
   
}
 */
    #endregion



