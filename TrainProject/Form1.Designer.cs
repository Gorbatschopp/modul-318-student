namespace TrainProject
{
    partial class Form1
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
            this.FromStationText = new System.Windows.Forms.TextBox();
            this.ToStationText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FromCombo = new System.Windows.Forms.ComboBox();
            this.ToCombo = new System.Windows.Forms.ComboBox();
            this.dataConnections = new System.Windows.Forms.DataGridView();
            this.FromRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LengthRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartureRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArrivalRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.departureDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDepartureTime = new System.Windows.Forms.TextBox();
            this.btnFromMap = new System.Windows.Forms.Button();
            this.btnToMap = new System.Windows.Forms.Button();
            this.btnDepartAt = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnArriveAt = new System.Windows.Forms.RadioButton();
            this.btnClearFromStation = new System.Windows.Forms.Button();
            this.btnClearToStation = new System.Windows.Forms.Button();
            this.btnSwitchStation = new System.Windows.Forms.Button();
            this.findNearStation = new System.Windows.Forms.Button();
            this.btnMakeMail = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataConnections)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Von";
            // 
            // FromStationText
            // 
            this.FromStationText.Location = new System.Drawing.Point(60, 29);
            this.FromStationText.Name = "FromStationText";
            this.FromStationText.Size = new System.Drawing.Size(206, 20);
            this.FromStationText.TabIndex = 1;
            this.FromStationText.TextChanged += new System.EventHandler(this.FromStationText_TextChanged);
            // 
            // ToStationText
            // 
            this.ToStationText.Location = new System.Drawing.Point(60, 103);
            this.ToStationText.Name = "ToStationText";
            this.ToStationText.Size = new System.Drawing.Size(206, 20);
            this.ToStationText.TabIndex = 2;
            this.ToStationText.TextChanged += new System.EventHandler(this.ToStationText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Zu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Verfügbare Verbindungen";
            // 
            // FromCombo
            // 
            this.FromCombo.FormattingEnabled = true;
            this.FromCombo.Location = new System.Drawing.Point(60, 51);
            this.FromCombo.Name = "FromCombo";
            this.FromCombo.Size = new System.Drawing.Size(206, 21);
            this.FromCombo.TabIndex = 10;
            this.FromCombo.SelectedValueChanged += new System.EventHandler(this.ComboFromTextChanged);
            // 
            // ToCombo
            // 
            this.ToCombo.FormattingEnabled = true;
            this.ToCombo.Location = new System.Drawing.Point(60, 129);
            this.ToCombo.Name = "ToCombo";
            this.ToCombo.Size = new System.Drawing.Size(206, 21);
            this.ToCombo.TabIndex = 11;
            this.ToCombo.SelectedValueChanged += new System.EventHandler(this.ComboToTextChanged);
            // 
            // dataConnections
            // 
            this.dataConnections.AllowUserToResizeColumns = false;
            this.dataConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataConnections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FromRow,
            this.ToRow,
            this.LengthRow,
            this.DepartureRow,
            this.ArrivalRow});
            this.dataConnections.Location = new System.Drawing.Point(60, 240);
            this.dataConnections.MultiSelect = false;
            this.dataConnections.Name = "dataConnections";
            this.dataConnections.ReadOnly = true;
            this.dataConnections.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataConnections.Size = new System.Drawing.Size(603, 316);
            this.dataConnections.TabIndex = 12;
            // 
            // FromRow
            // 
            this.FromRow.HeaderText = "Von";
            this.FromRow.Name = "FromRow";
            this.FromRow.ReadOnly = true;
            // 
            // ToRow
            // 
            this.ToRow.HeaderText = "Zu";
            this.ToRow.Name = "ToRow";
            this.ToRow.ReadOnly = true;
            // 
            // LengthRow
            // 
            this.LengthRow.HeaderText = "Fahrzeit";
            this.LengthRow.Name = "LengthRow";
            this.LengthRow.ReadOnly = true;
            // 
            // DepartureRow
            // 
            this.DepartureRow.HeaderText = "Abfahrtszeit";
            this.DepartureRow.Name = "DepartureRow";
            this.DepartureRow.ReadOnly = true;
            // 
            // ArrivalRow
            // 
            this.ArrivalRow.HeaderText = "Ankunftszeit";
            this.ArrivalRow.Name = "ArrivalRow";
            this.ArrivalRow.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(460, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Datum";
            // 
            // departureDatePicker
            // 
            this.departureDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.departureDatePicker.Location = new System.Drawing.Point(463, 37);
            this.departureDatePicker.Name = "departureDatePicker";
            this.departureDatePicker.Size = new System.Drawing.Size(200, 20);
            this.departureDatePicker.TabIndex = 14;
            this.departureDatePicker.ValueChanged += new System.EventHandler(this.departureDatePicker_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Zeit";
            // 
            // txtDepartureTime
            // 
            this.txtDepartureTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepartureTime.Location = new System.Drawing.Point(463, 103);
            this.txtDepartureTime.Name = "txtDepartureTime";
            this.txtDepartureTime.Size = new System.Drawing.Size(73, 20);
            this.txtDepartureTime.TabIndex = 16;
            this.txtDepartureTime.TextChanged += new System.EventHandler(this.txtDepartureTime_TextChanged);
            // 
            // btnFromMap
            // 
            this.btnFromMap.Location = new System.Drawing.Point(272, 51);
            this.btnFromMap.Name = "btnFromMap";
            this.btnFromMap.Size = new System.Drawing.Size(80, 23);
            this.btnFromMap.TabIndex = 17;
            this.btnFromMap.Text = "Karte";
            this.btnFromMap.UseVisualStyleBackColor = true;
            this.btnFromMap.Click += new System.EventHandler(this.btnFromMap_Click);
            // 
            // btnToMap
            // 
            this.btnToMap.Location = new System.Drawing.Point(272, 127);
            this.btnToMap.Name = "btnToMap";
            this.btnToMap.Size = new System.Drawing.Size(80, 23);
            this.btnToMap.TabIndex = 18;
            this.btnToMap.Text = "Karte";
            this.btnToMap.UseVisualStyleBackColor = true;
            this.btnToMap.Click += new System.EventHandler(this.btnToMap_Click);
            // 
            // btnDepartAt
            // 
            this.btnDepartAt.AutoSize = true;
            this.btnDepartAt.Checked = true;
            this.btnDepartAt.Location = new System.Drawing.Point(25, 28);
            this.btnDepartAt.Name = "btnDepartAt";
            this.btnDepartAt.Size = new System.Drawing.Size(38, 17);
            this.btnDepartAt.TabIndex = 19;
            this.btnDepartAt.TabStop = true;
            this.btnDepartAt.Text = "Ab";
            this.btnDepartAt.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnArriveAt);
            this.groupBox1.Controls.Add(this.btnDepartAt);
            this.groupBox1.Location = new System.Drawing.Point(463, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 72);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ab oder Ankunft";
            this.groupBox1.Visible = false;
            // 
            // btnArriveAt
            // 
            this.btnArriveAt.AutoSize = true;
            this.btnArriveAt.Location = new System.Drawing.Point(109, 28);
            this.btnArriveAt.Name = "btnArriveAt";
            this.btnArriveAt.Size = new System.Drawing.Size(38, 17);
            this.btnArriveAt.TabIndex = 20;
            this.btnArriveAt.Text = "An";
            this.btnArriveAt.UseVisualStyleBackColor = true;
            this.btnArriveAt.CheckedChanged += new System.EventHandler(this.btnArriveAt_CheckedChanged);
            // 
            // btnClearFromStation
            // 
            this.btnClearFromStation.Location = new System.Drawing.Point(272, 29);
            this.btnClearFromStation.Name = "btnClearFromStation";
            this.btnClearFromStation.Size = new System.Drawing.Size(80, 20);
            this.btnClearFromStation.TabIndex = 21;
            this.btnClearFromStation.Text = "Löschen";
            this.btnClearFromStation.UseVisualStyleBackColor = true;
            this.btnClearFromStation.Click += new System.EventHandler(this.btnClearFromStation_Click);
            // 
            // btnClearToStation
            // 
            this.btnClearToStation.Location = new System.Drawing.Point(272, 103);
            this.btnClearToStation.Name = "btnClearToStation";
            this.btnClearToStation.Size = new System.Drawing.Size(80, 20);
            this.btnClearToStation.TabIndex = 22;
            this.btnClearToStation.Text = "Löschen";
            this.btnClearToStation.UseVisualStyleBackColor = true;
            this.btnClearToStation.Click += new System.EventHandler(this.btnClearToStation_Click);
            // 
            // btnSwitchStation
            // 
            this.btnSwitchStation.Location = new System.Drawing.Point(272, 77);
            this.btnSwitchStation.Name = "btnSwitchStation";
            this.btnSwitchStation.Size = new System.Drawing.Size(80, 23);
            this.btnSwitchStation.TabIndex = 24;
            this.btnSwitchStation.Text = "Switch";
            this.btnSwitchStation.UseVisualStyleBackColor = true;
            this.btnSwitchStation.Click += new System.EventHandler(this.btnSwitchStation_Click);
            // 
            // findNearStation
            // 
            this.findNearStation.Location = new System.Drawing.Point(359, 29);
            this.findNearStation.Name = "findNearStation";
            this.findNearStation.Size = new System.Drawing.Size(95, 45);
            this.findNearStation.TabIndex = 25;
            this.findNearStation.Text = "Stationen in der nähe finden";
            this.findNearStation.UseVisualStyleBackColor = true;
            this.findNearStation.Click += new System.EventHandler(this.findNearStation_Click);
            // 
            // btnMakeMail
            // 
            this.btnMakeMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMakeMail.Location = new System.Drawing.Point(588, 562);
            this.btnMakeMail.Name = "btnMakeMail";
            this.btnMakeMail.Size = new System.Drawing.Size(75, 23);
            this.btnMakeMail.TabIndex = 26;
            this.btnMakeMail.Text = "Mail machen";
            this.btnMakeMail.UseVisualStyleBackColor = true;
            this.btnMakeMail.Click += new System.EventHandler(this.btnMakeMail_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 613);
            this.Controls.Add(this.btnMakeMail);
            this.Controls.Add(this.findNearStation);
            this.Controls.Add(this.btnSwitchStation);
            this.Controls.Add(this.btnClearToStation);
            this.Controls.Add(this.btnClearFromStation);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnToMap);
            this.Controls.Add(this.btnFromMap);
            this.Controls.Add(this.txtDepartureTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.departureDatePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataConnections);
            this.Controls.Add(this.ToCombo);
            this.Controls.Add(this.FromCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ToStationText);
            this.Controls.Add(this.FromStationText);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(757, 652);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing_1);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataConnections)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FromStationText;
        private System.Windows.Forms.TextBox ToStationText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FromCombo;
        private System.Windows.Forms.ComboBox ToCombo;
        private System.Windows.Forms.DataGridView dataConnections;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn LengthRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartureRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArrivalRow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker departureDatePicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDepartureTime;
        private System.Windows.Forms.Button btnFromMap;
        private System.Windows.Forms.Button btnToMap;
        private System.Windows.Forms.RadioButton btnDepartAt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton btnArriveAt;
        private System.Windows.Forms.Button btnClearFromStation;
        private System.Windows.Forms.Button btnClearToStation;
        private System.Windows.Forms.Button btnSwitchStation;
        private System.Windows.Forms.Button findNearStation;
        private System.Windows.Forms.Button btnMakeMail;
    }
}

