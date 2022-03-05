using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Newtonsoft.Json;
using System.IO;
namespace laba_7
{

    public partial class Form1 : Form
    {
        public class Settings
        {
            private int selected;
            private int size;
            private Color color;
            public System.EventHandler observers;
            public int pick_obj(int i) { _ = (i >= 0) ? selected = i : selected = 0; observers.Invoke(this, null); return selected; }
            public int pick_obj() { return selected; }
            public int resize(int size) { _ = (size >= 10 && size <= 100) ? this.size = size : size = this.size; observers.Invoke(this, null); return size; }
            public int resize() { return size; }
            public void set_color(Color color) { this.color = color; }
            public Color get_color() { return color; }
            public Settings()
            {
                selected = 0;
                size = 30;
                color = Color.Green;
            }
        }
        interface IObject
        {
          //  void set_p(int x, int y, int size, Color color);
            void set(int x, int y);
            void add(int x, int y);
            void set_color(Color color);
            void set_size(int size);
         //   System.Windows.Forms.Button inside();
        }
        public class Group : IObject
        {
            public Group[] elements;
            int count;
            public Group()
            {
                count = 100;
                elements = new Group[100];
            }
            public Group(int n)
            {
                count = n;
                elements = new Group[n];
            }
            public bool add(Group el)
            {
                for(int i = 0; i < count; i++)
                {
                    if(elements[i] != null) { elements[i] = el;return true; }
                }
                return false;
            }
            virtual public void set(int x, int y) {
                for(int i = 0; i < count; i++)
                {
                    if(elements[i] != null)
                    {
                        elements[i].set(x, y);
                    }
                }
            }
            virtual public void add(int x, int y)
            {
                for (int i = 0; i < count; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].add(x, y);
                    }
                }
            }
            virtual public void set_color(Color color)
            {
                for (int i = 0; i < count; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].set_color(color);
                    }
                }
            }
            virtual public void set_size(int size)
            {
                for (int i = 0; i < count; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].set_size(size);
                    }
                }
            }
        }
        public class Object : Group
        {
            private void set_p(int x, int y, int size, Color color)
            {
                _x = x; _y = y; _size = size;
                _color = color;
                obj.FlatStyle = FlatStyle.Flat;
                obj.FlatAppearance.BorderSize = 0;
                obj.Width = size;
                obj.Height = size;
                obj.Location = new System.Drawing.Point(_x - obj.Width / 2, _y - obj.Height / 2);
                obj.BackColor = color;
            }
            protected int _x, _y, _size;
            protected Color _color;
            public bool _selected;
            public System.Windows.Forms.Button obj = new System.Windows.Forms.Button();
            //public System.Windows.Forms.MouseEventHandler click_circle;
            public void set(int x, int y)
            {
                int p = _size / 2;
                if (x > p && x <= 1080 - p -22 && y > p + 22 && y <= 720 -40 - p)
                {
                    _x = x; _y = y;
                    obj.Location = new System.Drawing.Point(x - p, y - p);
                }
            }
            public void add(int x, int y)
            {
                this.set(_x + x, _y + y);
            }
            public void set_color(Color color)
            {
                _color = color;
                obj.BackColor = _color;
            }
            public void set_size(int size) { _ = size > 0 ? _size = size : size; }
            public Object()
            {
                set_p(10, 10, 60, System.Drawing.Color.Green);
                //circle.Click += new EventHandler(select_circle);

            }
            public Object(int x, int y)
            {
                set_p(x, y, 60, System.Drawing.Color.Green);
            }
            public Object(int x, int y, int size)
            {
                set_p(x, y, size, System.Drawing.Color.Green);
            }
            public Object(int x, int y, int size, Color color)
            {
                set_p(x, y, size, color);
            }
            virtual public System.Windows.Forms.Button inside()
            {
                return obj;
            }
            public bool select()
            {
                return _selected;
            }
            public bool select(bool _select)
            {
                if (_select)
                {
                    this._selected = true;
                    obj.BackColor = System.Drawing.Color.Purple;
                    return true;
                }
                else
                {
                    this._selected = false;
                    obj.BackColor = this._color;
                    return false;
                }
            }
        }
        public class Circle : Object
        {
            private int _radius;
            private void set_p()
            {
                _radius = _size / 2;
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, _size, _size);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Circle() { set_p(); }
            public Circle(int x, int y) : base(x, y) { set_p(); }
            public Circle(int x, int y, int radius) : base(x, y, radius * 2) { set_p(); }
            public Circle(int x, int y, int radius, Color color) : base(x, y, radius * 2, color) { set_p(); }
        }
        public class Square : Object
        {
            public Square() { }
            public Square(int x, int y) : base(x, y) { }
            public Square(int x, int y, int size) : base(x, y, size) { }
            public Square(int x, int y, int size, Color color) : base(x, y, size, color) { }
        }
        public class Triangle : Object
        {
            private void set_p()
            {
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddPolygon(new[] {
                    new Point(0, obj.Height),
                    new Point(obj.Height, obj.Width),
                    new Point(obj.Width / 2, 0)
                });
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Triangle() { set_p(); }
            public Triangle(int x, int y) : base(x, y) { set_p(); }
            public Triangle(int x, int y, int size) : base(x, y, size) { set_p(); }
            public Triangle(int x, int y, int size, Color color) : base(x, y, size, color) { set_p(); }
        }
        public class Storage
        {
            int _size;
            public Object[] massive;
            public int size() { return _size; }
            //public void add(int x, int y)
            //{
            //    int i = 0;
            //    while (i < _size && massive[i] != null)
            //    {
            //        i++;
            //    }
            //    if (i != _size)
            //    {
            //        massive[i] = new Triangle(x, y);
            //        massive[i].inside().Name = (i).ToString();
            //    }
            //}
            public void add(Object obj)
            {
                int i = 0;
                while (i < _size && massive[i] != null)
                {
                    i++;
                }
                if (i != _size)
                {
                    massive[i] = obj;
                    massive[i].inside().Name = (i).ToString();
                }
            }
            public void select_clear()
            {
                int i = 0;
                while (i < _size)
                {
                    if (massive[i] != null)
                    {
                        massive[i].select(false);
                    }
                    i++;
                }
            }
            public void recolor_selected(Color color)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (this.massive[i] != null)
                    {
                        if (this.massive[i].select()) { this.massive[i].set_color(color); }
                    }
                }
            }
            public void move_selected(int x, int y)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (this.massive[i] != null)
                    {
                        if (this.massive[i].select()) { massive[i].add(x, y); }
                    }
                }
            }
            public int del_selected()
            {
                int i = 0;
                int k = 0;
                while (i < _size)
                {
                    if (massive[i] != null && massive[i].select())
                    {
                        massive[i] = null;
                        k++;
                    }
                    i++;
                }
                return k;
            }
            public Object get(int i)
            {
                if (i < _size)
                {
                    return massive[i];
                }
                return null;
            }
            public Storage()
            {
                _size = 100;
                massive = new Object[100];
            }
            public Storage(int n)
            {
                _size = n;
                massive = new Object[n];
            }
        }
        Storage storage = new Storage();
        //int i = 0;
        Settings obj_settings = new Settings();
        public Form1()
        {
            InitializeComponent();
            obj_settings.observers += new EventHandler(this.updatefromsettings);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //storage = File.Exists("save.json") ? JsonConvert.DeserializeObject<Storage>(File.ReadAllText("save.json")) : new Storage();
            int size = storage.size();
            int k = 0;
            while (k < size)
            {
                if (storage.get(k) != null)
                {
                    System.Windows.Forms.Button circle = storage.get(k).inside();
                    circle.MouseClick += select_obj;
                    circle.KeyDown += del_selected_obj;
                }
                k++;
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //            i++;
            int p = obj_settings.resize();
            if (e.X > p && e.X <= 1080 - p - 22 && e.Y > p + 22 && e.Y <= 720 - p - 40)
            {
                Object obj = null;
                switch (obj_settings.pick_obj())
                {
                    case 0:
                        obj = new Circle(e.X, e.Y, obj_settings.resize() / 2, obj_settings.get_color());
                        break;
                    case 1:
                        obj = new Triangle(e.X, e.Y, obj_settings.resize(), obj_settings.get_color());
                        break;
                    case 2:
                        obj = new Square(e.X, e.Y, obj_settings.resize(), obj_settings.get_color());
                        break;
                    default:
                        obj = null;
                        //impossible
                        break;
                }
                storage.add(obj);
                if (obj != null)
                {
                    obj.inside().MouseClick += select_obj;
                    obj.inside().KeyDown += del_selected_obj;
                    obj.inside().KeyDown += move_obj;
                    this.Controls.Add(obj.inside());
                    //                label1.Text = i.ToString();
                    storage.select_clear();
                    obj.select(true);
                }
            }
        }
        private void select_obj(object sender, MouseEventArgs e)
        {
            Object circle = null;
            int k = 0;
            int size = storage.size();
            while (k < size)
            {
                if (storage.get(k) != null && sender == storage.get(k).inside())
                {
                    circle = storage.get(k);
                }
                k++;
            }
            if (Control.ModifierKeys == Keys.Control)
            {
                circle.select(!circle.select());
            }
            else
            {
                storage.select_clear();
                circle.select(true);
            }
        }
        private void del_selected_obj(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                {
                    Object circle = null;
                    int k = 0;
                    int size = storage.size();
                    while (k < size)
                    {
                        circle = storage.get(k);
                        if (circle != null && circle.select())
                        {
                            Controls.Remove(circle.inside());
                        }
                        k++;
                    }
                    storage.del_selected();
                    //                i = i - storage.del_selected();
                    //                label1.Text = i.ToString();
                }
            }
        }
        private void move_obj(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                storage.move_selected(0, -1);
            }
            if (e.KeyCode == Keys.A)
            {
                storage.move_selected(-1, 0);
            }
            if (e.KeyCode == Keys.S)
            {
                storage.move_selected(0, 1);
            }
            if (e.KeyCode == Keys.D)
            {
                storage.move_selected(1, 0);
            }
        }
        private void paint(object sender, EventArgs e)
        {
            int size = storage.size();
            int k = 0;
            Object circle = null;
            Controls.Clear();
            while (k < size)
            {
                circle = storage.get(k);
                if (circle != null)
                {
                    Controls.Add(circle.inside());
                }
                k++;
            }
        }
        private void updatefromsettings(object sender, EventArgs e)
        {
            switch (obj_settings.pick_obj())
            {
                case 0:
                    object_picker.Text = "Круг";
                    break;
                case 1:
                    object_picker.Text = "Треугольник";
                    break;
                case 2:
                    object_picker.Text = "Квадрат";
                    break;
            }
            object_picker.Size = new System.Drawing.Size(100, 20);
        }
        private void кругToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(0);
        }
        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(1);
        }
        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(2);
        }
        private void size_changer_ValueChanged(object sender, EventArgs e)
        {
            obj_settings.resize((int)size_changer.Value);
        }
        private void color_picker_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                obj_settings.set_color(colorDialog.Color);
                storage.recolor_selected(colorDialog.Color);
            }
        }
    }
}
