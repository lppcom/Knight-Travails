using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnightTravails
{
    // Provide a lookup for getting a tile's array index notation in a readable format
    static class StaticTileLocations
    {
        static Dictionary<int, string> rowNumbers = new Dictionary<int,string>()
        {
            { 0, "1"},
            { 1, "2"},
            { 2, "3"},
            { 3, "4"},
            { 4, "5"},
            { 5, "6"},
            { 6, "7"},
            { 7, "8"},
        };    
                
        static Dictionary<int, string> columnLetters = new Dictionary<int,string>()
        {
            { 0, "A"},
            { 1, "B"},
            { 2, "C"},
            { 3, "D"},
            { 4, "E"},
            { 5, "F"},
            { 6, "G"},
            { 7, "H"},
        };

      public static String GetRowNumber(int key)
      {
          String number;
          if (rowNumbers.TryGetValue(key, out number))
          {
              return number;
          }
          else
          {
              return null;
          }
      }

      public static String GetColumnLetter(int key)
      {
          String letter;
          if (columnLetters.TryGetValue(key, out letter))
          {
              return letter;
          }
          else
          {
              return null;
          }
      }
    }
}
