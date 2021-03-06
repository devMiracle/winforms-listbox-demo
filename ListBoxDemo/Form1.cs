﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListBoxDemo
{
    public partial class Form1 : Form
    {
        // private List<string> strings;
        // private BindingList<string> strings;
        private BindingList<dynamic> users;
        private ProgressBar progress;
        public Form1()
        {
            InitializeComponent();
            /* strings = new BindingList<string> {
                "Hello",
                "WinForms"
            }; */
            users = new BindingList<dynamic> {
                new { Id = 1, Name = "Name1", Pasword = "P1", Created_at = DateTime.Now },
                new { Id = 2, Name = "Name2", Pasword = "P2", Created_at = DateTime.Now }
            };
            // demoListBox.Items.AddRange(strings.ToArray());
            demoListBox.DataSource = users;
            demoListBox.DisplayMember = "Name";
            demoListBox.ValueMember = "Id";
            /* demoListBox.SelectionMode = SelectionMode.One;
            demoListBox.SelectedIndexChanged += (s, a) => {
                Console.WriteLine(demoListBox.SelectedItem);
            };*/

            demoListBox.SelectionMode = SelectionMode.MultiExtended;
            /*demoListBox.MultiColumn = true;
            demoListBox.Items.AddRange(new object[] {
            "Item 1, column 1, column 3",
            "Item 2, column 1, column 3",
            "Item 3, column 1, column 3",
            "Item 4, column 1, column 3",
            "Item 5, column 1, column 3",
            "Item 1, column 2, column 3",
            "Item 2, column 2, column 3",
            "Item 3, column 2"});*/

            comboBox1.DataSource = users;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //strings.Add(stringTextBox.Text);
            users.Add(stringTextBox.Text);
            // demoListBox.Items.Add(stringTextBox.Text);
        }

        private void deleteItemsButton_Click(object sender, EventArgs e)
        {
            // v1
            List<dynamic> toRemove = new List<dynamic>();
            if (demoListBox.SelectedItems.Count > 0)
            {
                showProgress(demoListBox.SelectedItems.Count);
            }
            foreach (var item in demoListBox.SelectedItems)
            {
                toRemove.Add(item);
            }
            toRemove.ForEach((toRemoveItem) => {
                Thread.Sleep(1000);
                users.Remove(toRemoveItem);
                increaseProgress();
            });
            goneProgress();
            // v2
            /*users =
                new BindingList<object>(
                    users.Except(
                        new List<object>(
                            demoListBox.SelectedItems.Cast<object>().ToArray()
                            )
                        ).ToList()
                    );
            demoListBox.DataSource = users;*/
        }

        private void showProgress(int max) {
            if (progress == null)
            {
                progress = new ProgressBar();
                this.progress.Name = "progress";
                progress.Maximum = max;
                progress.Step = 1;
                progress.Top = this.Height / 2 - progress.Height / 2;
                progress.Left = this.Width / 2 - progress.Width / 2;
            }
            
            this.Controls.Add(progress);
        }

        private void increaseProgress() {
            progress.PerformStep();
            //progress.Update();
        }

        private void goneProgress() {
            this.Controls.Remove(progress);
            progress.Value = 0;
        }
    }
}
