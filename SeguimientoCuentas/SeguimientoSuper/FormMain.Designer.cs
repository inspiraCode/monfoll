﻿namespace SeguimientoSuper
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.catalogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cobradoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descargarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catalogsToolStripMenuItem,
            this.cuentasToolStripMenuItem,
            this.toolStripMenuItemConfig});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(744, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // catalogsToolStripMenuItem
            // 
            this.catalogsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.cobradoresToolStripMenuItem});
            this.catalogsToolStripMenuItem.Name = "catalogsToolStripMenuItem";
            this.catalogsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.catalogsToolStripMenuItem.Text = "&Catálogos";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.clientesToolStripMenuItem.Text = "Cli&entes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // cobradoresToolStripMenuItem
            // 
            this.cobradoresToolStripMenuItem.Name = "cobradoresToolStripMenuItem";
            this.cobradoresToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.cobradoresToolStripMenuItem.Text = "C&obradores";
            this.cobradoresToolStripMenuItem.Click += new System.EventHandler(this.cobradoresToolStripMenuItem_Click);
            // 
            // cuentasToolStripMenuItem
            // 
            this.cuentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargarToolStripMenuItem,
            this.processToolStripMenuItem});
            this.cuentasToolStripMenuItem.Name = "cuentasToolStripMenuItem";
            this.cuentasToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.cuentasToolStripMenuItem.Text = "C&uentas";
            // 
            // descargarToolStripMenuItem
            // 
            this.descargarToolStripMenuItem.Name = "descargarToolStripMenuItem";
            this.descargarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.descargarToolStripMenuItem.Text = "&Descargar";
            this.descargarToolStripMenuItem.Click += new System.EventHandler(this.descargarToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.processToolStripMenuItem.Text = "&Proceso";
            this.processToolStripMenuItem.Click += new System.EventHandler(this.processToolStripMenuItem_Click);
            // 
            // toolStripMenuItemConfig
            // 
            this.toolStripMenuItemConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionToolStripMenuItem,
            this.toolStripSeparator2,
            this.acercaDeToolStripMenuItem});
            this.toolStripMenuItemConfig.Name = "toolStripMenuItemConfig";
            this.toolStripMenuItemConfig.Size = new System.Drawing.Size(90, 20);
            this.toolStripMenuItemConfig.Text = "&Herramientas";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.configuracionToolStripMenuItem.Text = "Configuración";
            this.configuracionToolStripMenuItem.Click += new System.EventHandler(this.configuracionToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de ...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatus,
            this.mainProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 357);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(744, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainStatus
            // 
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Size = new System.Drawing.Size(348, 17);
            this.mainStatus.Text = "Seguimiento a cuentas por cobrar - módulo de supervisor V 1.2.1";
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(100, 16);
            this.mainProgressBar.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(744, 379);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seguimiento Supervisor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem catalogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cobradoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descargarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripProgressBar mainProgressBar;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel mainStatus;
    }
}
