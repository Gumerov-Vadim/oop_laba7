namespace laba_7
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.object_picker = new System.Windows.Forms.ToolStripMenuItem();
            this.кругToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.треугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.квадратToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.color_picker = new System.Windows.Forms.ToolStripMenuItem();
            this.сгруппироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.разгруппироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.size_changer = new System.Windows.Forms.NumericUpDown();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.size_changer)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowDrop = true;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.object_picker,
            this.color_picker,
            this.сгруппироватьToolStripMenuItem,
            this.разгруппироватьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1064, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.TabStop = true;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // object_picker
            // 
            this.object_picker.AutoSize = false;
            this.object_picker.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.кругToolStripMenuItem1,
            this.треугольникToolStripMenuItem,
            this.квадратToolStripMenuItem});
            this.object_picker.Name = "object_picker";
            this.object_picker.Size = new System.Drawing.Size(100, 20);
            this.object_picker.Text = "Круг";
            // 
            // кругToolStripMenuItem1
            // 
            this.кругToolStripMenuItem1.Name = "кругToolStripMenuItem1";
            this.кругToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.кругToolStripMenuItem1.Text = "Круг";
            this.кругToolStripMenuItem1.Click += new System.EventHandler(this.кругToolStripMenuItem1_Click);
            // 
            // треугольникToolStripMenuItem
            // 
            this.треугольникToolStripMenuItem.Name = "треугольникToolStripMenuItem";
            this.треугольникToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.треугольникToolStripMenuItem.Text = "Треугольник";
            this.треугольникToolStripMenuItem.Click += new System.EventHandler(this.треугольникToolStripMenuItem_Click);
            // 
            // квадратToolStripMenuItem
            // 
            this.квадратToolStripMenuItem.Name = "квадратToolStripMenuItem";
            this.квадратToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.квадратToolStripMenuItem.Text = "Квадрат";
            this.квадратToolStripMenuItem.Click += new System.EventHandler(this.квадратToolStripMenuItem_Click);
            // 
            // color_picker
            // 
            this.color_picker.AutoSize = false;
            this.color_picker.Name = "color_picker";
            this.color_picker.Size = new System.Drawing.Size(100, 20);
            this.color_picker.Text = "Цвет";
            this.color_picker.Click += new System.EventHandler(this.color_picker_Click);
            // 
            // сгруппироватьToolStripMenuItem
            // 
            this.сгруппироватьToolStripMenuItem.AutoSize = false;
            this.сгруппироватьToolStripMenuItem.Name = "сгруппироватьToolStripMenuItem";
            this.сгруппироватьToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.сгруппироватьToolStripMenuItem.Text = "Сгруппировать";
            this.сгруппироватьToolStripMenuItem.Click += new System.EventHandler(this.сгруппироватьToolStripMenuItem_Click);
            // 
            // разгруппироватьToolStripMenuItem
            // 
            this.разгруппироватьToolStripMenuItem.AutoSize = false;
            this.разгруппироватьToolStripMenuItem.Name = "разгруппироватьToolStripMenuItem";
            this.разгруппироватьToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.разгруппироватьToolStripMenuItem.Text = "Разгруппировать";
            // 
            // size_changer
            // 
            this.size_changer.Location = new System.Drawing.Point(455, 4);
            this.size_changer.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.size_changer.Name = "size_changer";
            this.size_changer.Size = new System.Drawing.Size(120, 20);
            this.size_changer.TabIndex = 2;
            this.size_changer.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.size_changer.ValueChanged += new System.EventHandler(this.size_changer_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.size_changer);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.size_changer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem object_picker;
        private System.Windows.Forms.NumericUpDown size_changer;
        private System.Windows.Forms.ToolStripMenuItem кругToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem треугольникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem квадратToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem color_picker;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem сгруппироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem разгруппироватьToolStripMenuItem;
    }
}

