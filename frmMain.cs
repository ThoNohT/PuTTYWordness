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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace PuTTYWordness
{
    /// <summary>
    /// The main form of the PuTTYWordness program.
    /// </summary>
    public partial class frmMain : Form
    {
        #region Fields

        /// <summary>
        /// The list of currently used definitions.
        /// </summary>
        private List<CharDef> definitions = new List<CharDef>();

        /// <summary>
        /// The list of classes as they are when read from the current session in the registry.
        /// </summary>
        private List<int> initialClasses = new List<int>();

        /// <summary>
        /// The base path of the registry key that contains all the sessions and their configurations.
        /// </summary>
        private const string BASE_PATH = @"Software\SimonTatham\PuTTY\Sessions";

        /// <summary>
        /// The name of the currently selected session.
        /// </summary>
        private string currentSession = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the main form.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Updates the enabled states of the menu items depending on whether their functionality is available.
        /// </summary>
        private void UpdateMenuStates()
        {
            mnuSave.Enabled = mnuImport.Enabled = mnuExport.Enabled = this.currentSession != null;
        }

        /// <summary>
        /// Loads a session from the registry and fills the grid.
        /// </summary>
        private void lstSessions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Make sure something is selected.
            if (lstSessions.Text == "")
                return;

            this.currentSession = lstSessions.Text;
            var currentKey = string.Format(@"{0}\{1}", BASE_PATH, this.currentSession);

            var presentValues = Registry.CurrentUser.OpenSubKey(currentKey).GetValueNames().ToList();

            for (var i = 0; i < 8; i++)
            {
                var currentValue = string.Format(@"Wordness{0}", i * 32);

                // If a value is missing, we can't work with this.
                if (!presentValues.Contains(currentValue))
                {
                    MessageBox.Show("A registry value is missing, this session cannot be loaded.", "Error", MessageBoxButtons.OK);
                    this.currentSession = null;
                    UpdateMenuStates();
                    return;
                }

                // Get the value from the register, split it up and add it to the initial classes list.
                var val = (string)Registry.CurrentUser.OpenSubKey(currentKey).GetValue(currentValue);
                initialClasses.AddRange(val.Split(',').Select(x => int.Parse(x)).ToList());
            }

            // Fill the data source and update the menus.
            for (var i = 0; i < 256; i++)
                this.definitions.Add(new CharDef(i, (char)i, initialClasses[i]));
            valueGrid.DataSource = definitions;
            UpdateMenuStates();
        }

        /// <summary>
        /// Saves the currently displayed values for the current session in the registry. Note that this can only be called
        /// if a session is selected, therefore there is no check for this.
        /// </summary>
        private void mnuSave_Click(object sender, EventArgs e)
        {
            // Confirm that the user really wants to save.
            if (MessageBox.Show(string.Format(@"Are you sure you want to save session {0}?", this.currentSession), "Save", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            // Iterate over the registry values.
            var currentKey = string.Format(@"{0}\{1}", BASE_PATH, this.currentSession);
            for (var i = 0; i < 8; i++)
            {
                var currentValue = string.Format(@"Wordness{0}", i * 32);

                // Take the subset of definitions that fits into this registry value, convert it to a comma-separated string.
                var value = string.Join(",", this.definitions.Skip(i * 32).Take(32).Select(d => d.Class.ToString()));
                Registry.CurrentUser.OpenSubKey(currentKey, true).SetValue(currentValue, value);
            }

            // There has to be some sort of confirmation that saving is complete.
            MessageBox.Show("Saved");
        }

        /// <summary>
        /// Validates a changed cell. Note that only cells in the third column can be changed, but apparently all other columns get validated as well.
        /// So make sure only the third cell is validated. The check passes if the new value is an integer number between 0 and 9.
        /// </summary>
        private void valueGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Only validate the editable cell, fhs!
            if (e.ColumnIndex != 2) return;

            int newValue;
            if (!int.TryParse(e.FormattedValue.ToString(), out newValue))
                e.Cancel = true;

            if (0 > newValue || 9 < newValue)
                e.Cancel = true;
        }

        /// <summary>
        /// Loads the PuTTY sessions from the registry and populates the list of sessions.
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Check whether the key exists, if not, close.
            var baseKey = Registry.CurrentUser.OpenSubKey(BASE_PATH);
            if (baseKey == null)
            {
                MessageBox.Show("PuTTY registry key not found, is PuTTY installed?\r\nClosing.", @"Error", MessageBoxButtons.OK);
                this.Close();
                return;
            }
            
            // The key exists, enumerate all sessions.
            var sessions = Registry.CurrentUser.OpenSubKey(BASE_PATH).GetSubKeyNames();
            foreach (var session in sessions) lstSessions.Items.Add(session);
        }

        /// <summary>
        /// Exports the current definitions to the file the user chooses.
        /// </summary>
        private void mnuExport_Click(object sender, EventArgs e)
        {
            // Let the user pick the location of the save file.
            var dialog = new SaveFileDialog();
            dialog.Filter = "Wordness XML FIle|*.wxml";
            dialog.Title = "Save the current definitions.";
            dialog.ShowDialog();

            // Make sure a file was chosen.
            if (dialog.FileName == "")
                return;
            
            // Serialize the definitions to the specified file.
            var serializer = new XmlSerializer(typeof(ClassExport));
            var outputStream = new StreamWriter(dialog.FileName);
            serializer.Serialize(
                outputStream,
                new ClassExport
                {
                    Classes = this.definitions.Select(d => d.Class).ToList()
                });
            outputStream.Close();

            MessageBox.Show("Export complete");
        }

        // Imports the definitions from the file the user chooses.
        private void mnuImport_Click(object sender, EventArgs e)
        {
            // Let the user pick the location of the save file.
            var dialog = new OpenFileDialog();
            dialog.Filter = "Wordness XML File|*.wxml";
            dialog.Title = "Load definitions.";
            dialog.ShowDialog();

            // Make sure a file was chosen.
            if (dialog.FileName == "")
                return;

            // Try to deserialize the file.
            var serializer = new XmlSerializer(typeof(ClassExport));
            var inputStream = new StreamReader(dialog.FileName);
            ClassExport result;
            try
            {
                result = (ClassExport)serializer.Deserialize(inputStream);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Unable to load the specified file.", @"Error");
                inputStream.Close();
                return;
            }

            // We need the correct amount of definitions in the export.
            if (result.Classes.Count != 256) {
                MessageBox.Show(@"Incorrect number of definitions in the specified file.", @"Error");
                inputStream.Close();
                return;
            }

            if (result.Classes.Any(c => 0 > c || 9 < c))
            {
                MessageBox.Show("Not all definitions in the specified file are valid.", @"Error");
                inputStream.Close();
                return;
            }

            // Update the list.
            for (var i = 0; i < 256; i++)
            {
                this.definitions[i].Class = result.Classes[i];
            }

            valueGrid.Refresh();

            MessageBox.Show("Import complete");
        }
    }
}
