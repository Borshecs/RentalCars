namespace RentalCost
{
    partial class Form4
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_idDis = new System.Windows.Forms.ComboBox();
            this.comboBox_idcars = new System.Windows.Forms.ComboBox();
            this.rentcostDataSet12 = new RentalCost.rentcostDataSet12();
            this.discountsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.discountsTableAdapter = new RentalCost.rentcostDataSet12TableAdapters.DiscountsTableAdapter();
            this.rentcostDataSet13 = new RentalCost.rentcostDataSet13();
            this.issuedVehiclesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.issuedVehiclesTableAdapter = new RentalCost.rentcostDataSet13TableAdapters.IssuedVehiclesTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rentcostDataSet12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.discountsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentcostDataSet13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.issuedVehiclesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-16, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 69);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(229, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Запись скидки определенная";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(99, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID скидки";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(99, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "ID выданного авто";
            // 
            // comboBox_idDis
            // 
            this.comboBox_idDis.DataSource = this.discountsBindingSource;
            this.comboBox_idDis.DisplayMember = "ID_Discounts";
            this.comboBox_idDis.FormattingEnabled = true;
            this.comboBox_idDis.Location = new System.Drawing.Point(327, 245);
            this.comboBox_idDis.Name = "comboBox_idDis";
            this.comboBox_idDis.Size = new System.Drawing.Size(147, 21);
            this.comboBox_idDis.TabIndex = 3;
            this.comboBox_idDis.ValueMember = "ID_Discounts";
            // 
            // comboBox_idcars
            // 
            this.comboBox_idcars.DataSource = this.issuedVehiclesBindingSource;
            this.comboBox_idcars.DisplayMember = "ID_IssuedVehicles";
            this.comboBox_idcars.FormattingEnabled = true;
            this.comboBox_idcars.Location = new System.Drawing.Point(327, 278);
            this.comboBox_idcars.Name = "comboBox_idcars";
            this.comboBox_idcars.Size = new System.Drawing.Size(147, 21);
            this.comboBox_idcars.TabIndex = 4;
            this.comboBox_idcars.ValueMember = "ID_IssuedVehicles";
            // 
            // rentcostDataSet12
            // 
            this.rentcostDataSet12.DataSetName = "rentcostDataSet12";
            this.rentcostDataSet12.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // discountsBindingSource
            // 
            this.discountsBindingSource.DataMember = "Discounts";
            this.discountsBindingSource.DataSource = this.rentcostDataSet12;
            // 
            // discountsTableAdapter
            // 
            this.discountsTableAdapter.ClearBeforeFill = true;
            // 
            // rentcostDataSet13
            // 
            this.rentcostDataSet13.DataSetName = "rentcostDataSet13";
            this.rentcostDataSet13.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // issuedVehiclesBindingSource
            // 
            this.issuedVehiclesBindingSource.DataMember = "IssuedVehicles";
            this.issuedVehiclesBindingSource.DataSource = this.rentcostDataSet13;
            // 
            // issuedVehiclesTableAdapter
            // 
            this.issuedVehiclesTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(258, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_idcars);
            this.Controls.Add(this.comboBox_idDis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rentcostDataSet12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.discountsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rentcostDataSet13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.issuedVehiclesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_idDis;
        private System.Windows.Forms.ComboBox comboBox_idcars;
        private rentcostDataSet12 rentcostDataSet12;
        private System.Windows.Forms.BindingSource discountsBindingSource;
        private rentcostDataSet12TableAdapters.DiscountsTableAdapter discountsTableAdapter;
        private rentcostDataSet13 rentcostDataSet13;
        private System.Windows.Forms.BindingSource issuedVehiclesBindingSource;
        private rentcostDataSet13TableAdapters.IssuedVehiclesTableAdapter issuedVehiclesTableAdapter;
        private System.Windows.Forms.Button button1;
    }
}