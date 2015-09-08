using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    //Public is the accessability modifier other classes can access this class
   public class WorkBench 
    {
       //Class-level variables
       public int classLevelVariabelOne = 3; //Visable to other classes
       private int classLevelVariableTwo = 7; //Only visable to this class (WorkBench)
       private int classLevelVariableThree; //Declaring the variable, without setting a value

       public void Function()
       {
           int functionLevelVariableOne; //limited in scope to this function / method only

           if (classLevelVariabelOne < 5)
           {
               functionLevelVariableOne = 1;
               int innerVariable;
           }

           if (classLevelVariableTwo >= 5)
           {
               functionLevelVariableOne = 3;
               int innerVariable;
           }
       }


       public void Function2()
       {

           int functionLevelVariableOne;  //Limited in scope, can have the same name as the variable on line 18 w/o issue

           if (classLevelVariableTwo < 5) 
           {
               functionLevelVariableOne = 3; 
               int innerVariable;
           }
           if (classLevelVariableTwo >= 5)
           {
               functionLevelVariableOne = 4;
               int innerVariableOne;
           }
       }
    }
}
