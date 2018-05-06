namespace UI_Insert_Del_UpdateView_Data_Grid_
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
            this.planDniaBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.moldTrackerDataSet3 = new UI_Insert_Del_UpdateView_Data_Grid_.MoldTrackerDataSet3();
            this.moldTrackerDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.moldTrackerDataSet = new UI_Insert_Del_UpdateView_Data_Grid_.MoldTrackerDataSet();
            this.moldTrackerDataSet2 = new UI_Insert_Del_UpdateView_Data_Grid_.MoldTrackerDataSet2();
            this.planDniaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.planDniaTableAdapter = new UI_Insert_Del_UpdateView_Data_Grid_.MoldTrackerDataSet2TableAdapters.planDniaTableAdapter();
            this.planDniaTableAdapter1 = new UI_Insert_Del_UpdateView_Data_Grid_.MoldTrackerDataSet3TableAdapters.planDniaTableAdapter();
            this.dailyGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.planDniaBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planDniaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dailyGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // planDniaBindingSource1
            // 
            this.planDniaBindingSource1.DataMember = "planDnia";
            this.planDniaBindingSource1.DataSource = this.moldTrackerDataSet3;
            // 
            // moldTrackerDataSet3
            // 
            this.moldTrackerDataSet3.DataSetName = "MoldTrackerDataSet3";
            this.moldTrackerDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // moldTrackerDataSetBindingSource
            // 
            this.moldTrackerDataSetBindingSource.DataSource = this.moldTrackerDataSet;
            this.moldTrackerDataSetBindingSource.Position = 0;
            // 
            // moldTrackerDataSet
            // 
            this.moldTrackerDataSet.DataSetName = "MoldTrackerDataSet";
            this.moldTrackerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // moldTrackerDataSet2
            // 
            this.moldTrackerDataSet2.DataSetName = "MoldTrackerDataSet2";
            this.moldTrackerDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // planDniaBindingSource
            // 
            this.planDniaBindingSource.DataMember = "planDnia";
            this.planDniaBindingSource.DataSource = this.moldTrackerDataSet2;
            // 
            // planDniaTableAdapter
            // 
            this.planDniaTableAdapter.ClearBeforeFill = true;
            // 
            // planDniaTableAdapter1
            // 
            this.planDniaTableAdapter1.ClearBeforeFill = true;
            // 
            // dailyGrid
            // 
            this.dailyGrid.AllowUserToAddRows = false;
            this.dailyGrid.AllowUserToDeleteRows = false;
            this.dailyGrid.AllowUserToResizeColumns = false;
            this.dailyGrid.AllowUserToResizeRows = false;
            this.dailyGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dailyGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dailyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dailyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyGrid.Location = new System.Drawing.Point(0, 0);
            this.dailyGrid.Name = "dailyGrid";
            this.dailyGrid.ReadOnly = true;
            this.dailyGrid.Size = new System.Drawing.Size(1300, 800);
            this.dailyGrid.TabIndex = 0;
            // 
            // dailyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dailyGrid);
            this.Name = "dailyControl";
            this.Size = new System.Drawing.Size(1300, 800);
            ((System.ComponentModel.ISupportInitialize)(this.planDniaBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moldTrackerDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planDniaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dailyGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource moldTrackerDataSetBindingSource;
        private MoldTrackerDataSet moldTrackerDataSet;
        private System.Windows.Forms.BindingSource planDniaBindingSource;
        private MoldTrackerDataSet2 moldTrackerDataSet2;
        private MoldTrackerDataSet2TableAdapters.planDniaTableAdapter planDniaTableAdapter;
        private System.Windows.Forms.BindingSource planDniaBindingSource1;
        private MoldTrackerDataSet3 moldTrackerDataSet3;
        private MoldTrackerDataSet3TableAdapters.planDniaTableAdapter planDniaTableAdapter1;
        private System.Windows.Forms.DataGridView dailyGrid;
    }
}
