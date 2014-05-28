﻿using CasseBrique.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CasseBrique.Views
{
    public partial class LevelCreation : Form
    {
        public int BrickHeight { get; set; }
        public int BrickWidth { get; set; }
        public bool IsAddingBricks { get; set; }
        public int NbMaxBricks { get; set; }
        public bool NoToolSelected { get; set; }
        public bool AddBonusSelected { get; set; }
        string LevelName { get; set; }

        public TextBox ErrorConsole { get; set; }

        public Control currentBrick { get; set; }
        public LevelCreation()
        {
            
            AddBonusSelected = false;
            NoToolSelected = true;
            NbMaxBricks = 12;
            BrickHeight = 34;
            BrickWidth = 67;
            IsAddingBricks = false;
            InitializeComponent();
            ErrorConsole = this.txtErrorMessage;
            this.pnlNbBricks.Text = String.Format("Nombre de Briques restantes : {0}", NbMaxBricks);
        }
        public void decNbBricks()
        {
            NbMaxBricks--;
            this.pnlNbBricks.Text = String.Format("Nombre de Briques restantes : {0}",NbMaxBricks);
        }
        public void incNbBricks()
        {
            NbMaxBricks++;
            this.pnlNbBricks.Text = String.Format("Nombre de Briques restantes : {0}", NbMaxBricks);
        }
        public void validate()
        {
            CustomLevel lvl = new CustomLevel(LevelName, null);
            Console.WriteLine(lvl.Path);
            if (!File.Exists(lvl.Path))
            {
                lvl.write();
                this.Close();
            }
            else
            {
                ErrorConsole.AppendText("Nom de niveau déjà pris.\r\n");
                
            }
        }

        public void addBrickToolAction()
        {
            NoToolSelected = false;
            IsAddingBricks = true;

            currentBrick = new MovableBrick(this, BrickWidth, BrickHeight);
            currentBrick.BackColor = Color.FromArgb(51, 51, 51);


            pnl3Map.Controls.Add(currentBrick);
        }
        public void addBonusToolAction()
        {
            AddBonusSelected = true;
        }

        public void disableAllTools()
        {
            pnl3Map.Controls.Remove(currentBrick);
            AddBonusSelected = false;
            NoToolSelected = false;
            IsAddingBricks = false;
        }
        public void deleteBrickToolAction()
        {
            
            IsAddingBricks = false;
            NoToolSelected = true;
        }
        private void testCreationLevel_Load(object sender, EventArgs e)
        {

        }

        private void pnl4Player_Click(object sender, EventArgs e)
        {
            disableAllTools();

            addBrickToolAction();
        }

        private void pnl3Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsAddingBricks)
            {
                MovableBrick c=null;
                foreach (Control control in pnl3Map.Controls)
                {
                    if (control is MovableBrick)
                    {
                        c =(MovableBrick) control;
                    }
                }
               

                c.BrickMove(MousePosition.X, MousePosition.Y);
                pnl3Map.Refresh();
            }
            
        }

        private void pnlNoTool_MouseClick(object sender, MouseEventArgs e)
        {

            disableAllTools();
            deleteBrickToolAction();
        }

        private void txtLevelName_TextChanged(object sender, EventArgs e)
        {

            if (txtLevelName.Text.Count() < 10)
            {
                LevelName = txtLevelName.Text;
                pnlLvlName.Text = txtLevelName.Text;
            }
            
        }

        private void pnlValidate_MouseClick(object sender, MouseEventArgs e)
        {
            validate();
            
        }

        

        
        private void lblBricks_Click(object sender, EventArgs e)
        {
            disableAllTools();
            addBrickToolAction();
        }

        private void lblDelete_Click(object sender, EventArgs e)
        {
            disableAllTools();
            deleteBrickToolAction();
        }

        private void lblBonus_Click(object sender, EventArgs e)
        {
            disableAllTools();
            addBonusToolAction();
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            disableAllTools();
            addBonusToolAction();
        }

        private void lblValidate_Click(object sender, EventArgs e)
        {
            validate();
        }

       

       

      

      

        

    }







    public class MovableBrick : Panel
    {
        public LevelCreation ParentForm { get; set; }
        public MovableBrick(LevelCreation f, int width, int height)
            : base()
        {
            this.Size = new Size(width, height);
            ParentForm = f;
        }
        public void BrickMove(int X,int Y)
        {
            this.SetBounds(X - this.ParentForm.Bounds.X - this.Parent.Bounds.X - this.Width / 2, Y - this.ParentForm.Bounds.Y - this.Parent.Bounds.Y - this.Height , this.Width, this.Height);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            BrickMove(MousePosition.X , MousePosition.Y );
            this.Parent.Refresh();
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ControlCollection controls = this.Parent.Controls;
            int x = (int)Math.Floor((double)Bounds.X / Width) * Width;
            int y = (int)Math.Floor((double)Bounds.Y / Height) * Height;
            bool canBeAdded = true;
            foreach (Control control in controls)
            {
                if (control.Bounds.X == x && control.Bounds.Y == y)
                {
                    canBeAdded = false;
                }
            }
            if (canBeAdded && ParentForm.NbMaxBricks>0)
            {
                this.Parent.Controls.Add(new StaticBrick(ParentForm, Width, Height, x, y));
                ParentForm.decNbBricks();
            }
            else if(!canBeAdded)
            {

                ParentForm.ErrorConsole.AppendText("Brique déjà existante.\r\n");
            
            }
            else if(ParentForm.NbMaxBricks<=0){
                ParentForm.ErrorConsole.AppendText("Plus de briques disponibles.\r\n");
            }
            
            
            
        }
    }


    public class StaticBrick : Panel
    {
        public LevelCreation ParentForm { get; set; }
        public bool HasBonus { get; set; }
        public StaticBrick(LevelCreation f, int width, int height, int x, int y)
            : base()
        {
            HasBonus = false;
            this.BackColor = Color.FromArgb(51, 51, 51);
            this.SetBounds(x, y, width, height);
            ParentForm = f;
            this.Refresh();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (ParentForm.NoToolSelected)
            {
                Parent.Controls.Remove(this);
                ParentForm.incNbBricks();
            }
            else
            {
                if (ParentForm.AddBonusSelected)
                {
                    setBonus(true);
                }
                
            }
        }

        public void setBonus(bool bonus) {
            if (bonus)
            {
                this.BackColor = Color.FromArgb(80, 150, 1);
                HasBonus = true;
            }
            else
            {
                this.BackColor = Color.FromArgb(51, 51, 51);
            }
        }

    }
}