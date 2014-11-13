using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheKnightTravails
{
    class Tile
    {
        public int Row { get; set;}
        public int Column { get; set; }
        
        public Tile(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public bool Matches(Tile tile)
        {
            return tile.Column == this.Column && tile.Row == this.Row;
        }

        public override String ToString()
        {
            String columnString = StaticTileLocations.GetColumnLetter(Column);
            String rowString = StaticTileLocations.GetRowNumber(Row);

            return columnString + rowString;
        }
    }
}
