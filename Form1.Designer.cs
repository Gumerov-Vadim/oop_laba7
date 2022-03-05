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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Узел0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Узел2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Узел1", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.object_picker = new System.Windows.Forms.ToolStripMenuItem();
            this.кругToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.треугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.квадратToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.color_picker = new System.Windows.Forms.ToolStripMenuItem();
            this.size_changer = new System.Windows.Forms.NumericUpDown();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.size_changer)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowDrop = true;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.object_picker,
            this.color_picker});
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
            this.кругToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.кругToolStripMenuItem1.Text = "Круг";
            this.кругToolStripMenuItem1.Click += new System.EventHandler(this.кругToolStripMenuItem1_Click);
            // 
            // треугольникToolStripMenuItem
            // 
            this.треугольникToolStripMenuItem.Name = "треугольникToolStripMenuItem";
            this.треугольникToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.треугольникToolStripMenuItem.Text = "Треугольник";
            this.треугольникToolStripMenuItem.Click += new System.EventHandler(this.треугольникToolStripMenuItem_Click);
            // 
            // квадратToolStripMenuItem
            // 
            this.квадратToolStripMenuItem.Name = "квадратToolStripMenuItem";
            this.квадратToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.квадратToolStripMenuItem.Text = "Квадрат";
            this.квадратToolStripMenuItem.Click += new System.EventHandler(this.квадратToolStripMenuItem_Click);
            // 
            // color_picker
            // 
            this.color_picker.Name = "color_picker";
            this.color_picker.Size = new System.Drawing.Size(45, 20);
            this.color_picker.Text = "Цвет";
            this.color_picker.Click += new System.EventHandler(this.color_picker_Click);
            // 
            // size_changer
            // 
            this.size_changer.Location = new System.Drawing.Point(292, 4);
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
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(909, 27);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Узел0";
            treeNode1.Text = "Узел0";
            treeNode2.Name = "Узел2";
            treeNode2.Text = "Узел2";
            treeNode3.Name = "Узел1";
            treeNode3.Text = "Узел1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(155, 561);
            this.treeView1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.size_changer);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.TreeView treeView1;
    }
}

