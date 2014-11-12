using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheKnightTravails
{
    class Tile
    {
        private int row;
        private int column;
        
        public Tile(int column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public override String ToString()
        {
            String columnString = StaticTileLocations.GetColumnLetter(this.column);
            String rowString = StaticTileLocations.GetRowNumber(this.row);

            return columnString + rowString;
        }
    }
}
