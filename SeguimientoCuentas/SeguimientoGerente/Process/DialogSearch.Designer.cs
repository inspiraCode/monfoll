﻿namespace SeguimientoGerente.Process
{
    partial class DialogSearch
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSerie = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxClientName = new System.Windows.Forms.ComboBox();
            this.comboBoxFolios = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cliente:";
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxClient.Location = new System.Drawing.Point(74, 19);
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(79, 21);
            this.comboBoxClient.TabIndex = 1;
            this.comboBoxClient.SelectionChangeCommitted += new System.EventHandler(this.comboBoxClient_SelectionChangeCommitted);
            this.comboBoxClient.Enter += new System.EventHandler(this.comboBoxClient_Enter);
            this.comboBoxClient.Leave += new System.EventHandler(this.comboBoxClient_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Serie:";
            // 
            // comboBoxSerie
            // 
            this.comboBoxSerie.Location = new System.Drawing.Point(69, 52);
            this.comboBoxSerie.Name = "comboBoxSerie";
            this.comboBoxSerie.Size = new System.Drawing.Size(97, 21);
            this.comboBoxSerie.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Folio:";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(74, 89);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(130, 44);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "Buscar";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(210, 89);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(127, 44);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancelar Búsqueda";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxClientName
            // 
            this.comboBoxClientName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxClientName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxClientName.Location = new System.Drawing.Point(159, 19);
            this.comboBoxClientName.Name = "comboBoxClientName";
            this.comboBoxClientName.Size = new System.Drawing.Size(288, 21);
            this.comboBoxClientName.TabIndex = 2;
            this.comboBoxClientName.SelectionChangeCommitted += new System.EventHandler(this.textBoxClient_TextChanged);
            // 
            // comboBoxFolios
            // 
            this.comboBoxFolios.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxFolios.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxFolios.Location = new System.Drawing.Point(210, 52);
            this.comboBoxFolios.Name = "comboBoxFolios";
            this.comboBoxFolios.Size = new System.Drawing.Size(127, 21);
            this.comboBoxFolios.TabIndex = 6;
            // 
            // DialogSearch
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(460, 156);
            this.ControlBox = false;
            this.Controls.Add(this.comboBoxFolios);
            this.Controls.Add(this.comboBoxClientName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxSerie);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxClient);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DialogSearch";
            this.Text = "Buscar Documento";
            this.Load += new System.EventHandler(this.DialogSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.ComboBox comboBoxClient;
        public System.Windows.Forms.ComboBox comboBoxSerie;
        private System.Windows.Forms.ComboBox comboBoxClientName;
        public System.Windows.Forms.ComboBox comboBoxFolios;
    }
}