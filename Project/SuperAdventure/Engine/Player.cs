using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
   public class Player : LivingCreature
    {
        public int Gold { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        public Location CurrentLocation { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        

        public Player(int currentHitPoints,
                      int maximumDamage,
                      int gold,
                      int experiencePoints,
                      int level) : base (currentHitPoints,maximumDamage)
       {
           Gold = gold;
           ExperiencePoints = experiencePoints;
           Level = level;

           Inventory = new List<InventoryItem>();
           Quests = new List<PlayerQuest>();
       }

       public bool HasRequiredItemToEnterThisLocation(Location location)
       {
           if (location.ItemRequiredToEnterHere == null)
           {
               //There is no item required to enter this location, so return "true"
               return true;
           }

           //see if the player has the required item in their inventory
           foreach (InventoryItem ii in Inventory)
           {
               if (ii.Details.ID == location.ItemRequiredToEnterHere.ID)
               {
                   //We found the required item, so return "true"
                   return true;
               }
               
           }
           //We didn't find the required item in their inventory, so return "false" 
           return false;
       }

       public bool HasThisQuest(Quest quest)
       {
           //See if the player has all the items needed to complete the quest 
           foreach (PlayerQuest playerQuest in Quests)
           {
               if(playerQuest.Details.ID == quest.ID)
               {
                   return true;
               }
           }
           return false;
       }

       public bool CompletedThisQuest(Quest quest)
       {
           foreach (PlayerQuest playerQuest in Quests)
           {
               if (playerQuest.Details.ID == quest.ID)
               {
                   return playerQuest.IsCompleted;
               }
           }
           return false;
       }

       public bool HasAllQuestCompletionItems(Quest quest)
       {
           //See if the player has all the items needed to complete the quest
           foreach (QuestCompletionItem qCi in quest.QuestCompletionItems)
           {
               bool foundItemInPlayersInventory = false;

               //Check each item in the players inventory, to see if they have it (quantity)
               foreach (InventoryItem ii in Inventory)
               {
                   //Player has the item in their inventory
                   if (ii.Details.ID == qCi.Details.ID)
                   {
                       foundItemInPlayersInventory = true;
                       //Player does NOT have enough of the Item
                       if (ii.Quantity < qCi.Quantity)
                       {
                           return false;
                       }
                   }
                   
               }
               //The player does not have any of this quest completion item in their inventory
               if (!foundItemInPlayersInventory)
               {
                   return false;
                   
               }

           }

           //If we get here the player must have all the required items & the correct quantity
           return true;
       }

       public void RemoveQuestCompletionItems(Quest quest)
       {
           foreach(QuestCompletionItem qCi in quest.QuestCompletionItems)
           {
               foreach (InventoryItem ii in Inventory)
               {
                   if(ii.Details.ID == qCi.Details.ID)
                   {
                       //Subtract the quantity from the player's inventory that was needded to complete the quest
                       ii.Quantity -= qCi.Quantity;
                       break;
                   }
               }
           }
       }

       public void AddItemToInventory(Item itemToAdd)
       {
           foreach (InventoryItem ii in Inventory)
           {
               if (ii.Details.ID == itemToAdd.ID)
               {
                   //They have the item in their inventory so increase the quantity by one
                   ii.Quantity++;

                   return;  //Added the item marked it complete exit function
               }
           }
           //They didn't have the item so add it to their inventory (quantity of 1)
           Inventory.Add(new InventoryItem(itemToAdd, 1));
       }

       public void MarkQuestCompleted(Quest quest)
       {
           foreach (PlayerQuest pQ in Quests)
           {
               if (pQ.Details.ID == quest.ID)
               {
                   //Mark it as completed
                   pQ.IsCompleted = true;

                   return; //Found quest marked it complete exit function
               }
           }
       }
    }
}
