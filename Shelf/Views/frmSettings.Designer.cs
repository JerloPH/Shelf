
namespace Shelf.Views
{
    partial class frmSettings
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
            this.panelGrid = new System.Windows.Forms.Panel();
            this.gridSetting = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCaption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRestart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelControls = new System.Windows.Forms.Panel();
            this.panelAction = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.panelValueSet = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.panelValue = new System.Windows.Forms.Panel();
            this.panelDesc = new System.Windows.Forms.Panel();
            this.lblDesc = new System.Windows.Forms.Label();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetting)).BeginInit();
            this.panelControls.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelValueSet.SuspendLayout();
            this.panelDesc.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.gridSetting);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(662, 433);
            this.panelGrid.TabIndex = 0;
            // 
            // gridSetting
            // 
            this.gridSetting.AllowUserToAddRows = false;
            this.gridSetting.AllowUserToDeleteRows = false;
            this.gridSetting.AllowUserToResizeRows = false;
            this.gridSetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colCaption,
            this.colValue,
            this.colDesc,
            this.colRestart,
            this.colSetType});
            this.gridSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSetting.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridSetting.Location = new System.Drawing.Point(0, 0);
            this.gridSetting.Name = "gridSetting";
            this.gridSetting.RowHeadersWidth = 51;
            this.gridSetting.RowTemplate.Height = 29;
            this.gridSetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSetting.Size = new System.Drawing.Size(662, 433);
            this.gridSetting.TabIndex = 1;
            this.gridSetting.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridSetting_CellFormatting);
            this.gridSetting.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSetting_CellMouseEnter);
            this.gridSetting.SelectionChanged += new System.EventHandler(this.gridSetting_SelectionChanged);
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.Visible = false;
            // 
            // colCaption
            // 
            this.colCaption.DataPropertyName = "Caption";
            this.colCaption.HeaderText = "Caption";
            this.colCaption.MinimumWidth = 6;
            this.colCaption.Name = "colCaption";
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "Value";
            this.colValue.HeaderText = "Value";
            this.colValue.MinimumWidth = 6;
            this.colValue.Name = "colValue";
            // 
            // colDesc
            // 
            this.colDesc.DataPropertyName = "Description";
            this.colDesc.HeaderText = "Description";
            this.colDesc.MinimumWidth = 6;
            this.colDesc.Name = "colDesc";
            this.colDesc.Visible = false;
            // 
            // colRestart
            // 
            this.colRestart.DataPropertyName = "Restart";
            this.colRestart.HeaderText = "Restart Required";
            this.colRestart.MinimumWidth = 6;
            this.colRestart.Name = "colRestart";
            this.colRestart.Visible = false;
            // 
            // colSetType
            // 
            this.colSetType.DataPropertyName = "settingType";
            this.colSetType.HeaderText = "Setting Type";
            this.colSetType.MinimumWidth = 6;
            this.colSetType.Name = "colSetType";
            this.colSetType.Visible = false;
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.panelAction);
            this.panelControls.Controls.Add(this.panelValueSet);
            this.panelControls.Controls.Add(this.panelDesc);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 272);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(662, 161);
            this.panelControls.TabIndex = 1;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.btnSave);
            this.panelAction.Controls.Add(this.btnReset);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAction.Location = new System.Drawing.Point(0, 97);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(662, 64);
            this.panelAction.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(201, 64);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnReset.Location = new System.Drawing.Point(461, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(201, 64);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // panelValueSet
            // 
            this.panelValueSet.Controls.Add(this.btnApply);
            this.panelValueSet.Controls.Add(this.panelValue);
            this.panelValueSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelValueSet.Location = new System.Drawing.Point(0, 41);
            this.panelValueSet.Name = "panelValueSet";
            this.panelValueSet.Size = new System.Drawing.Size(662, 56);
            this.panelValueSet.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnApply.Location = new System.Drawing.Point(500, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(162, 56);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "APPLY";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // panelValue
            // 
            this.panelValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panelValue.Location = new System.Drawing.Point(0, 0);
            this.panelValue.Margin = new System.Windows.Forms.Padding(4);
            this.panelValue.Name = "panelValue";
            this.panelValue.Size = new System.Drawing.Size(662, 56);
            this.panelValue.TabIndex = 0;
            // 
            // panelDesc
            // 
            this.panelDesc.Controls.Add(this.lblDesc);
            this.panelDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDesc.Location = new System.Drawing.Point(0, 0);
            this.panelDesc.Name = "panelDesc";
            this.panelDesc.Size = new System.Drawing.Size(662, 41);
            this.panelDesc.TabIndex = 0;
            // 
            // lblDesc
            // 
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(662, 41);
            this.lblDesc.TabIndex = 0;
            this.lblDesc.Text = "Description";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 433);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.panelGrid);
            this.MinimumSize = new System.Drawing.Size(650, 460);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSetting)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.panelAction.ResumeLayout(false);
            this.panelValueSet.ResumeLayout(false);
            this.panelDesc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.DataGridView gridSetting;
        private System.Windows.Forms.Panel panelDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Panel panelValueSet;
        private System.Windows.Forms.Panel panelValue;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCaption;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRestart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSetType;
    }
}