﻿namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    partial class dailyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.moldTrackerDataSet = new UI_Insert_Del_UpdateView_Data_Grid_.MoldTrackerDataSet();
            this.moldTrackerDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.moldTrackerDataSetBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1213, 722);
            this.dataGridView1.TabIndex = 0;
            // 
            // moldTrackerDataSet
            // 
            this.moldTrackerDataSet.DataSetName = "MoldTrackerDataSet";
            this.moldTrackerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // moldTrackerDataSetBindingSource
            // 
            this.moldTrackerDataSetBindingSource.DataSource = this.moldTrackerDataSet;
            this.moldTrackerDataSetBindingSource.Position = 0;
            // 
            // dailyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "dailyControl";
            this.Size = new System.Drawing.Size(1213, 722);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource moldTrackerDataSetBindingSource;
        private MoldTrackerDataSet moldTrackerDataSet;
    }
}
