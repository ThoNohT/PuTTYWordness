/********************************************************************************
 Copyright (C) 2014 Eric Bataille <e.c.p.bataille@gmail.com>

 This program is free software; you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation; either version 2 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program; if not, write to the Free Software
 Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307, USA.
********************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuTTYWordness
{
    /// <summary>
    /// A character definition. This class is used to populate the valueGrid.
    /// </summary>
    class CharDef
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CharDef"/>.
        /// </summary>
        /// <param name="id">The identifier of the definition.</param>
        /// <param name="character">The character this definition is for.</param>
        /// <param name="wordClass">The class that is assigned to this character.</param>
        public CharDef(int id, char character, int wordClass) {
            this.Id = id;
            this.Character = character;
            this.Class = wordClass;
        }

        /// <summary>
        /// Gets the identifier of the definition. This property is not editable.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the character this definition is for. This property is not editable.
        /// </summary>
        public char Character { get; private set; }

        /// <summary>
        /// Gets the class that is assigned to this character. This property is editable.
        /// </summary>
        public int Class { get; set; }
    }
}
